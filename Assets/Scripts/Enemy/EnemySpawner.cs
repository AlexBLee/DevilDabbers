using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject enemyPrefab;
    public GameObject groundPlane;

    public float spawnY = 2.5f;
    private float timer = 0f;

    private Vector3 _spawnBounds;
    public GameObject playerObj;

    public GameObject enemyDeathParticleSys;

    private void Awake()
    {
        instance = this;
        timer = 0.5f;
    }

    // Use this for initialization
    void Start ()
    {
        if(groundPlane != null)
        {
            MeshRenderer mr = groundPlane.GetComponent<MeshRenderer>();
            if(mr != null)
            {
                _spawnBounds = mr.bounds.extents;
            }

        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(SpawnTimer());
    }


    public void SpawnEnemy()
    {
        if(enemyPrefab != null)
        {
            GameObject clone = Instantiate(enemyPrefab, FindSpawnPosition(), Quaternion.identity);

            // Set a random colour.
            Color newColour = Random.ColorHSV();
            Material mat = clone.GetComponent<MeshRenderer>().material;
            mat.color = newColour;

            Enemy en = clone.GetComponent<Enemy>();
            en.playerObj = playerObj;
        }
    }


    public void SpawnDeathParticleSystem(GameObject enemy)
    {
        Color curColour = enemy.GetComponent<MeshRenderer>().material.color;
        float r = Random.Range(Mathf.Clamp(curColour.r - 0.1f, 0.0f, 1.0f), Mathf.Clamp(curColour.r + 0.1f, 0.0f, 1.0f));
        float g = Random.Range(Mathf.Clamp(curColour.g - 0.1f, 0.0f, 1.0f), Mathf.Clamp(curColour.g + 0.1f, 0.0f, 1.0f));
        float b = Random.Range(Mathf.Clamp(curColour.b - 0.1f, 0.0f, 1.0f), Mathf.Clamp(curColour.b + 0.1f, 0.0f, 1.0f));
        Color otherColour = new Color(r, g, b);


        if (enemyDeathParticleSys != null)
        {
            GameObject obj = Instantiate(enemyDeathParticleSys, enemy.transform.position, Quaternion.identity);
            ParticleSystem ps = obj.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;
            ParticleSystem.MinMaxGradient mainColourGradient = main.startColor;
            mainColourGradient.colorMin = curColour;
            mainColourGradient.colorMax = otherColour;
            main.startColor = mainColourGradient;

            ps.Emit(100);
            Destroy(ps.gameObject, ps.main.startLifetime.constantMax);

        }
    }

    private Vector3 FindSpawnPosition()
    {
        Vector3 pos = groundPlane.transform.position;

        float randX = Random.Range(-_spawnBounds.x, _spawnBounds.x);
        float randZ = Random.Range(-_spawnBounds.z, _spawnBounds.z);
        Vector3 randOffset = new Vector3(randX, spawnY, randZ);

        pos += randOffset;

        return pos;
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(timer);
        SpawnEnemy();
        StopAllCoroutines();

    }
}
