using UnityEngine;

public class VirusLogic : MonoBehaviour
{
    [Header("Settings")]
    public bool isOnGhostPlatform = false; 
    public SpriteRenderer visualVirus;
    public Collider2D colliderVirus;

    void Start()
    {
 
        if (visualVirus == null) visualVirus = GetComponent<SpriteRenderer>();
        if (colliderVirus == null) colliderVirus = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isOnGhostPlatform)
        {
            CheckVisibility();
        }
    }

    void CheckVisibility()
    {
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
            
            bool isGhostMode = HeroSkinManager.instance.isGhostMode; 
            bool isPulu = HeroSkinManager.instance.isPuluMask;       
            if (isGhostMode && !isPulu)
            {
                Debug.Log("Virus Hancur oleh Masker Medis!");
                Destroy(gameObject); 
            }
            else
            {
                Debug.Log("Hero Mati kena Virus!");
                
                if (GameRules.instance != null)
                {
                    GameRules.instance.Kalah();
                }
            }
        }
    }
}