using UnityEngine;
using UnityEngine.SceneManagement; 

public class UIManager : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject gameUIPanel;
    public GameObject pausePanel;

    void Start()
    {
        Time.timeScale = 1f; 
        if(pausePanel != null) pausePanel.SetActive(false);
        if(gameUIPanel != null) gameUIPanel.SetActive(true);
    }


    public void TombolPause()
    {
        if(pausePanel != null) pausePanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void TombolResume()
    {
        if(pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void TombolBackToMenu()
    {
        Debug.Log("Pindah ke Main Menu...");

        if (GameRules.instance != null)
        {
            if (GameRules.instance.winPanel != null) GameRules.instance.winPanel.SetActive(false);
            if (GameRules.instance.losePanel != null) GameRules.instance.losePanel.SetActive(false);
        }

        Time.timeScale = 1f; 
        SceneManager.LoadScene("Mainmenu"); 
    }
}