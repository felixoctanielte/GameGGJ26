using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D col; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        
        if (HeroSkinManager.instance == null) return;

   
        
        bool isPakaiMaskerMedis = HeroSkinManager.instance.isGhostMode && !HeroSkinManager.instance.isPuluMask;

        UpdatePlatform(isPakaiMaskerMedis);
    }

    void UpdatePlatform(bool aktif)
    {
        
        if (spriteRenderer != null) 
        {
            spriteRenderer.enabled = aktif;
        }


        if (col != null) 
        {
            col.enabled = aktif;
        }
    }
}