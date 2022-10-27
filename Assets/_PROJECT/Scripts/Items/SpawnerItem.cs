using UnityEngine;
using System.Collections.Generic;

/*
    This class is designed for placement of objects according to the square-nest method. 
    This is most suitable for placing things in locations with a pac-man level design, 
    where the width of corridors, passages and walls does not change.
 */

public class SpawnerItem : MonoBehaviour, IHosted
{
    [SerializeField] GameObject[] itemsList;

    [SerializeField] float spawnChance;
    [SerializeField] float spawnChanceLevelMultiplier;
    

    [SerializeField] float floorZMin;
    [SerializeField] float floorZMax;
    [SerializeField] float floorY;
    [SerializeField] float floorXMin;
    [SerializeField] float floorXMax;
    [SerializeField] float xStep;
    [SerializeField] float zStep;
    

    [SerializeField] private List<AvoidSettings> settings;


    void Start()
    {
        LevelController lc = FindObjectOfType<LevelController>(true);
        float chance = spawnChance + lc.LevelCurrent * spawnChanceLevelMultiplier;
        for (int j = 0; j <= (int)((floorZMax - floorZMin)/zStep); j++)
        {
            for (int i = 0; i < (int)((floorXMax - floorXMin)/xStep); i++)
            {
                if (!IsNeedToAvoid(new Vector3(floorXMin + i * xStep, floorY, floorZMin + j * zStep ), settings))
                {
                    if (Random.Range(0f, 1f) < chance)
                    {
                        Vector3 spawnPos = new Vector3(floorXMin + i * xStep, floorY, floorZMin + j * zStep);
                        GameObject item = Instantiate(itemsList[Random.Range(0, itemsList.Length)], spawnPos, Quaternion.identity);
                        item.transform.SetParent(gameObject.transform);
                    }
                }
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

