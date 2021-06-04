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
            //�������� ����
        }
        set
        {
            _myScore = value;
            // ���� �� ������
            if(_myScore > _bestScore)
            //������ ����Ʈ ���ھ�� ũ��
            {
                _bestScore = _myScore;
                // ����Ʈ���ھ� �� ����
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
        // ���ڿ� BestScore �� ������ _bestScore ������
        //PlayerPrefs int , float , string �� �����ü�����
    }

    void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
    }
}
