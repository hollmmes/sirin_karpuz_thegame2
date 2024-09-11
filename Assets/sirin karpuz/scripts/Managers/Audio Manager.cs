using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private AudioSource mergeSource;


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
   
    public void PlayMergeSound()
    {
        mergeSource.pitch = Random.Range(0.8f, 1.3f);
        mergeSource.Play();
    }
    private void MergeProcessedCallBack(FruitType fruitType, Vector2 mergePos)
    {
        PlayMergeSound();
    }
}
