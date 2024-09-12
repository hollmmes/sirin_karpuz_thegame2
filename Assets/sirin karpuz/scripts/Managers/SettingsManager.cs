using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private GameObject resetProgressPrompt;
    [SerializeField] private Toggle sfxToggle;
    [Header(" Actions")]
    public static Action<bool> onSFXValueChanged;

    [Header("Data")]
    private const string sfxActiveKey = "sfxActiveKey";
    void Start()
    {
        ToggleCallback(sfxToggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetProgressButtonCallBack()
    {
        resetProgressPrompt.SetActive(true);
    }

    public void ToggleCallback(bool sfxActive)
    {
        onSFXValueChanged?.Invoke(sfxActive);
        SaveData();
    }

    public void ResetProgressYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void ResetProgressNo()
    {
        resetProgressPrompt.SetActive(false);
    }
    private void LoadData()
    {
        sfxToggle.isOn =  PlayerPrefs.GetInt(sfxActiveKey) == 1;
    }   
    private void SaveData()
    {
      

        PlayerPrefs.SetInt(sfxActiveKey, sfxToggle.isOn ? 1 : 0);
    }

}
