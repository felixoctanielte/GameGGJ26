using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TambahSkor(int nilai)
    {
        score += nilai;
        UpdateTampilan();
    }

    // --- TAMBAHAN BARU: FUNGSI RESET SKOR ---
    public void ResetSkor()
    {
        score = 0; // Kembalikan ke 0
        UpdateTampilan(); // Update teks di layar
    }
    // ----------------------------------------

    void UpdateTampilan()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}