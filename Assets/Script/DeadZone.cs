using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // OPSI 1: Trigger (Rekomendasi)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Debug.Log("Jatuh ke Jurang!");
            MatikanPemain();
        }
    }

    // OPSI 2: Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Debug.Log("Jatuh ke Jurang!");
            MatikanPemain();
        }
    }

    void MatikanPemain()
    {
        // Panggil GameRules
        if (GameRules.instance != null)
        {
            // --- PERUBAHAN UTAMA DI SINI ---
            // Dulu: GameRules.instance.ResetKeAwal();
            // Sekarang: Panggil fungsi KALAH agar panel muncul
            GameRules.instance.Kalah();
        }
    }
}