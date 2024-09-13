using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject ShopPanel;




    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }




    void Start()


    {
       // SetMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GameStateChangedCallback(GameState gameState)
    {

        switch (gameState) { 
        
        case GameState.Menu:
            SetMenu();
            break;


        case GameState.Game:
            SetGame(); 
            break;

        case GameState.Gameover:
            SetGameover();
            break;


        }




    }
    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        settingsPanel.SetActive(false);
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
        settingsPanel.SetActive(false);

    }


    public void PlayButtonCallBack()
    {
        GameManager.Instance.SetGameState();
        SetGame();
    }
    public void NextButtonCallBack()
    {
        SceneManager.LoadScene(0);

    }
    public void SettingsButtonCallBack() 
    { 
     
        settingsPanel.SetActive(true);
    
    }

    public void CloseSettingsButtonCallBack()
    {

        settingsPanel.SetActive(false);

    }
    public void ShowButtonCallBack() => ShopPanel.SetActive(true);
    public void CloseButtonCallBack() => ShopPanel.SetActive(false);

}
