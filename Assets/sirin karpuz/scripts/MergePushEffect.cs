using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePushEffect : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float pushRadius;
    [SerializeField] private float pushMagnitude;
    private Vector2 pushPosition;


    [Header("Debug")]
    [SerializeField] private bool enableGizmos;
    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallBack;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallBack;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MergeProcessedCallBack(FruitType fruitType, Vector2 mergePos)
    {
        pushPosition = mergePos;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mergePos, pushRadius);

        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Fruit fruit))
            {
                Vector2 force = ((Vector2)fruit.transform.position - mergePos).normalized;
                force *= pushMagnitude;


                fruit.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pushPosition, pushRadius);

    }

}

#endif