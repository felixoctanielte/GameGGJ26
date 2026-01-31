using UnityEngine;
using UnityEngine.UI;

public class HeroSkinManager : MonoBehaviour
{
    public static HeroSkinManager instance;

    [Header("Settings Komponen")]
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public HeroMovement movementScript;
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
    

    [Tooltip("Berapa lama masker medis aktif? (Request: 3 Detik)")]
    public float durasiAktifMedis = 3f; 
    
    [Tooltip("Berapa lama nunggu sampai bisa dipakai lagi? (Samakan dengan Spawn Virus)")]
    public float durasiCooldownMedis = 3f; 

    [Header("Asset - Masker Pulu (Tombol N)")]
    public Sprite idlePulu; public Sprite jumpPulu;
    public float durasiMaxPulu = 3f;

    [Header("Settings System (Pulu Only)")]
    public float kecepatanRechargePulu = 1f; 

    private enum MaskState { Normal, Medis, Pulu }
    private MaskState currentState = MaskState.Normal;

    private float staminaMedis;
    private float staminaPulu;

    private bool isMedisCooldown = false; 
    private bool isPuluExhausted = false;

    [HideInInspector] public bool isGhostMode = false;
    [HideInInspector] public bool isPuluMask = false;

    void Awake()
    {
        instance = this;
        Animator anim = GetComponent<Animator>();
        if (anim != null) Destroy(anim);
    }

    void Start()
    {
        // Set stamina awal full
        staminaMedis = durasiAktifMedis;
        staminaPulu = durasiMaxPulu;

        // Setup Slider
        if (sliderMedis != null) sliderMedis.maxValue = durasiAktifMedis;
        if (sliderPulu != null) sliderPulu.maxValue = durasiMaxPulu;

        if (movementScript == null) movementScript = GetComponent<HeroMovement>();
    }

    void Update()
    {
        HandleInput();
        HandleStamina();
        UpdateStatusVars();
        UpdateSprite();
        UpdateUI();
    }

    void HandleInput()
    {
       
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentState == MaskState.Pulu) currentState = MaskState.Normal;
            else if (staminaPulu > 0 && !isPuluExhausted) currentState = MaskState.Pulu;
        }
        
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (currentState == MaskState.Medis)
            {
                
                currentState = MaskState.Normal;
            }
            else
            {
                
                if (!isMedisCooldown && staminaMedis >= durasiAktifMedis * 0.9f) 
                {
                    currentState = MaskState.Medis;
                }
                else
                {
                    Debug.Log("Masker Medis Masih Cooldown! Tunggu Bar Penuh.");
                }
            }
        }
    }

    void HandleStamina()
    {
        if (currentState == MaskState.Medis)
        {
            staminaMedis -= Time.deltaTime;
           
            if (staminaMedis <= 0)
            {
                staminaMedis = 0;
                currentState = MaskState.Normal; 
                isMedisCooldown = true; 
            }
        }
        else 
        {
            if (staminaMedis < durasiAktifMedis)
            {
                float rechargeRate = durasiAktifMedis / durasiCooldownMedis;
                staminaMedis += Time.deltaTime * rechargeRate;
            }
            else
            {
                staminaMedis = durasiAktifMedis; 
                isMedisCooldown = false; 
            }
        }

        if (currentState == MaskState.Pulu)
        {
            staminaPulu -= Time.deltaTime;
            if (staminaPulu <= 0) { staminaPulu = 0; isPuluExhausted = true; currentState = MaskState.Normal; }
        }
        else
        {
            if (staminaPulu < durasiMaxPulu) staminaPulu += Time.deltaTime * kecepatanRechargePulu;
            if (staminaPulu > durasiMaxPulu * 0.2f) isPuluExhausted = false;
        }
    }

    void UpdateStatusVars()
    {
        if (currentState == MaskState.Normal) { isGhostMode = false; isPuluMask = false; }
        else if (currentState == MaskState.Medis) { isGhostMode = true; isPuluMask = false; }
        else if (currentState == MaskState.Pulu) { isGhostMode = true; isPuluMask = true; }
    }

    void UpdateSprite()
    {
        if (spriteRenderer == null) return;

        bool isJumping = false;
        if (movementScript != null) isJumping = !movementScript.isGrounded;

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

        if (targetSprite == null) targetSprite = idleNormal;
        if (spriteRenderer.sprite != targetSprite) spriteRenderer.sprite = targetSprite;

        if (shieldVisual != null) shieldVisual.SetActive(currentState == MaskState.Medis);
    }

    void UpdateUI()
    {
        // Update Nilai Slider
        if (sliderMedis != null) sliderMedis.value = staminaMedis;
        if (sliderPulu != null) sliderPulu.value = staminaPulu;

        if (fillMedis != null)
        {
            if (currentState == MaskState.Medis) 
            {
                fillMedis.color = Color.cyan;
            }
            else if (isMedisCooldown || staminaMedis < durasiAktifMedis) 
            {
                fillMedis.color = Color.red;
            }
            else 
            {
                fillMedis.color = Color.white; 
            }
        }

        
        if (fillPulu != null)
        {
            if (isPuluExhausted) fillPulu.color = Color.red;
            else if (currentState == MaskState.Pulu) fillPulu.color = Color.yellow;
            else fillPulu.color = Color.white;
        }
    }
}