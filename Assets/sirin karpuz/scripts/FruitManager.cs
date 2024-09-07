using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField ] private LineRenderer fruitSpawnLine;


    [Header("Settings")]
    [SerializeField] private float fruitsYSpawnPos;


    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    void Start()
    {
        HideLine();



    }

    // Update is called once per frame
    void Update()

    {
        ManagePlayerInput();


        
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDownCallback();

        else if (Input.GetMouseButton(0)) // Mouse drag event
            MouseDragCallback();

        else if (Input.GetMouseButtonUp(0))
            MouseUpCallback();
    }



    private void MouseDownCallback()
    {


        DisplayLine();
        PlaceLineAtClickedPosition();


    }
    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();
    }





    private void MouseUpCallback()
    {
        HideLine();
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
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
