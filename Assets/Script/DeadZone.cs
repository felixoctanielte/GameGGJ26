using UnityEngine;

public class DeadZone : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Debug.Log("Jatuh ke Jurang!");
            MatikanPemain();
        }
    }

 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Debug.Log("Jatuh ke Jurang!");
            MatikanPemain();
        }
    }

    void MatikanPemain()
    {
      
        if (GameRules.instance != null)
        {
            GameRules.instance.Kalah();
        }
    }
}