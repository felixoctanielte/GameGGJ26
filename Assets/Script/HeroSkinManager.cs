using UnityEngine;

public class HeroSkinManager : MonoBehaviour
{
    public static HeroSkinManager instance;

    [Header("Settings")]
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Asset - Monyet Biasa")]
    public Sprite idleNormal;
    public Sprite jumpNormal;

    [Header("Asset - Masker Medis (Normal)")]
    public Sprite idleMedis;
    public Sprite jumpMedis;

    [Header("Asset - Masker Pulu (Emas)")]
    public Sprite idlePulu;
    public Sprite jumpPulu;

    // Status (Bisa dibaca script lain)
    public bool isGhostMode = false; // Sedang tekan M?
    public bool isPuluMask = false;  // False = Medis, True = Pulu

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        // 1. Cek Input Ganti Masker (Tombol N)
        if (Input.GetKeyDown(KeyCode.N))
        {
            isPuluMask = !isPuluMask; // Switch true/false
            Debug.Log("Ganti Masker: " + (isPuluMask ? "PULU" : "MEDIS"));
        }

        // 2. Cek Input Mode Ghost (Tombol M ditahan)
        // Sesuaikan dengan script Ghost Platform kamu (Input.GetKey)
        if (Input.GetKey(KeyCode.M))
        {
            isGhostMode = true;
        }
        else
        {
            isGhostMode = false;
        }

        // 3. Update Gambar (Sprite Swapping)
        UpdateSprite();
    }

    void UpdateSprite()
    {
        // Cek apakah sedang lompat? (Velocity Y tidak 0)
        // Kita pakai toleransi 0.1f biar ga flickering pas diem
        bool isJumping = Mathf.Abs(rb.velocity.y) > 0.1f;

        if (isGhostMode)
        {
            // --- MODE MASKER (M Ditekan) ---
            if (isPuluMask)
            {
                // Pakai Masker Pulu
                spriteRenderer.sprite = isJumping ? jumpPulu : idlePulu;
            }
            else
            {
                // Pakai Masker Medis
                spriteRenderer.sprite = isJumping ? jumpMedis : idleMedis;
            }
        }
        else
        {
            // --- MODE NORMAL (M Tidak Ditekan) ---
            spriteRenderer.sprite = isJumping ? jumpNormal : idleNormal;
        }
    }
}