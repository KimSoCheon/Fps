using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletEffect;
    public GameObject groundEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;
        if (y < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ºÎ‹HÈù ¾Ö´Â ::::" + collision.gameObject.name);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameObject effct = Instantiate(groundEffect) as GameObject;
            effct.transform.position = transform.position;
        }
        else
        {
            GameObject effct = Instantiate(bulletEffect) as GameObject;
            effct.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
