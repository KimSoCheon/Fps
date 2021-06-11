using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 myLocalPositoin;
    void Start()
    {
        myLocalPositoin = transform.localPosition;
        //StartCoroutine(CameraShakeProcess());
    }

    IEnumerator CameraShakeProcess(float shakeTime,float shakeSence)
    //IEnumerator = �ڷ�ƾ �� yield return �� �Լ��� �־�� ����̰�����
    
    //yield return �� �����ٶ� ������ 
    {
        float curTime = 0;
        while (curTime < shakeTime)
        {
            curTime += Time.deltaTime;
            transform.localPosition = myLocalPositoin;

            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-shakeSence, +shakeSence);
            pos.y = Random.Range(-shakeSence, +shakeSence);
            pos.z = Random.Range(-shakeSence, +shakeSence);
            transform.localPosition += pos;
            
            yield return new WaitForEndOfFrame();
            
        }
        transform.localPosition = myLocalPositoin;
        //�������� �� ���� Ʈ�������� ���������� �̵�
    }

    public void PlayCameraShake()
    {
        StartCoroutine(CameraShakeProcess(1f, 0.25f));
    }

    IEnumerator OtherCouroutine()
    {
        Debug.Log("�ٸ� �ڷ�ƾ ����::::");
        yield return new WaitForSeconds(5f);
        Debug.Log("�ٸ� �ڷ�ƾ ��::::");
    }
}
