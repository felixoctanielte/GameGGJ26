using UnityEngine;

public class PlatformPoint : MonoBehaviour
{
    private bool sudahDihitung = false; // Ini kuncinya!
    public int nilaiPoin = 10; // Misal nilai default 10

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah yang nabrak adalah Hero
        if (collision.gameObject.CompareTag("Hero"))
        {
            // Kita cek dulu: Apakah platform ini SUDAH DIHITUNG?
            if (!sudahDihitung)
            {
                // 1. Tambah Skor
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.TambahSkor(nilaiPoin);
                }

                // 2. Tambah Hitungan Lompatan
                if (GameRules.instance != null)
                {
                    GameRules.instance.CatatLompatan();
                }

                // Kunci platform ini biar tidak bisa dihitung lagi
                sudahDihitung = true; 
                
                Debug.Log("Pijakan Baru! Skor nambah.");
            }
        }
    }

    // --- TAMBAHAN PENTING: FUNGSI RESET ---
    // Fungsi ini akan dipanggil oleh GameRules saat kamu kalah
    public void ResetStatus()
    {
        sudahDihitung = false; // Buka kuncinya lagi
        Debug.Log("Platform di-reset, siap diinjak lagi.");
    }
}