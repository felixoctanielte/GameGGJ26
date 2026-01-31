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

    [Header("UI")]
    public TextMeshProUGUI infoText;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    // Fungsi ini dipanggil DARI PlatformPoint
    public void CatatLompatan()
    {
        if (isGameOver) return;

        lompatanSaatIni++;
        UpdateUI();

        // Cek Kekalahan
        if (lompatanSaatIni > batasLompatan)
        {
            ResetKeAwal();
        }
    }

    public void ResetKeAwal()
    {
        Debug.Log("KALAH! Reset semua sistem.");
        
        // 1. Reset Posisi Hero
        hero.transform.position = posisiStart.position;
        Rigidbody2D rb = hero.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero; // Stop gerak hero

        // 2. Reset Hitungan Lompatan
        lompatanSaatIni = 0;

        // 3. Reset Skor (Balik ke 0)
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetSkor();
        }

        // 4. [INI YANG KAMU MINTA] RESET SEMUA PLATFORM
        // Cari semua object yang pake script PlatformPoint
        PlatformPoint[] semuaPlatform = FindObjectsOfType<PlatformPoint>();
        
        // Perintah satu per satu untuk reset
        foreach (PlatformPoint p in semuaPlatform)
        {
            p.ResetStatus(); // Panggil fungsi reset di script PlatformPoint
        }

        UpdateUI();
    }

    public void Menang()
    {
        isGameOver = true;
        if (infoText != null) infoText.text = "YOU WIN!";
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