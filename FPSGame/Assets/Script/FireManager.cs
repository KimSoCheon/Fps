using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject bulletPreFabs;
    public Transform firePosition;

    public float power = 25f;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPreFabs) as GameObject;

            bullet.transform.position = firePosition.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = cameraTransform.forward * power;
        }
    }
}
