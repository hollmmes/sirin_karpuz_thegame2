using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    [SerializeField] private GameObject purchaseButton;
    

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSOs;
    private bool[] unlockedState;
    private void Awake()
    {
        unlockedState = new bool[skinDataSOs.Length];
        LoadData();
    }
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Initialize()
    {
        purchaseButton.SetActive(false);
        for (int i = 0; i < skinDataSOs.Length; i++)
        {
           SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParent);

            skinButtonInstance.Configure(skinDataSOs[i].GetObjectPrefabs()[0].GetSprite());

            if (i == 0)
                skinButtonInstance.Select();



            int j = i;
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallBack(j));
        }
    }

    private void SkinButtonClickedCallBack(int skinButtonIndex)
    {
        Debug.Log("skin button index" + skinButtonIndex);


        for (int i = 0; i < skinButtonParent.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParent.GetChild(i).GetComponent<SkinButton>();

            if (i == skinButtonIndex)
                currentSkinButton.Select();
            else
                currentSkinButton.UnSelect();
        }
        ManagePurchaseButtonVisibility(skinButtonIndex);

    }
    private void ManagePurchaseButtonVisibility(int skinButtonIndex)
    {
        purchaseButton.SetActive(!unlockedState[skinButtonIndex]);
        if (unlockedState[skinButtonIndex])
            purchaseButton.SetActive(false);
        else
            purchaseButton.SetActive(true);
    }
    private void LoadData()
    {
        for (int i = 0; i < unlockedState.Length; i++)
        {
            int unlockValue = PlayerPrefs.GetInt("SkinButton" + i);

            if (i == 0)
                unlockValue = 1;

            if (unlockValue == 1)
                unlockedState[i] = true;
        }
               
    }

}
