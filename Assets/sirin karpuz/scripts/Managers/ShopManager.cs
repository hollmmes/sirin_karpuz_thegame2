using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private TextMeshProUGUI skinLabelText;
    

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSOs;
    private bool[] unlockedStates;
    private const string skinButtonKey = "SkinButton_";
    private const string lastSelectedSkinKey = "LastSelectedSkin";

    [Header("Veriables")]
    private int lastSelectedSkin;

    [Header("Actions")]
    public static Action<SkinDataSO> onSkinSelected;
    private void Awake()
    {
        unlockedStates = new bool[skinDataSOs.Length];
        
    }
    void Start()
    {
        Initialize();
        LoadData();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PurchaseButtonCallBack()
    {
        unlockedStates[lastSelectedSkin] = true;

        SaveData();

        SkinButtonClickedCallBack(lastSelectedSkin);
    }
    private void Initialize()
    {
        purchaseButton.SetActive(false);
        for (int i = 0; i < skinDataSOs.Length; i++)
        {
           SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParent);

            skinButtonInstance.Configure(skinDataSOs[i].GetObjectPrefabs()[0].GetSprite());




            int j = i;
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallBack(j));
        }
    }

    private void SkinButtonClickedCallBack(int skinButtonIndex,bool shouldSaveLastSkin = true)
    {
        lastSelectedSkin = skinButtonIndex;

        for (int i = 0; i < skinButtonParent.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParent.GetChild(i).GetComponent<SkinButton>();

            if (i == skinButtonIndex)
                currentSkinButton.Select();
            else
                currentSkinButton.UnSelect();
        }

        if (IsSkinUnlocked(skinButtonIndex))
        {
            onSkinSelected?.Invoke(skinDataSOs[skinButtonIndex]);
            if (shouldSaveLastSkin)
                SaveLastSelectedSkin();
        }
            

        ManagePurchaseButtonVisibility(skinButtonIndex);

        UpdateSkinLabel(skinButtonIndex);

    }
    private void UpdateSkinLabel(int skinButtonIndex)
    {
        skinLabelText.text = skinDataSOs[skinButtonIndex].GetName();
    }
    private void ManagePurchaseButtonVisibility(int skinButtonIndex)
    {
        purchaseButton.SetActive(!IsSkinUnlocked(skinButtonIndex));
    }

    private bool IsSkinUnlocked(int skinButtonIndex)
    {
        return unlockedStates[skinButtonIndex];
    }


    private void LoadData()
    {

        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockValue = PlayerPrefs.GetInt(skinButtonKey + i);

            if (i == 0)
                unlockValue = 1;

            if (unlockValue == 1)
                unlockedStates[i] = true;
        }

        LoadLastSelectedSkin();
               
    }

    private void SaveData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockValue = unlockedStates[i] ? 1 : 0;

                PlayerPrefs.SetInt(skinButtonKey + i, unlockValue);

        }
    }

    private void LoadLastSelectedSkin()
    {
        int lastSelectedSkinIndex = PlayerPrefs.GetInt(lastSelectedSkinKey);
        SkinButtonClickedCallBack(lastSelectedSkinIndex, false);
    }
    private void SaveLastSelectedSkin()
    {
        PlayerPrefs.SetInt(lastSelectedSkinKey, lastSelectedSkin);
    }

}
