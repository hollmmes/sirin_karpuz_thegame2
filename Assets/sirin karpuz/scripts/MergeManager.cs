using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void CollisionBetweenFruitsCallback(Fruit sender)
    {
        Debug.Log("auuuu  "+sender.name);
    }
}
