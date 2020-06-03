using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private BallManager() { }
    private GameObject _gameObject;
    private Transform _transform;
    private Rigidbody _rigidbody;
    [SerializeField] private Material _material;
    [SerializeField] private float acceleration = 200;
    [SerializeField] private float maxSpeed = 5;
    private int currentColorNo = 0;
    [SerializeField] private GameObject paintTrailPrefab;
    private ColorTrail activeColorTrail;
    public LayerMask isGroundedLayer;
    private bool isLevelCleared = false;
    private Vector3 fallStartPosition;
    private Vector3 fallFinalPosition;

    private void Start()
    {
        _gameObject = gameObject;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        ChangeColor(0,true);//White color 
        _rigidbody.MovePosition(transform.position + new Vector3(0,20,0));
        _rigidbody.velocity = Vector3.down * 15;
        _rigidbody.maxAngularVelocity = maxSpeed;
    }
    private void Update()
    {
        if(!isLevelCleared)
        {
            BallControl();
            ArrangeColorTrail();
            CheckBallHeight();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fountain"))
        {
            int fountainColorNo = other.GetComponent<Fountain>().colorNo;
            OnTouchFountain(fountainColorNo);
        }
        if(other.CompareTag("Final Point"))
        {
            fallStartPosition = _transform.position;
            fallFinalPosition = other.transform.position + new Vector3(0,-5, 0);
            GameManager.Instance.OnBallTouchFinalPoint();            
        }
        if(other.CompareTag("Final Point"))
        {
            fallStartPosition = _transform.position;
            fallFinalPosition = other.transform.position + new Vector3(0, -5, 0);
            GameManager.Instance.OnBallTouchFinalPoint();
        }
        if(other.CompareTag("Gem Star"))
        {
            other.GetComponent<Animator>().SetTrigger("Collect");
            other.GetComponent<SphereCollider>().enabled = false;
            GameManager.Instance.OnGemCollectedStar();
        }
        if(other.CompareTag("Gem Diamond"))
        {
            other.GetComponent<Animator>().SetTrigger("Collect");
            other.GetComponent<SphereCollider>().enabled = false;
            GameManager.Instance.OnGemCollectedDiamond();
        }
    }

    public int GetBallCurrentColorNo()
    {
        return currentColorNo;
    }

    public Color32 GetBallCurrentColor()
    {
        return _material.color;
    }

    private void ArrangeColorTrail()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.7F, isGroundedLayer);
        activeColorTrail.SetPosition(_transform.position - new Vector3(0, 0.49F, 0), isGrounded);
    }

    void CheckBallHeight()
    {
        if(transform.position.y < -10)
        {
            GameManager.Instance.OnBallFell();
        }
    }


    /// <summary>
    /// Movement scripts of the ball.
    /// </summary>
    private void BallControl()
    {
        Vector2 joyStickValues = GameManager.Instance.GetJoystick();
        Vector3 force = new Vector3(joyStickValues.y, 0, -joyStickValues.x) * acceleration;
        _rigidbody.AddTorque(force);
    }
    /// <summary>
    /// Function that works when a paint drop from the color fountain hits on the ball.
    /// </summary>
    private void OnTouchFountain(int fountainColorNo)
    {
        int newColorNo = GameManager.Instance.ColorMix(currentColorNo, fountainColorNo);
        ChangeColor(newColorNo);
    }
    /// <summary>
    /// Make the ball change it's color.
    /// </summary>
    /// <param name="colorNo"></param>
    private void ChangeColor(int newColor, bool forceChange = false)
    {
        if(newColor == currentColorNo && !forceChange) { return; }//Avoids same color changes
        Color32 currentColor = GameManager.Instance.GetColor(currentColorNo);
        StartCoroutine(ChangeColorInTime(currentColor, newColor));
    }
    IEnumerator ChangeColorInTime(Color32 from, int newColorNo)
    {
        Color32 newColor = GameManager.Instance.GetColor(newColorNo);
        activeColorTrail = Instantiate(paintTrailPrefab, _transform.position, Quaternion.identity).GetComponent<ColorTrail>();
        activeColorTrail.GetComponent<ColorTrail>().ChangeColor(newColorNo);
        float timer = 0;
        while(timer < 1)
        {
            timer += 2 * Time.deltaTime;
            Color32 color = Color32.Lerp(from, newColor, timer);
            _material.SetColor("_Color", color);
            yield return new WaitForEndOfFrame();
        }
        currentColorNo = newColorNo;
    }

    public void OnLevelCleared()
    {
        isLevelCleared = true;
        StartCoroutine(FallInToHole());
    }

    IEnumerator FallInToHole()
    {
        _rigidbody.velocity = Vector3.zero;
        GetComponent<SphereCollider>().isTrigger = true;

        float timer = 0;

        while(timer < 3)
        {
            timer += Time.deltaTime;
            Vector3 fallingPosition = Vector3.Lerp(fallStartPosition, fallFinalPosition, timer);
            _rigidbody.MovePosition(fallingPosition);
            yield return new WaitForEndOfFrame();
        }
        _gameObject.SetActive(false);
    }
}
