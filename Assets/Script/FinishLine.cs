using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Gunakan OnCollisionEnter2D karena Ground_Finish adalah benda padat
    void OnCollisionEnter2D(Collision2D collision)
    {
        CekKemenangan(collision.gameObject);
    }

    // Jaga-jaga kalau kamu setting Collidernya sebagai 'Is Trigger'
    void OnTriggerEnter2D(Collider2D other)
    {
        CekKemenangan(other.gameObject);
    }

    void CekKemenangan(GameObject player)
    {
        // Cek apakah yang mendarat adalah Hero
        if (player.CompareTag("Hero"))
        {
            if (GameRules.instance != null)
            {
                // AMBIL DATA DARI GAMERULES
                int langkahSekarang = GameRules.instance.lompatanSaatIni;
                int targetLangkah = GameRules.instance.batasLompatan; // Biasanya 10

                // LOGIKA PENGECEKAN
                // Jika langkah BENAR-BENAR 10 (atau sesuai batas)
                if (langkahSekarang == targetLangkah)
                {
                    Debug.Log("SUKSES! Langkah pas: " + langkahSekarang);
                    GameRules.instance.Menang();
                }
                else
                {
                    // Jika langkah kurang dari 10 (atau tidak sama)
                    Debug.Log("GAGAL! Langkah kamu baru: " + langkahSekarang + ". Harusnya: " + targetLangkah);
                    
                    // Hukum player dengan Reset
                    GameRules.instance.ResetKeAwal();
                }
            }
        }
    }
}