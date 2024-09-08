using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fruit : MonoBehaviour
{

    [Header("Actions")]
    public static Action<Fruit> onCollisionWithFruit;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Fruit fruit))
        {

            onCollisionWithFruit?.Invoke(this);

        }

    }



}
