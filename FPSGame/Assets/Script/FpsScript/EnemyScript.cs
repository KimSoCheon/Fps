using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum ENEMYSTATE
    {
        NONE = -1,
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
    public bool isAttack = false;
    public PlayerState playerState;
    public AudioClip zombieDeadSound;
    void Start()
    {
        enemyState = ENEMYSTATE.IDLE;
        target = GameObject.Find("Player").transform;
        enemyCharacterController = GetComponent<CharacterController>();
        playerState = target.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (playerState.isDead)
        {
            enemyAnim.enabled = false;
            return;
        }


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
                    enemyAnim.SetBool("WALK", true);
                    enemyAnim.SetBool("IDLE", false);
                }
                break;
            case ENEMYSTATE.MOVE:
                //float distance = (target.position - transform.position).magnitude;
                //Vector3 에서 Vector3를 float 으로 빼는게 불가능해서 매그너트 를이용해서 뺌
                // 계산식으로 선언한것 거리 갱신
                float distance = Vector3.Distance(target.position, transform.position);
                //함수로 선언한것
                //Debug.Log("현재 플레이어와 나와의 거리는 ::::" + distance);
                if(distance < attackRange)
                    // 사거리 안에 들어왔다면
                {
                    enemyState = ENEMYSTATE.ATTACK;
                    stateTime = attackStateMaxTime;
                    enemyAnim.SetBool("WALK", false);
                    enemyAnim.SetBool("ATTACK", true);
                    stateTime = 0;
                }
                else
                {
                    Vector3 dir = target.position - transform.position;
                    //백터3 dir 값을 백터쓰리 포지션 값끼리 뺌
                    
                    //Debug.DrawLine(target.position, transform.position, Color.red);
                    //DrawLine 타겟의 포지션에서 지정한 포지션과 컬러값을 그림
                    //빨간선 이 플레이어와 에너미와 의 거리Debug.Log
                    
                    //Debug.Log(dir);
                    
                    dir.y = 0;
                    dir.Normalize();
                    //노멀라이즈 = 정규화
                    //단순화 시켜서 방향만 구하려고 씀
                    
                    //Debug.Log(dir);
                    
                    enemyCharacterController.SimpleMove(dir * speed);
                    //simpleMove 는 적들한태 많이씀 단순 이동
                    
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
                    //에너미가 플레이어에 다가올때 (러프 란(Lerp)선형보간은 지정한 물체가 물체한태 곡선으로 이동할때 곡선으로 바뀔수있게 쿼터니언(Quaternion).러프 를 씀 )
                    enemyAnim.SetBool("WALK", true);
                }
                break;
            case ENEMYSTATE.ATTACK:
                stateTime += Time.deltaTime;
                if(stateTime > attackStateMaxTime)
                    //공격과 공격 타임
                {
                    stateTime = 0;
                    target.GetComponent<PlayerState>().DamageByEnemy();
                }
                float dist = Vector3.Distance(target.position , transform.position);
                if(dist > attackRange)
                {
                    enemyState = ENEMYSTATE.MOVE;
                    enemyAnim.SetBool("ATTACK", false);

                }
                break;
            case ENEMYSTATE.DAMAGE:
                stateTime += Time.deltaTime;
                enemyAnim.SetBool("WALK", false);
                enemyAnim.SetBool("ATTACK", false);
                if (stateTime > 1f)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                    stateTime = 0;
                    enemyAnim.SetBool("WALK", true);
                    enemyAnim.SetBool("DAMAGED", false);
                }

                if (hp <= 0)
                {
                    AudioManager.Instance().PlaySfx(zombieDeadSound);
                    ScoreManager.InsTance().myScore += 10;
                    enemyState = ENEMYSTATE.DEAD;
                }
                    break;
                case ENEMYSTATE.DEAD:
                enemyAnim.SetBool("WALK", false);
                enemyAnim.SetBool("DAMAGED", false);
                enemyAnim.SetBool("DEAD", true);
                enemyCharacterController.enabled = false;
                AnimatorClipInfo [] animatorClipInfo;
                animatorClipInfo = enemyAnim.GetCurrentAnimatorClipInfo(0);
                StartCoroutine(DeadProcess(3f));
                //if (stateTime > animationInfo[0].clip.length)
                //Destroy(gameObject, 3f);
                    break;
                default:
                    break;
        }
    }

    IEnumerator DeadProcess(float t)
    {
        //enemyState = ENEMYSTATE.NONE;
        yield return new WaitForSeconds(t);
        while (transform.position.y > -t)
        {
            Vector3 temp = transform.position;
            temp.y -= 0.01f * Time.deltaTime;
            transform.position = temp;
            yield return new WaitForEndOfFrame();
        }

        //Destroy(gameObject);
        InitEnemy();
        gameObject.SetActive(false);
    }
    //약자 Init 은 이니셜라이즈 초기화하다
    void InitEnemy()
    {
        hp = 5;
        enemyState = ENEMYSTATE.IDLE;
        enemyCharacterController.enabled = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            --hp;
            enemyState = ENEMYSTATE.DAMAGE;
            enemyAnim.SetBool("DAMAGED", true);
        }
    }
}
