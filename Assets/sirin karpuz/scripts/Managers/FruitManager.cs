using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField] private Fruit[] spawnableFruits;
    [SerializeField] private Transform fruitsParent;
    [SerializeField ] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;


    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPos;
    [SerializeField] private float spawnDelay;
    private bool canControl;
    private bool isControlling;

    [Header("Next fruit settins")]
    private int nextFruitIndex;


    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    [Header("Actions")]
    public static Action onNextFruitIndexSet;


    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallBack;
    }



    void Start()
    {
        SetNextFruitIndex();
        
        canControl = true;
        HideLine();
    }

    // Update is called once per frame
    void Update()

    {
        if (canControl) 
        ManagePlayerInput();


        
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDownCallback();

        else if (Input.GetMouseButton(0) ) 
        { 
            if (isControlling)
                MouseDragCallback(); 
            else
                MouseDownCallback();
            
        }

        else if (Input.GetMouseButtonUp(0) && isControlling)
            MouseUpCallback();
    }



    private void MouseDownCallback()
    {


        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();

        isControlling = true;

    }
    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();

        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitsYSpawnPos));


    }





    private void MouseUpCallback()
    {
        HideLine();
        if (currentFruit != null)
             currentFruit.EnablePhysics();

        canControl = false;
        StartControlTimer();
        isControlling = false;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Fruit fruitToInstantiate = spawnableFruits[nextFruitIndex];

        currentFruit = Instantiate(
            fruitToInstantiate,
            spawnPosition, 
            Quaternion.identity, 
            fruitsParent);

        SetNextFruitIndex();




        currentFruit.name = "Fruit_" + UnityEngine.Random.Range(0, 1000);
    }

    private void SetNextFruitIndex()
    {
        nextFruitIndex = UnityEngine.Random.Range(0, spawnableFruits.Length);
        onNextFruitIndexSet?.Invoke();
    }
    public string GetNextFruitName()
    {
        return spawnableFruits[nextFruitIndex].name;
    }
    public Sprite GetNextFruitSprite()
    {
        return spawnableFruits[nextFruitIndex].GetSprite();
    }


    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedposition = GetClickedWorldPosition();

        worldClickedposition.y = fruitsYSpawnPos;
        return worldClickedposition;
    }


    private void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);


    }






    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }

   
    private void StartControlTimer()
    {
        Invoke("StopControlTimer", spawnDelay);
    }
    private void StopControlTimer()
    {
        canControl = true;
    }
    private void MergeProcessedCallBack(FruitType fruitType, Vector2 spawnPosition)
    {

        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType()== fruitType)
               { 
                SpawnMergedFruit(fruitPrefabs[i], spawnPosition);
                break;
            }

        }


    }

    private void SpawnMergedFruit (Fruit fruit, Vector2 spawnPosition)
    {
       Fruit fruitInstance= Instantiate(fruit, spawnPosition, Quaternion.identity,fruitsParent);
        fruitInstance.EnablePhysics();
    }













#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-5, fruitsYSpawnPos, 0), new Vector3(5, fruitsYSpawnPos, 0));

    }
#endif
}
