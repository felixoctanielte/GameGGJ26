using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    [Header("Settings Gerak")]
    public float kecepatan = 2f;
    public float jarakPatroli = 1f; // Seberapa jauh dia jalan ke kiri/kanan dari titik awal

    private Vector3 posisiAwal;
    private bool bergerakKeKanan = true;

    void Start()
    {
        posisiAwal = transform.position; // Catat posisi lahirnya
    }

    void Update()
    {
        // Kalau ini virus hantu dan sedang tidak terlihat, jangan gerak
        VirusLogic logic = GetComponent<VirusLogic>();
        if (logic != null && logic.isOnGhostPlatform && !GetComponent<SpriteRenderer>().enabled) return;
        
        Patroli();
    }

    void Patroli()
    {
        if (bergerakKeKanan)
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            // Cek apakah sudah terlalu jauh ke kanan
            if (transform.position.x >= posisiAwal.x + jarakPatroli)
            {
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            // Cek apakah sudah terlalu jauh ke kiri
            if (transform.position.x <= posisiAwal.x - jarakPatroli)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        bergerakKeKanan = !bergerakKeKanan;
        // Balik arah gambar virusnya
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
}