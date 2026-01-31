using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    [Header("Settings Gerak")]
    public float kecepatan = 2f;
    public float jarakPatroli = 1f; 

    private Vector3 posisiAwal;
    private bool bergerakKeKanan = true;

    void Start()
    {
        posisiAwal = transform.position; 
    }

    void Update()
    {

        VirusLogic logic = GetComponent<VirusLogic>();
        if (logic != null && logic.isOnGhostPlatform && !GetComponent<SpriteRenderer>().enabled) return;
        
        Patroli();
    }

    void Patroli()
    {
        if (bergerakKeKanan)
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            if (transform.position.x >= posisiAwal.x + jarakPatroli)
            {
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            if (transform.position.x <= posisiAwal.x - jarakPatroli)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        bergerakKeKanan = !bergerakKeKanan;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
}