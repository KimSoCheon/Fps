using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomManager : MonoBehaviour
{
    public List<string> numbers;
    public Text numberText;
    public string[] results;
    public int rnd;
    public int cnt;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void RandomPick()
    {
        for (int i = 0; i < results.Length; i++)
        {
            rnd = Random.Range(0, numbers.Count);
            numberText.text = numbers[rnd];
            results[i] = numbers[rnd];
            numbers.RemoveAt(rnd);
            // (rnd) Áö¿ò
        }
    }

    public void SelctedName()
    {
        if (cnt < numbers.Count)
        {
            results[cnt] = numbers[rnd];
        }
    }
}
