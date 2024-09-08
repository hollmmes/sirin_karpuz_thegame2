using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField ] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;


    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPos;
    private bool canControl;
    private bool isControlling;


    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    void Start()
    {
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
        currentFruit.EnablePhysics();

        canControl = false;
        StartControlTimer();
        isControlling = false;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        currentFruit = Instantiate(fruitPrefabs[Random.Range(0,fruitPrefabs.Length)], spawnPosition, Quaternion.identity);
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
        Invoke("StopControlTimer", .5f);
    }
    private void StopControlTimer()
    {
        canControl = true;
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
