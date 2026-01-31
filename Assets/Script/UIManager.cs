using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject gameUIPanel;
    public GameObject pausePanel;

    [Header("Referensi Lain")]
    public GameObject hero; // Masukkan Hero kamu disini

    void Start()
    {
        // Saat game pertama kali dibuka, langsung masuk ke Menu Utama
        BukaMainMenu();
    }

    // --- FUNGSI TOMBOL MAIN MENU ---

    public void TombolPlay()
    {
        // 1. Matikan panel menu, nyalakan UI game
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        pausePanel.SetActive(false);

        // 2. Jalankan Waktu
        Time.timeScale = 1f;

        // 3. Reset Game ke kondisi awal (Panggil script GameRules yang sudah kita buat)
        if (GameRules.instance != null)
        {
            GameRules.instance.ResetKeAwal(); 
        }
    }

    public void TombolHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true); // Tampilkan instruksi
    }

    public void TombolBackFromHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true); // Balik ke menu
    }

    public void TombolExit()
    {
        Application.Quit();
        Debug.Log("Keluar Game");
    }

    // --- FUNGSI TOMBOL DI DALAM GAME ---

    public void TombolPause()
    {
        pausePanel.SetActive(true); // Munculkan menu pause
        Time.timeScale = 0f; // Bekukan waktu (game berhenti)
    }

    public void TombolResume()
    {
        pausePanel.SetActive(false); // Hilangkan menu pause
        Time.timeScale = 1f; // Lanjutkan waktu
    }

    public void TombolBackToMenu()
    {
        // Balik ke logika Main Menu
        BukaMainMenu();
    }

    // --- LOGIKA UTAMA ---
    void BukaMainMenu()
    {
        // Aktifkan Panel Menu, Matikan yang lain
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        gameUIPanel.SetActive(false);
        pausePanel.SetActive(false);

        // PENTING: Waktu harus berhenti saat di Main Menu
        Time.timeScale = 0f; 
    }
}