using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    [Header("Timer")]
    [SerializeField] private float durationThreshold;
    private float timer;
    private bool timerOn;
    private bool isGameOver;


    void Start()
    {
        
    }

    
    void Update()
    {
        if(!isGameOver)
            ManageGameOver();



    }
    private void ManageGameOver()
    {
        if (timerOn)
        {
            ManageTimerOn();

        }
        else
        {
            if (isFruitAboveLine())
                startTimer();
        }
    }
    private bool isFruitAboveLine()
    {
        for (int i = 0; i < fruitsParent.childCount; i++)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
                continue;

         if (isFruitAboveLine(fruitsParent.GetChild(i)))
            return true;
        }
        return false;
    }


    private void ManageTimerOn()
    {
        timer += Time.deltaTime;

        if (!isFruitAboveLine())
            stopTimer();


        if (timer >= durationThreshold)
             Gameover();
        
    }
    private bool isFruitAboveLine(Transform fruit)
    {
        if (fruit.position.y > deadLine.transform.position.y)
           return true;

        return false;
            
    }
    private void startTimer()
    {
        timer = 0;
        timerOn = true;
    }
    private void stopTimer()
    {
        timerOn = false;    
    }
    private void Gameover()
    {
        Debug.LogError("Oyun Bitti");
        isGameOver = true;
    }
}
