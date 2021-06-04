using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager _instance = null;
    public static ScoreManager InsTance()
    {
        return _instance;
    }

    int _bestScore = 0;
    int _myScore = 0;

    public int bestScore
    {
        get
        {
            return _bestScore;
        }
    }

    public int myScore
    {
        get
        {
            return _myScore;
            //최종값을 리턴
        }
        set
        {
            _myScore = value;
            // 벨류 를 가져옴
            if(_myScore > _bestScore)
            //벨류가 베스트 스코어보다 크면
            {
                _bestScore = _myScore;
                // 베스트스코어 는 벨류
                SaveBestScore();
            }
        }
    }
    void Start()
    {
        if (_instance == null)
            _instance = this;

        LoadBestScore();
    }

    void Update()
    {
        if (_instance == null)
            _instance = this;
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
        // 문자열 BestScore 에 변수값 _bestScore 를저장
        //PlayerPrefs int , float , string 만 가져올수있음
    }

    void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
    }
}
