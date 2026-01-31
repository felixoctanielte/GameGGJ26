using UnityEngine;

public class FloatingAnim : MonoBehaviour
{
    [Header("Setting Gerakan")]
    public float tinggiGerakan = 0.5f; // Seberapa jauh naik turunnya (Amplitude)
    public float kecepatan = 2f;      // Seberapa cepat dia gerak (Frequency)

    private Vector3 posisiAwal;

    void Start()
    {
        // Simpan posisi awal bendera saat game mulai
        posisiAwal = transform.position;
    }

    void Update()
    {
        // Rumus Sinus: Menghasilkan angka naik turun (-1 sampai 1) secara halus
        float perubahanY = Mathf.Sin(Time.time * kecepatan) * tinggiGerakan;

        // Terapkan ke posisi bendera (Posisi Awal + Perubahan Y)
        transform.position = new Vector3(posisiAwal.x, posisiAwal.y + perubahanY, posisiAwal.z);
    }
}