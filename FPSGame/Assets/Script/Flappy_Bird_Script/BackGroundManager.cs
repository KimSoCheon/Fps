using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public Vector3 originPos;
    public float speed;
    public float limitX;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x < -limitX)
        {
            transform.position = originPos;
        }
    }
}
