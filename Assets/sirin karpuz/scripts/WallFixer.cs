using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFixer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform LeftWall;
    void Start()
    {
        float aspectRatio = (float)Screen.height / Screen.width;
        //Debug.Log("Aspect rat; "+aspectRatio);

        Camera mainCamera = Camera.main;   

        float halfHorizontalFov = mainCamera.orthographicSize / aspectRatio;
        Debug.Log("World width"+ halfHorizontalFov);

        rightWall.transform.position = new Vector3(halfHorizontalFov+.5f,0,0);
        LeftWall.transform.position = -rightWall.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
