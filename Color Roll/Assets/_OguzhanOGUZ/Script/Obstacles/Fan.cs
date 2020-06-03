using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] float fanForce = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            Vector3 forceDirection = Vector3.Normalize(other.transform.position - _transform.position);
            other.GetComponent<Rigidbody>().AddForce(-transform.forward * fanForce);
        }
    }
}
