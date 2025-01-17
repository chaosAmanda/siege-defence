using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private float radius;

    [SerializeField] private GameObject enemy;

    [SerializeField] private float cooldown;

    [SerializeField] private GameObject Manager;

    public float TowerBase;

    private int spawncount = 0;

    void NewSpawn()
    {
        float spawnX = Random.Range(-radius, radius);
        int spawnSide = Random.Range(0, 1);

        float spawnZ = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(spawnX, 2));
        if (spawnSide == 0)
        {
            spawnZ = -spawnZ;
        }

        GameObject newEnemy = Instantiate(enemy, new Vector3(spawnX, TowerBase, spawnZ), Quaternion.identity);
        newEnemy.GetComponent<enemyBehaviour>().Manager = Manager;
        spawncount++;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0) {
            if (spawncount <= 10)
            {
                NewSpawn();
                cooldown = Random.Range(3, 5);
            }
            else if (spawncount <= 20) 
            {
                NewSpawn();
                cooldown = Random.Range(2, 4);
            }
            else
            {
                NewSpawn();
                cooldown = Random.Range(1, 2);
            }
        }

        cooldown -= Time.deltaTime; 
    }
}
