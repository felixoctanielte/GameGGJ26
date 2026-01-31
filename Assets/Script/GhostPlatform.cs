using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Mengambil referensi komponen gambar pada objek ini
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Saat game dimulai, platform otomatis sembunyi dulu
        SetVisibility(false);
    }

    // --- BAGIAN EVENT (Mendengarkan MaskSystem) ---

    void OnEnable()
    {
        // Mendaftar ke event: "Kalau masker nyala, panggil fungsi ShowPlatform"
        MaskSystem.OnMaskEquip += ShowPlatform;
        
        // Mendaftar ke event: "Kalau masker mati, panggil fungsi HidePlatform"
        MaskSystem.OnMaskUnequip += HidePlatform;
    }

    void OnDisable()
    {
        // Wajib lapor berhenti mendaftar saat objek dimatikan/pindah scene agar tidak error
        MaskSystem.OnMaskEquip -= ShowPlatform;
        MaskSystem.OnMaskUnequip -= HidePlatform;
    }

    // --- BAGIAN LOGIKA ---

    void ShowPlatform()
    {
        SetVisibility(true);
    }

    void HidePlatform()
    {
        SetVisibility(false);
    }

    void SetVisibility(bool isVisible)
    {
        if (spriteRenderer != null)
        {
            // KUNCI UTAMA:
            // Kita hanya mengubah 'enabled' pada SpriteRenderer (Gambarnya).
            // Kita TIDAK menyentuh Collider. 
            // Jadi walaupun isVisible = false (gambar hilang), hero tetap bisa berdiri di atasnya.
            spriteRenderer.enabled = isVisible;
        }
    }
}