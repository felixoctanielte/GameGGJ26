using UnityEngine;

public class FinishLine : MonoBehaviour
{
   
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Hero"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
            
                if (contact.normal.y < -0.5f)
                {
                    Debug.Log("Mendarat di atas Finish Line!");
                    CekKemenangan(); 
                    break; 
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
          
            if (other.transform.position.y > transform.position.y)
            {
                CekKemenangan();
            }
        }
    }

    void CekKemenangan()
    {
        if (GameRules.instance != null)
        {
          
            int langkahSekarang = GameRules.instance.lompatanSaatIni;
            int targetLangkah = GameRules.instance.batasLompatan; 

            if (langkahSekarang == targetLangkah)
            {
                Debug.Log("SUKSES! Langkah pas: " + langkahSekarang);
                GameRules.instance.Menang();
            }
            else
            {
                Debug.Log("GAGAL! Langkah kamu: " + langkahSekarang + ". Target: " + targetLangkah);
                
                GameRules.instance.ResetKeAwal();
            }
        }
    }
}