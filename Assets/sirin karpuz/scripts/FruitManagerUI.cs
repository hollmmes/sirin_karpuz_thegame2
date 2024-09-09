using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(FruitManager))]
public class FruitManagerUI : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Image nextFruitImage;
    [SerializeField] private TextMeshProUGUI nextFruitText;
    private FruitManager fruitManager;


    private void Awake()
    {
        fruitManager = GetComponent<FruitManager>();
        FruitManager.onNextFruitIndexSet += UpdateNextFruitImage;
    }

    void Start()
    {
        
    }


    private void UpdateNextFruitImage()
    {
        if(fruitManager == null) 
            fruitManager = GetComponent<FruitManager>();

        nextFruitImage.sprite = fruitManager.GetNextFruitSprite();
    }
}