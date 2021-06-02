using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterMove : MonoBehaviour
{
    public Transform cameraTransform;

    public float moveSpeed = 10f; // 이동 스피드 10f
    public float jumpSpeed = 10f;// 점프 스피드 10f
    public float gravity = -20f;// 중량 -20f
    public float yVelocity = 0; // y축의 속도 0

    public CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // 캐릭터 컨트롤러 에 CharacterController 컴퍼넌트를 지정
    }

    void Update()
    {
        // 마우스 위치 정보값을( h , v )로 지정
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // moveDirection 을 new Vector3로 위치정보 값 지정
        Vector3 moveDirection = new Vector3(h, 0, v);

        // moveDirection 는 메인카메라의 위치정보 방향값(moveDirection)
        moveDirection = cameraTransform.TransformDirection(moveDirection);

        // moveDirection 과 moveSpeed를 곱한값
        moveDirection *= moveSpeed;

        // 만약 땅바닥에 붙어있다면
        if (characterController.isGrounded)
        {
            // yVelocity 는 0인데
            yVelocity = 0;
            //만약 스페이스 키를 누르면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 에 jumpSpeed 변수를 지정
                yVelocity = jumpSpeed;
            }
        }

        // y벨로시티 와 그라비티 에 Time.deltaTime 더한값을
        yVelocity += (gravity * Time.deltaTime);
       
        // moveDirectoin.y 에 y벨로시티 를 지정
        moveDirection.y = yVelocity;

        //캐릭터 컨트롤러의 이동은 무브디랙션 에 Time.deltaTime 을 더한값을 출력
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
