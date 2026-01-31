using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;
    [Header("Score Settings")]
public int targetScore = 10; 
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
        // Hasilnya nanti: "Score Platform : 0 / 10"
        scoreText.text = "Score Platform : " + score.ToString() + " / " + targetScore.ToString();
    }
}
}