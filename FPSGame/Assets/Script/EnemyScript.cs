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
    //�ֳʹ� ������Ʈ�� IDLE�� ���� (enemyState ����)
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
        //��Ű �ѹ� ����ġ�� �ڵ�����
        //()�ȿ� enemyState ���� �ְ� ���� �� enum���� ������ ���� �ڵ� ���̽�ȭ 
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
                //Vector3 ���� Vector3�� float ���� ���°� �Ұ����ؼ� �ű׳�Ʈ ���̿��ؼ� ��
                // �������� �����Ѱ� �Ÿ� ����
                float distance = Vector3.Distance(target.position, transform.position);
                //�Լ��� �����Ѱ�
                
                if(distance < attackRange)
                    // ��Ÿ� �ȿ� ���Դٸ�
                {
                    enemyState = ENEMYSTATE.ATTACK;
                    stateTime = attackStateMaxTime;
                }
                else
                {
                    Vector3 dir = target.position - transform.position;
                    //����3 dir ���� ���;��� ������ ������ ��
                    
                    Debug.DrawLine(target.position, transform.position, Color.red);
                    //DrawLine Ÿ���� �����ǿ��� ������ �����ǰ� �÷����� �׸�
                    //������ �� �÷��̾�� ���ʹ̿� �� �Ÿ�Debug.Log
                    
                    //Debug.Log(dir);
                    
                    dir.y = 0;
                    dir.Normalize();
                    //��ֶ����� = ����ȭ
                    //�ܼ�ȭ ���Ѽ� ���⸸ ���Ϸ��� ��
                    
                    Debug.Log(dir);
                    
                    enemyCharacterController.SimpleMove(dir * speed);
                    //simpleMove �� �������� ���̾� �ܼ� �̵�
                    
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
                    //���ʹ̰� �÷��̾ �ٰ��ö� (���� ��(Lerp)���������� ������ ��ü�� ��ü���� ����� �̵��Ҷ� ����� �ٲ���ְ� ���ʹϾ�(Quaternion).���� �� �� )

                }
                break;
            case ENEMYSTATE.ATTACK:
                stateTime += Time.deltaTime;
                if(stateTime > attackStateMaxTime)
                    //���ݰ� ���� Ÿ��
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
