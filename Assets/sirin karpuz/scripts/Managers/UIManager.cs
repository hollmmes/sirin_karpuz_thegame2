using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    void Start()
    {
        SetMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }  
    private void SetGame()
    {
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
        gameoverPanel.SetActive(false);
    } 
    private void SetGameover()
    {
        gameoverPanel.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        
    }



}
