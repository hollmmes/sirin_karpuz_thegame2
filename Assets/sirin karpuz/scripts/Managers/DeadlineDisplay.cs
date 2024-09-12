using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlineDisplay : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GameStateChangedCallback(GameState gameState)
    {

     if(gameState == GameState.Game)
            StartCheckingForNearbyFruits();
     else
            StopCheckingForNearbyFruits();       
    }

    private void StartCheckingForNearbyFruits()
    {
        StartCoroutine(CheckForNearbyFruitsCoroutine());
    } 
    private void StopCheckingForNearbyFruits()
    {
        HideDeadLine();
        StopCoroutine(CheckForNearbyFruitsCoroutine());
    }

    IEnumerator CheckForNearbyFruitsCoroutine()
    {

        while (true)
        {

            bool foundNearbyFruit = false;

            for (int i = 0; i < fruitsParent.childCount; i++)
            {

                if(!fruitsParent.GetChild(i).GetComponent<Fruit>().HasCollided())
                    continue;

                float distance = Mathf.Abs(fruitsParent.GetChild(i).position.y - deadLine.transform.position.y);

                if (distance <= 1)
                {
                    ShowDeadLine();
                    foundNearbyFruit = true;
                    break;
                }
            }

            if (!foundNearbyFruit)
                HideDeadLine();

            yield return new WaitForSeconds(1);
        }

       
    }
    private void ShowDeadLine() => deadLine.SetActive(true);
    private void HideDeadLine() => deadLine.SetActive(false);
       

}
