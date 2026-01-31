using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB ADA untuk pindah scene

public class UIManager : MonoBehaviour
{
    [Header("Panel UI")]
    // Kita tidak butuh MainMenuPanel lagi disini karena beda scene
    public GameObject gameUIPanel;
    public GameObject pausePanel;

    void Start()
    {
        // Saat game mulai, pastikan:
        // 1. Waktu berjalan normal
        Time.timeScale = 1f; 
        
        // 2. Panel Pause mati, UI Game nyala
        if(pausePanel != null) pausePanel.SetActive(false);
        if(gameUIPanel != null) gameUIPanel.SetActive(true);
    }

    // --- FUNGSI TOMBOL PAUSE (DI DALAM GAME) ---

    public void TombolPause()
    {
        if(pausePanel != null) pausePanel.SetActive(true); // Munculkan menu pause
        Time.timeScale = 0f; // Bekukan waktu
    }

    public void TombolResume()
    {
        if(pausePanel != null) pausePanel.SetActive(false); // Hilangkan menu pause
        Time.timeScale = 1f; // Lanjutkan waktu
    }

    // --- FUNGSI TOMBOL HOME (BALIK KE MENU) ---
    
    public void TombolBackToMenu()
    {
        Debug.Log("Pindah ke Main Menu...");

        // 1. Matikan panel win/lose dari script GameRules biar rapi (Opsional tapi bagus)
        if (GameRules.instance != null)
        {
            if (GameRules.instance.winPanel != null) GameRules.instance.winPanel.SetActive(false);
            if (GameRules.instance.losePanel != null) GameRules.instance.losePanel.SetActive(false);
        }

        // 2. PENTING: Waktu harus dinormalkan dulu sebelum pindah scene
        // Kalau tidak, nanti pas main lagi waktunya masih berhenti
        Time.timeScale = 1f; 

        // 3. Pindah ke Scene "Mainmenu"
        // Pastikan nama scene di Unity sama persis (huruf besar/kecil ngaruh)
        SceneManager.LoadScene("Mainmenu"); 
    }
}