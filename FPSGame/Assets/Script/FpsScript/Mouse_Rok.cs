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

        //ȭ�� ȸ���� ���� ����
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
        //���Ϸ����� ���� ��Ȯ�� �������ϴ°��� ���
        // ���콺���������� ����ī�޶� ���� �̵�
    }
}
