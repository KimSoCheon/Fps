using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterMove : MonoBehaviour
{
    public Transform cameraTransform;

    public float moveSpeed = 10f; // �̵� ���ǵ� 10f
    public float jumpSpeed = 10f;// ���� ���ǵ� 10f
    public float gravity = -20f;// �߷� -20f
    public float yVelocity = 0; // y���� �ӵ� 0

    public CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // ĳ���� ��Ʈ�ѷ� �� CharacterController ���۳�Ʈ�� ����
    }

    void Update()
    {
        // ���콺 ��ġ ��������( h , v )�� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // moveDirection �� new Vector3�� ��ġ���� �� ����
        Vector3 moveDirection = new Vector3(h, 0, v);

        // moveDirection �� ����ī�޶��� ��ġ���� ���Ⱚ(moveDirection)
        moveDirection = cameraTransform.TransformDirection(moveDirection);

        // moveDirection �� moveSpeed�� ���Ѱ�
        moveDirection *= moveSpeed;

        // ���� ���ٴڿ� �پ��ִٸ�
        if (characterController.isGrounded)
        {
            // yVelocity �� 0�ε�
            yVelocity = 0;
            //���� �����̽� Ű�� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity �� jumpSpeed ������ ����
                yVelocity = jumpSpeed;
            }
        }

        // y���ν�Ƽ �� �׶��Ƽ �� Time.deltaTime ���Ѱ���
        yVelocity += (gravity * Time.deltaTime);
       
        // moveDirectoin.y �� y���ν�Ƽ �� ����
        moveDirection.y = yVelocity;

        //ĳ���� ��Ʈ�ѷ��� �̵��� ����𷢼� �� Time.deltaTime �� ���Ѱ��� ���
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
