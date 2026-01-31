using UnityEngine;
using System;

public class MaskSystem : MonoBehaviour
{
    [Header("Pengaturan Waktu")]
    public float durasiPakai = 60f;    // Waktu masker aktif (3 detik)
    public float durasiCooldown = 1f; // Waktu tunggu (7 detik)

    [Header("Status (Jangan diubah manual)")]
    public bool sedangPakai = false;
    public bool sedangCooldown = false;
    public float sisaWaktuPakai;
    public float sisaWaktuCooldown;

    // Event system
    public static event Action OnMaskEquip;
    public static event Action OnMaskUnequip;

    void Update()
    {
        // 1. INPUT: Hanya bisa tekan M kalau TIDAK sedang pakai DAN TIDAK sedang cooldown
        if (Input.GetKeyDown(KeyCode.M) && !sedangPakai && !sedangCooldown)
        {
            MulaiPakaiMasker();
        }

        // 2. LOGIKA DURASI (Saat Masker Dipakai)
        if (sedangPakai)
        {
            sisaWaktuPakai -= Time.deltaTime;
            
            if (sisaWaktuPakai <= 0)
            {
                SelesaiPakaiMasker();
            }
        }

        // 3. LOGIKA COOLDOWN (Saat Nunggu)
        if (sedangCooldown)
        {
            sisaWaktuCooldown -= Time.deltaTime;

            if (sisaWaktuCooldown <= 0)
            {
                SelesaiCooldown();
            }
        }
    }

    void MulaiPakaiMasker()
    {
        sedangPakai = true;
        sisaWaktuPakai = durasiPakai;
        
        Debug.Log("Masker ON! (Sisa 3 Detik)");
        OnMaskEquip?.Invoke(); // Munculkan platform hantu
    }

    void SelesaiPakaiMasker()
    {
        sedangPakai = false;
        
        // Langsung masuk ke fase Cooldown
        sedangCooldown = true;
        sisaWaktuCooldown = durasiCooldown;

        Debug.Log("Masker Habis! Mulai Cooldown (Tunggu 7 Detik...)");
        OnMaskUnequip?.Invoke(); // Hilangkan platform hantu
    }

    void SelesaiCooldown()
    {
        sedangCooldown = false;
        Debug.Log("SIAP! Masker bisa dipakai lagi.");
    }
}