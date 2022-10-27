using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnemySpeedChange : MonoBehaviour
{
    [SerializeField] float levelSpeedMultiplier = 0.25f;

    // Start is called before the first frame update
    void Start()
    {

        LevelController lc = FindObjectOfType<LevelController>(true);
        GetComponent<ThirdPersonControllerAI>().MoveSpeed = GetComponent<ThirdPersonControllerAI>().MoveSpeed + GetComponent<ThirdPersonControllerAI>().MoveSpeed * lc.LevelCurrent * levelSpeedMultiplier;
    }

}
