using UnityEngine;
using System;

public class MaskSystem : MonoBehaviour
{
    [Header("Pengaturan Waktu")]
    public float durasiPakai = 60f;    
    public float durasiCooldown = 1f; 

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
     
        if (Input.GetKeyDown(KeyCode.M) && !sedangPakai && !sedangCooldown)
        {
            MulaiPakaiMasker();
        }

        if (sedangPakai)
        {
            sisaWaktuPakai -= Time.deltaTime;
            
            if (sisaWaktuPakai <= 0)
            {
                SelesaiPakaiMasker();
            }
        }

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
        OnMaskEquip?.Invoke(); 
    }

    void SelesaiPakaiMasker()
    {
        sedangPakai = false;
        sedangCooldown = true;
        sisaWaktuCooldown = durasiCooldown;

        Debug.Log("Masker Habis! Mulai Cooldown (Tunggu 7 Detik...)");
        OnMaskUnequip?.Invoke(); 
    }

    void SelesaiCooldown()
    {
        sedangCooldown = false;
        Debug.Log("SIAP! Masker bisa dipakai lagi.");
    }
}