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
    //�ֳʹ� ������Ʈ�� IDLE�� ���� (enemyState ����)
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
        //��Ű �ѹ� ����ġ�� �ڵ�����
        //()�ȿ� enemyState ���� �ְ� ���� �� enum���� ������ ���� �ڵ� ���̽�ȭ 
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
                //Vector3 ���� Vector3�� float ���� ���°� �Ұ����ؼ� �ű׳�Ʈ ���̿��ؼ� ��
                // �������� �����Ѱ� �Ÿ� ����
                float distance = Vector3.Distance(target.position, transform.position);
                //�Լ��� �����Ѱ�
                //Debug.Log("���� �÷��̾�� ������ �Ÿ��� ::::" + distance);
                if(distance < attackRange)
                    // ��Ÿ� �ȿ� ���Դٸ�
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
                    //����3 dir ���� ���;��� ������ ������ ��
                    
                    //Debug.DrawLine(target.position, transform.position, Color.red);
                    //DrawLine Ÿ���� �����ǿ��� ������ �����ǰ� �÷����� �׸�
                    //������ �� �÷��̾�� ���ʹ̿� �� �Ÿ�Debug.Log
                    
                    //Debug.Log(dir);
                    
                    dir.y = 0;
                    dir.Normalize();
                    //��ֶ����� = ����ȭ
                    //�ܼ�ȭ ���Ѽ� ���⸸ ���Ϸ��� ��
                    
                    //Debug.Log(dir);
                    
                    enemyCharacterController.SimpleMove(dir * speed);
                    //simpleMove �� �������� ���̾� �ܼ� �̵�
                    
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
                    //���ʹ̰� �÷��̾ �ٰ��ö� (���� ��(Lerp)���������� ������ ��ü�� ��ü���� ����� �̵��Ҷ� ����� �ٲ���ְ� ���ʹϾ�(Quaternion).���� �� �� )
                    enemyAnim.SetBool("WALK", true);
                }
                break;
            case ENEMYSTATE.ATTACK:
                stateTime += Time.deltaTime;
                if(stateTime > attackStateMaxTime)
                    //���ݰ� ���� Ÿ��
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
    //���� Init �� �̴ϼȶ����� �ʱ�ȭ�ϴ�
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
