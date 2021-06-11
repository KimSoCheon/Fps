using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBird : MonoBehaviour
{
    public float jumpSpeed;
    public bool isStart = false;
    public GameObject birds;
    public GameObject gameOverText;


    Rigidbody rigid;
    void Start()

    {
        rigid = GetComponent<Rigidbody>();
        gameOverText.SetActive(false);
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, jumpSpeed,0f);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Destroy(birds);
            gameOverText.SetActive(true);
        }
    }
}
