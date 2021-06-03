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
    //IEnumerator = 코루틴 은 yield return 이 함수가 있어야 사용이가능함
    
    //yield return 는 따르다란 뜻으로 
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
        //다흔든다음 내 원래 트렌스폼의 포지션으로 이동
    }

    public void PlayCameraShake()
    {
        StartCoroutine(CameraShakeProcess(1f, 0.25f));
    }

    IEnumerator OtherCouroutine()
    {
        Debug.Log("다른 코루틴 시작::::");
        yield return new WaitForSeconds(5f);
        Debug.Log("다른 코루틴 끝::::");
    }
}
