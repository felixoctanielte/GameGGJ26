using UnityEngine;

public class PlatformPoint : MonoBehaviour
{
    private bool sudahDihitung = false;
    public int nilaiPoin = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
               
                if (contact.normal.y < -0.5f) 
                {
                    ProsesPijakan();
                    break;
                }
            }
        }
    }

    void ProsesPijakan()
    {
        if (!sudahDihitung)
        {
            // Tambah Skor
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.TambahSkor(nilaiPoin);
            }

            // Catat Lompatan
            if (GameRules.instance != null)
            {
                GameRules.instance.CatatLompatan();
            }

            // Kunci agar tidak dihitung lagi
            sudahDihitung = true;
            Debug.Log("Pijakan Valid (Dari Atas)!");
        }
    }

    public void ResetStatus()
    {
        sudahDihitung = false;
    }
}