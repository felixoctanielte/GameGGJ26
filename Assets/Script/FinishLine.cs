using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Gunakan OnCollisionEnter2D agar kita bisa deteksi arah "Injak"
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Cek Tag
        if (collision.gameObject.CompareTag("Hero"))
        {
            // 2. CEK ARAH DATANG (Logika "Di Atas")
            // Kita cek semua titik kontak tumbukan
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Jika normal.y < -0.5f, artinya tekanan datang dari atas (Hero mendarat)
                // Sama seperti logic di PlatformPoint
                if (contact.normal.y < -0.5f)
                {
                    Debug.Log("Mendarat di atas Finish Line!");
                    CekKemenangan(); // Panggil fungsi cek
                    break; // Cukup satu titik valid, langsung keluar loop
                }
            }
        }
    }

    // OPSI CADANGAN: Jika kamu tetap pakai Is Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            // Karena Trigger tidak punya 'ContactPoint', kita cek posisi Y manual
            // Jika Kaki Hero (posisi y) lebih tinggi dari Pusat Finish Line
            if (other.transform.position.y > transform.position.y)
            {
                CekKemenangan();
            }
        }
    }

    void CekKemenangan()
    {
        if (GameRules.instance != null)
        {
            // AMBIL DATA DARI GAMERULES
            int langkahSekarang = GameRules.instance.lompatanSaatIni;
            int targetLangkah = GameRules.instance.batasLompatan; // Biasanya 10

            // LOGIKA PENGECEKAN
            if (langkahSekarang == targetLangkah)
            {
                // Jika langkah PAS 10
                Debug.Log("SUKSES! Langkah pas: " + langkahSekarang);
                GameRules.instance.Menang();
            }
            else
            {
                // Jika langkah BELUM atau LEBIH dari 10
                Debug.Log("GAGAL! Langkah kamu: " + langkahSekarang + ". Target: " + targetLangkah);
                
                // Hukum player dengan Reset
                GameRules.instance.ResetKeAwal();
            }
        }
    }
}