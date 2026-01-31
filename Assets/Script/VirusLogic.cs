using UnityEngine;

public class VirusLogic : MonoBehaviour
{
    [Header("Settings")]
    public bool isOnGhostPlatform = false; // CENTANG INI jika virus ditaruh di platform ghost
    public SpriteRenderer visualVirus;
    public Collider2D colliderVirus;

    void Start()
    {
        // Pastikan punya referensi visual
        if (visualVirus == null) visualVirus = GetComponent<SpriteRenderer>();
        if (colliderVirus == null) colliderVirus = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Logika Visibility (Hanya untuk virus di Ghost Platform)
        if (isOnGhostPlatform)
        {
            CheckVisibility();
        }
    }

    void CheckVisibility()
    {
        // Virus Ghost HANYA terlihat jika Hero sedang tekan M DAN pakai Masker Pulu
        if (HeroSkinManager.instance != null)
        {
            bool heroPakaiPulu = HeroSkinManager.instance.isGhostMode && HeroSkinManager.instance.isPuluMask;

            if (heroPakaiPulu)
            {
                visualVirus.enabled = true;
            }
            else
            {
                visualVirus.enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            InteractWithHero();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            InteractWithHero();
        }
    }

    void InteractWithHero()
    {
        if (HeroSkinManager.instance != null)
        {
            // Ambil status Hero
            bool isGhostMode = HeroSkinManager.instance.isGhostMode; // Tekan M
            bool isPulu = HeroSkinManager.instance.isPuluMask;       // Tipe Pulu

            // SYARAT SERANGAN:
            // Hero harus tekan M (Mode Masker) DAN Pakai Masker Medis (Bukan Pulu)
            if (isGhostMode && !isPulu)
            {
                // HERO MENYERANG VIRUS!
                Debug.Log("Virus Hancur oleh Masker Medis!");
                Destroy(gameObject); 
            }
            else
            {
                // HERO MATI
                Debug.Log("Hero Mati kena Virus!");
                
                if (GameRules.instance != null)
                {
                    // --- PERUBAHAN UTAMA DI SINI ---
                    // Dulu: GameRules.instance.ResetKeAwal();
                    // Sekarang: Panggil fungsi KALAH agar panel muncul
                    GameRules.instance.Kalah();
                }
            }
        }
    }
}