using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CameraManager() { }
    Transform _transform;
    [SerializeField] Transform target;
    [SerializeField] float height = 12;
    [SerializeField] float distance = 10;
    [SerializeField] float followSpeed = 5;
    private float timer = 0;

    private void Awake()
    {
        _transform = transform;
        FollowTarget(target.position,1000); // Arrange first position.
        transform.localEulerAngles = new Vector3(40,0,0);
    }

    void LateUpdate()
    {
        //Wait for ball to settle down.
        if(timer < 2)
        {
            timer += Time.deltaTime;
        }
        if(target && timer >= 2)
        {
            FollowTarget(target.position, followSpeed);
        }
    }

    public void OnBallFell()
    {
        target = null;
    }

    public void OnLevelCleared()
    {
        target = null;
    }

    private void FollowTarget(Vector3 targetPosition, float _followSpeed)
    {
        Vector3 followPosition = targetPosition + new Vector3(0, height, -distance);
        _transform.position = Vector3.Slerp(_transform.position, followPosition, _followSpeed * Time.deltaTime);

    }
}
