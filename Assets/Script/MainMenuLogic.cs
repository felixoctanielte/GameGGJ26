using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ADA

public class MainMenuLogic : MonoBehaviour
{
    // Dipanggil oleh tombol PLAY
    public void MulaiGame()
    {
        Debug.Log("Memulai Game...");
        // Pastikan nama scene game kamu "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }

    // Dipanggil oleh tombol EXIT
    public void KeluarGame()
    {
        Debug.Log("Keluar dari Aplikasi!");
        Application.Quit();
    }
}