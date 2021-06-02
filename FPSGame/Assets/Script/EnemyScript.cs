using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum ENEMYSTATE
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD
    }
    public ENEMYSTATE enemyState = ENEMYSTATE.IDLE;
    public float stateTime;
    public float idleStateTime = 2f;
    public Animator enemyAnim;
    public Transform target;

    public float speed = 0.5f;
    public float rotationSpeed = 10f;
    public float attackRange = 2.5f;
    public float attackStateMaxTime = 1.024f;
    public CharacterController enemyCharacterController;
    //애너미 스테이트를 IDLE로 고정 (enemyState 변수)
    public int hp = 5;
    void Start()
    {
        enemyState = ENEMYSTATE.IDLE;
        target = GameObject.Find("Player").transform;
        enemyCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        switch (enemyState)
        //탭키 한번 스위치문 자동설정
        //()안에 enemyState 변수 넣고 엔터 시 enum에서 선언한 상태 자동 케이스화 
        {
            case ENEMYSTATE.IDLE:
                stateTime += Time.deltaTime;
                if(stateTime > idleStateTime)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                    enemyAnim.SetTrigger("WALK");
                    enemyAnim.ResetTrigger("IDLE");
                }
                break;
            case ENEMYSTATE.MOVE:
                //float distance = (target.position - transform.position).magnitude;
                //Vector3 에서 Vector3를 float 으로 빼는게 불가능해서 매그너트 를이용해서 뺌
                // 계산식으로 선언한것 거리 갱신
                float distance = Vector3.Distance(target.position, transform.position);
                //함수로 선언한것
                
                if(distance < attackRange)
                    // 사거리 안에 들어왔다면
                {
                    enemyState = ENEMYSTATE.ATTACK;
                    stateTime = attackStateMaxTime;
                }
                else
                {
                    Vector3 dir = target.position - transform.position;
                    //백터3 dir 값을 백터쓰리 포지션 값끼리 뺌
                    
                    Debug.DrawLine(target.position, transform.position, Color.red);
                    //DrawLine 타겟의 포지션에서 지정한 포지션과 컬러값을 그림
                    //빨간선 이 플레이어와 에너미와 의 거리Debug.Log
                    
                    //Debug.Log(dir);
                    
                    dir.y = 0;
                    dir.Normalize();
                    //노멀라이즈 = 정규화
                    //단순화 시켜서 방향만 구하려고 씀
                    
                    Debug.Log(dir);
                    
                    enemyCharacterController.SimpleMove(dir * speed);
                    //simpleMove 는 적들한태 많이씀 단순 이동
                    
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
                    //에너미가 플레이어에 다가올때 (러프 란(Lerp)선형보간은 지정한 물체가 물체한태 곡선으로 이동할때 곡선으로 바뀔수있게 쿼터니언(Quaternion).러프 를 씀 )

                }
                break;
            case ENEMYSTATE.ATTACK:
                stateTime += Time.deltaTime;
                if(stateTime > attackStateMaxTime)
                    //공격과 공격 타임
                {
                    stateTime = 0;
                    enemyAnim.SetTrigger("ATTACK");
                    enemyAnim.ResetTrigger("WALK");
                }
                float dist = Vector3.Distance(target.position , transform.position);
                if(dist > attackRange)
                {
                    enemyState = ENEMYSTATE.MOVE;
                    enemyAnim.SetTrigger("WALK");
                    enemyAnim.ResetTrigger("ATTACK");

                }
                break;
            case ENEMYSTATE.DAMAGE:
                enemyAnim.SetTrigger("DAMAGED");
                stateTime += Time.deltaTime;
                if(stateTime > idleStateTime)
                {
                    stateTime = 0;
                    --hp;
                    enemyState = ENEMYSTATE.IDLE;
                    enemyAnim.SetTrigger("IDLE");
                    enemyAnim.ResetTrigger("DAMAGED");
                }
                if (hp <= 0)
                {
                    enemyState = ENEMYSTATE.DEAD;
                    enemyAnim.SetTrigger("DEAD");
                    enemyAnim.ResetTrigger("IDLE");
                    enemyAnim.ResetTrigger("WALK");
                    enemyAnim.ResetTrigger("DAMAGED");
                    stateTime = 0;
                }
                    break;
                case ENEMYSTATE.DEAD:
                stateTime += Time.deltaTime;
                AnimatorClipInfo [] animationInfo;
                animationInfo = enemyAnim.GetCurrentAnimatorClipInfo(0);
                //if (stateTime > animationInfo[0].clip.length)
                if (stateTime > 1.024f)
                    Destroy(gameObject);

                    break;
                default:
                    break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            enemyState = ENEMYSTATE.DAMAGE;
        }
    }
}
