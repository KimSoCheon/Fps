using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Rok : MonoBehaviour
{
    public float secsitvty = 500f;
    public float rotationX;
    public float rotationY;
    //public float limitY = 360f;
    public PlayerState playerState;
    void Start()
    {
        playerState = transform.parent.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (playerState.isDead)
            return;

        float mouseMoveValueX = Input.GetAxis("Mouse X");
        float mouseMoveValueY = Input.GetAxis("Mouse Y");
        //Debug.Log("mouseMoveValueX:::" + mouseMoveValueX);
        //Debug.Log("mouseMoveValueY:::" + mouseMoveValueY);
        rotationY += mouseMoveValueX * secsitvty * Time.deltaTime;
        rotationX += mouseMoveValueY * secsitvty * Time.deltaTime;

        //화면 회전값 고정 범위
        if (rotationX > 50f)
        {
            rotationX = 50f;
        }
        if (rotationX < -55f)
        {
            rotationX = -55f;
        }

        /*if (rotationY > limitY)
        {
            rotationY = limitY;
        }
        if (rotationY < -limitY)
        {
            rotationY = -limitY;
        }
        */

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0f);
        //Debug.Log("transform.rotation" + transform.rotation);
        //Debug.Log("transform.rotation.eulerAngles" + transform.rotation.eulerAngles);
        //오일러값이 들어가야 정확히 내가원하는값을 출력
        // 마우스를기준으로 메인카메라 시점 이동
    }
}
