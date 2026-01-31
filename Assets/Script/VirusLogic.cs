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
                // Munculkan Virus
                visualVirus.enabled = true;
            }
            else
            {
                // Sembunyikan Virus
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
                
                // Efek suara atau partikel bisa ditambah di sini
                Destroy(gameObject); 
            }
            else
            {
                // HERO MATI
                // Karena ga pake masker, atau pake masker Pulu (Pulu cuma buat lihat, bukan nyerang)
                Debug.Log("Hero Mati kena Virus!");
                
                if (GameRules.instance != null)
                {
                    GameRules.instance.ResetKeAwal();
                }
            }
        }
    }
}