using UnityEngine;

public class PlatformPoint : MonoBehaviour
{
    private bool sudahDihitung = false;
    public int nilaiPoin = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Cek apakah yang nabrak adalah Hero
        if (collision.gameObject.CompareTag("Hero"))
        {
            // 2. LOGIKA BARU: Cek Arah Datang
            // Kita cek titik kontak tumbukan. 
            // Kalau titik tumbukan ada di ATAS pusat platform, berarti diinjak dari atas.
            // normal.y < -0.5f artinya Hero menekan ke bawah (mendarat).
            
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Jika arah tumbukan mengarah ke bawah (Hero mendarat di atas platform)
                if (contact.normal.y < -0.5f) 
                {
                    ProsesPijakan();
                    break; // Cukup satu titik kontak yang valid
                }
            }
        }
    }

    void ProsesPijakan()
    {
        // Cek apakah sudah dihitung sebelumnya
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