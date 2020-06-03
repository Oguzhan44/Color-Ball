using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float range = 3;
    [SerializeField] Transform piston;

    // Update is called once per frame
    void Update()
    {
        piston.localPosition = new Vector3(0,Mathf.Abs(Mathf.Sin(speed * Time.time)) * -range, 0);
    }
}
