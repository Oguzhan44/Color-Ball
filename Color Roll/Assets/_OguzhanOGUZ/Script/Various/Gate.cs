using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public int colorNo = 1;
    [SerializeField] private MeshRenderer blockDoorMesh;
    [SerializeField] private Transform gate;
    [SerializeField] List<MeshRenderer> gameMashes;
    private bool isDoorOpened = false;

    private void Start()
    {
        Color32 gateColor = GameManager.Instance.GetColor(colorNo);
        Material newMat = new Material(Shader.Find("Standard"));
        newMat.SetColor("_Color", gateColor);
        newMat.SetFloat("_Metallic", 0.5F);
        newMat.SetFloat("_Glossiness", 0.5F);
        blockDoorMesh.material = newMat;

        foreach(var item in gameMashes)
        {
            item.material = newMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            if(GameManager.Instance.GetBallCurrentColorNo() == colorNo)
            {
                OpenTheDoor();
            }
        }
    }

    void OpenTheDoor()
    {
        if(!isDoorOpened)
        {
            isDoorOpened = true;
            StartCoroutine(OpenDoorInTime());
        }
    }

    IEnumerator OpenDoorInTime()
    {
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            gate.localScale = new Vector3(1 - timer, 1, 1);
            yield return new WaitForEndOfFrame();
        }        
    }
}
