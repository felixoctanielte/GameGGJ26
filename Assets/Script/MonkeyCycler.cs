using UnityEngine;
using System.Collections;

public class MonkeyCycler : MonoBehaviour
{
    public GameObject[] listMonyet;
    public float gantiTiapDetik = 2f;
    private int indexSekarang = 0;

    void Start()
    {
        StartCoroutine(GantiMonyetRoutine());
    }

    IEnumerator GantiMonyetRoutine()
    {
        while (true)
        {
            foreach (GameObject m in listMonyet)
            {
                m.SetActive(false);
            }

            listMonyet[indexSekarang].SetActive(true);

            yield return new WaitForSeconds(gantiTiapDetik);

            indexSekarang++;
            
            if (indexSekarang >= listMonyet.Length)
            {
                indexSekarang = 0;
            }
        }
    }
}