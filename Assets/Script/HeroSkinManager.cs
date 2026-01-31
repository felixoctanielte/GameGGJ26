using UnityEngine;
using UnityEngine.UI;

public class HeroSkinManager : MonoBehaviour
{
    public static HeroSkinManager instance;

    [Header("Settings Komponen")]
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public HeroMovement movementScript; // TARIK Script HeroMovement ke sini di Inspector!
    public GameObject shieldVisual;

    [Header("UI Toolbar & Cooldown")]
    public Slider sliderMedis;
    public Image fillMedis;
    public Slider sliderPulu;
    public Image fillPulu;

    [Header("Asset - Monyet Biasa")]
    public Sprite idleNormal; public Sprite jumpNormal;

    [Header("Asset - Masker Medis (Tombol M)")]
    public Sprite idleMedis; public Sprite jumpMedis;
    public float durasiMaxMedis = 5f;

    [Header("Asset - Masker Pulu (Tombol N)")]
    public Sprite idlePulu; public Sprite jumpPulu;
    public float durasiMaxPulu = 3f;

    [Header("Settings System")]
    public float kecepatanRecharge = 1f;

    private enum MaskState { Normal, Medis, Pulu }
    private MaskState currentState = MaskState.Normal;

    private float staminaMedis;
    private float staminaPulu;
    private bool isMedisExhausted = false;
    private bool isPuluExhausted = false;

    [HideInInspector] public bool isGhostMode = false;
    [HideInInspector] public bool isPuluMask = false;

    void Awake()
    {
        instance = this;
        
        // Hapus Animator Manual agar tidak bentrok
        Animator anim = GetComponent<Animator>();
        if (anim != null) Destroy(anim);
    }

    void Start()
    {
        staminaMedis = durasiMaxMedis;
        staminaPulu = durasiMaxPulu;

        if (sliderMedis != null) sliderMedis.maxValue = durasiMaxMedis;
        if (sliderPulu != null) sliderPulu.maxValue = durasiMaxPulu;

        // Otomatis cari script movement kalau lupa ditarik manual
        if (movementScript == null) movementScript = GetComponent<HeroMovement>();
    }

    void Update()
    {
        HandleInput();
        HandleStamina();
        UpdateStatusVars();
        UpdateSprite(); // Logic perbaikan ada di dalam sini
        UpdateUI();
    }

    // ... (HandleInput, HandleStamina, UpdateStatusVars SAMA SEPERTI SEBELUMNYA) ...
    // ... Copy paste saja bagian input/stamina/status dari script lama ...
    
    void HandleInput() { /* ... COPY DARI SCRIPT SEBELUMNYA ... */ 
       if (Input.GetKeyDown(KeyCode.N)) {
           if (currentState == MaskState.Pulu) currentState = MaskState.Normal;
           else if (staminaPulu > 0 && !isPuluExhausted) currentState = MaskState.Pulu;
       }
       else if (Input.GetKeyDown(KeyCode.M)) {
           if (currentState == MaskState.Medis) currentState = MaskState.Normal;
           else if (staminaMedis > 0 && !isMedisExhausted) currentState = MaskState.Medis;
       }
    }

    void HandleStamina() { /* ... COPY DARI SCRIPT SEBELUMNYA ... */ 
        if (currentState == MaskState.Medis) {
            staminaMedis -= Time.deltaTime;
            if (staminaMedis <= 0) { staminaMedis = 0; isMedisExhausted = true; currentState = MaskState.Normal; }
        } else if (currentState == MaskState.Pulu) {
            staminaPulu -= Time.deltaTime;
            if (staminaPulu <= 0) { staminaPulu = 0; isPuluExhausted = true; currentState = MaskState.Normal; }
        }
        if (currentState != MaskState.Medis) {
            if (staminaMedis < durasiMaxMedis) staminaMedis += Time.deltaTime * kecepatanRecharge;
            if (staminaMedis > durasiMaxMedis * 0.2f) isMedisExhausted = false;
        }
        if (currentState != MaskState.Pulu) {
            if (staminaPulu < durasiMaxPulu) staminaPulu += Time.deltaTime * kecepatanRecharge;
            if (staminaPulu > durasiMaxPulu * 0.2f) isPuluExhausted = false;
        }
    }

    void UpdateStatusVars() { 
        if (currentState == MaskState.Normal) { isGhostMode = false; isPuluMask = false; }
        else if (currentState == MaskState.Medis) { isGhostMode = true; isPuluMask = false; }
        else if (currentState == MaskState.Pulu) { isGhostMode = true; isPuluMask = true; }
    }

    // --- BAGIAN YANG DIPERBAIKI ---
    void UpdateSprite()
    {
        if (spriteRenderer == null) return;

        // PERUBAHAN UTAMA: 
        // Jangan pakai rb.velocity.y, tapi pakai variable isGrounded dari script sebelah.
        // Kalau isGrounded == false, berarti sedang lompat/jatuh.
        bool isJumping = false;
        
        if (movementScript != null)
        {
            isJumping = !movementScript.isGrounded; 
        }

        Sprite targetSprite = null;

        switch (currentState)
        {
            case MaskState.Normal:
                targetSprite = isJumping ? jumpNormal : idleNormal;
                break;
            case MaskState.Medis:
                targetSprite = isJumping ? jumpMedis : idleMedis;
                break;
            case MaskState.Pulu:
                targetSprite = isJumping ? jumpPulu : idlePulu;
                break;
        }

        // Safety check biar gambar gak hilang
        if (targetSprite == null) targetSprite = idleNormal;

        // HANYA GANTI GAMBAR JIKA MEMANG BERBEDA
        // (Ini mencegah Unity me-render ulang gambar yang sama berkali-kali dalam 1 detik)
        if (spriteRenderer.sprite != targetSprite)
        {
            spriteRenderer.sprite = targetSprite;
        }

        if (shieldVisual != null) shieldVisual.SetActive(currentState == MaskState.Medis);
    }

    void UpdateUI() { /* ... COPY DARI SCRIPT SEBELUMNYA ... */ 
        if (sliderMedis != null) sliderMedis.value = staminaMedis;
        if (sliderPulu != null) sliderPulu.value = staminaPulu;
        if (fillMedis != null) {
            if (isMedisExhausted) fillMedis.color = Color.red;
            else if (currentState == MaskState.Medis) fillMedis.color = Color.cyan;
            else fillMedis.color = Color.white;
        }
        if (fillPulu != null) {
            if (isPuluExhausted) fillPulu.color = Color.red;
            else if (currentState == MaskState.Pulu) fillPulu.color = Color.yellow;
            else fillPulu.color = Color.white;
        }
    }
}