using UnityEngine;
using System.Collections.Generic;

/*
    This class is designed for placement of objects randomly in current space
 */

public class SpawnerEnemy : MonoBehaviour, IHosted
{
    [SerializeField] GameObject[] itemsList;

    [SerializeField] int spawnQuantity;
    [SerializeField] int levelSpawnQuantityMultiplier = 1;


    [SerializeField] float floorZMin;
    [SerializeField] float floorZMax;
    [SerializeField] float floorY;
    [SerializeField] float floorXMin;
    [SerializeField] float floorXMax;

    [SerializeField] private List<AvoidSettings> settings;

    void Start()
    {
        LevelController lc = FindObjectOfType<LevelController>(true);
        spawnQuantity += lc.LevelCurrent * levelSpawnQuantityMultiplier;
        while (spawnQuantity > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(floorXMin, floorXMax), floorY, Random.Range(floorZMin, floorZMax));
            if (!IsNeedToAvoid(spawnPos, settings))
            {
                spawnQuantity--;
                GameObject enemy = Instantiate(itemsList[Random.Range(0, itemsList.Length)], spawnPos, Quaternion.identity);
                enemy.transform.SetParent(gameObject.transform);
            }
        }
    }

    public bool IsNeedToAvoid(Vector3 pos, List<AvoidSettings> settings)
    {
        foreach (AvoidSettings setting in settings)
        {
            Collider[] col = Physics.OverlapSphere(pos, setting.avoidRadius, setting.skipLayer);
            if (col.Length > 0) return true;

        }
        return false;
    }
}
