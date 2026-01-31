using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    [Header("Settings Spawner")]
    public GameObject virusPrefab; // Masukkan Prefab Virus Merah/Ungu di sini
    public Transform[] titikSpawn; // Tarik object spawnPoint1, spawnPoint2 ke sini
    public float intervalWaktu = 3f; // Muncul setiap berapa detik?

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // Jika waktu sudah habis, munculkan virus
        if (timer >= intervalWaktu)
        {
            SpawnVirus();
            timer = 0; // Reset timer
        }
    }

    void SpawnVirus()
    {
        // Pilih titik spawn secara acak
        int indexAcak = Random.Range(0, titikSpawn.Length);
        Transform lokasiTerpilih = titikSpawn[indexAcak];

        // Munculkan virus di lokasi tersebut
        Instantiate(virusPrefab, lokasiTerpilih.position, Quaternion.identity);
    }
}