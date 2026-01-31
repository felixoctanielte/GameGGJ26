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

    public void ResetSkor()
    {
        score = 0; 
        UpdateTampilan(); 
    }
    

   void UpdateTampilan()
{
    if (scoreText != null)
    {
        scoreText.text = "Score Platform : " + score.ToString() + " / " + targetScore.ToString();
    }
}
}