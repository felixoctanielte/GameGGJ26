using UnityEngine;

public class FloatingAnim : MonoBehaviour
{
    [Header("Setting Gerakan")]
    public float tinggiGerakan = 0.5f; 
    public float kecepatan = 2f;      

    private Vector3 posisiAwal;

    void Start()
    {
        posisiAwal = transform.position;
    }

    void Update()
    {
        float perubahanY = Mathf.Sin(Time.time * kecepatan) * tinggiGerakan;
        transform.position = new Vector3(posisiAwal.x, posisiAwal.y + perubahanY, posisiAwal.z);
    }
}