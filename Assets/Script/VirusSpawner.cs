using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    [Header("Settings Spawner")]
    public GameObject virusPrefab; 
    public Transform[] titikSpawn; 
    public float intervalWaktu = 3f; 

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalWaktu)
        {
            SpawnVirus();
            timer = 0; 
        }
    }

    void SpawnVirus()
    {
        int indexAcak = Random.Range(0, titikSpawn.Length);
        Transform lokasiTerpilih = titikSpawn[indexAcak];
        Instantiate(virusPrefab, lokasiTerpilih.position, Quaternion.identity);
    }
}