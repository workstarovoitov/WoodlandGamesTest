using UnityEngine;
using Cinemachine;
/*
    It would be nice to split this class in two :
    separate for spawn routine and separate for teleporting on hit
 */
public class SpawnerPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Vector3 spawnOffset;
    private GameObject player;
    private bool needToTeleport = false;

    void Start()
    {
        HealthController.OnHealthChanged += TeleportToSafePlace;

        player = Instantiate(playerPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position + spawnOffset, Quaternion.Euler(Vector3.zero));
        CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>(true);
        GameObject target = GameObject.FindGameObjectWithTag("CinemachineTarget");
        vcam.Follow = target.transform;
    }

    private void FixedUpdate()
    {
        if (needToTeleport)
        {
            player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position + spawnOffset;
            needToTeleport = false;
        }
    }

    private void OnDisable()
    {
        HealthController.OnHealthChanged -= TeleportToSafePlace;
    }

    private void TeleportToSafePlace()
    {
        needToTeleport = true;
    }
}
