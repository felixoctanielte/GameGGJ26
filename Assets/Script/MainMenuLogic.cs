using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuLogic : MonoBehaviour
{
    public void MulaiGame()
    {
        Debug.Log("Memulai Game...");
        SceneManager.LoadScene("SampleScene");
    }

  
    public void KeluarGame()
    {
        Debug.Log("Keluar dari Aplikasi!");
        Application.Quit();
    }
}