using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // OPSI 1: Jika Collider kamu dicentang "Is Trigger" (Rekomendasi)
    // Pemain akan menembus garis ini lalu mati
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero")) // Pastikan Tag player kamu "Hero"
        {
            Debug.Log("Jatuh ke Jurang!");
            MatikanPemain();
        }
    }

    // OPSI 2: Jika Collider kamu TIDAK dicentang "Is Trigger"
    // Pemain akan nabrak (seperti tanah) lalu mati
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
        // Panggil wasit (GameRules) untuk mereset game
        if (GameRules.instance != null)
        {
            GameRules.instance.ResetKeAwal();
        }
    }
}