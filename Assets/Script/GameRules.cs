using UnityEngine;
using TMPro;

public class GameRules : MonoBehaviour
{
    public static GameRules instance;

    [Header("Aturan Main")]
    public int batasLompatan = 10;
    public Transform posisiStart;
    public GameObject hero;
    
    [Header("Status")]
    public int lompatanSaatIni = 0;
    public bool isGameOver = false;

    [Header("UI Popups (Drag dari Hierarchy)")]
    public GameObject winPanel;   // Masukkan 'winPanel'
    public GameObject losePanel;  // Masukkan 'losePanel'
    public TextMeshProUGUI infoText; 

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateUI();
        // Pastikan kedua panel mati saat mulai
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void CatatLompatan()
    {
        if (isGameOver) return;

        lompatanSaatIni++;
        UpdateUI();

        // Cek Kekalahan (Langkah Habis) -> Langsung Game Over
        if (lompatanSaatIni > batasLompatan)
        {
            Kalah(); // Panggil fungsi Kalah
        }
    }

    // --- FUNGSI MENANG ---
    public void Menang()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        Debug.Log("YOU WIN!");
        if (winPanel != null) winPanel.SetActive(true); // Munculkan Win Panel
        Time.timeScale = 0f; // Stop Waktu
    }

    // --- FUNGSI KALAH (Game Over) ---
    public void Kalah()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("GAME OVER!");
        if (losePanel != null) losePanel.SetActive(true); // Munculkan Lose Panel
        Time.timeScale = 0f; // Stop Waktu
    }

    // --- FUNGSI RESET / RETRY ---
    // Dipanggil oleh tombol "Retry" (Panah Melingkar)
    public void ResetKeAwal()
    {
        Debug.Log("RETRY GAME");

        isGameOver = false;
        Time.timeScale = 1f; // Jalankan waktu lagi

        // 1. Sembunyikan Semua Popup
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        // 2. Reset Posisi Hero
        if (hero != null)
        {
            hero.transform.position = posisiStart.position;
            Rigidbody2D rb = hero.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        // 3. Reset Data
        lompatanSaatIni = 0;
        if (ScoreManager.instance != null) ScoreManager.instance.ResetSkor();
        
        // 4. Reset Platform (Biar bisa diinjak lagi)
        PlatformPoint[] semuaPlatform = FindObjectsOfType<PlatformPoint>();
        foreach (PlatformPoint p in semuaPlatform)
        {
            p.ResetStatus();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (infoText != null && !isGameOver)
        {
            int sisa = batasLompatan - lompatanSaatIni;
            infoText.text = "Sisa Langkah: " + sisa;
        }
    }
}