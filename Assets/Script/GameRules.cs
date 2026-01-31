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
    public GameObject winPanel;  
    public GameObject losePanel;  
    public TextMeshProUGUI infoText; 

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateUI();
  
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void CatatLompatan()
    {
        if (isGameOver) return;

        lompatanSaatIni++;
        UpdateUI();

        if (lompatanSaatIni > batasLompatan)
        {
            Kalah(); 
        }
    }

    public void Menang()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        Debug.Log("YOU WIN!");
        if (winPanel != null) winPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void Kalah()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("GAME OVER!");
        if (losePanel != null) losePanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void ResetKeAwal()
    {
        Debug.Log("RETRY GAME");

        isGameOver = false;
        Time.timeScale = 1f; 

    
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        if (hero != null)
        {
            hero.transform.position = posisiStart.position;
            Rigidbody2D rb = hero.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        lompatanSaatIni = 0;
        if (ScoreManager.instance != null) ScoreManager.instance.ResetSkor();
        
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