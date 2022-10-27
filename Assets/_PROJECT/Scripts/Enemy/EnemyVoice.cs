using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceAudioClips;
    [SerializeField] [Range(0, 1)] private float voiceAudioVolume = 0.25f;
    [SerializeField] private float timeBetweenRoar = 5;

    private float lastTimeRoar = 0;
    private float currentTimeBetweenRoar = 1;
    void Update()
    {
        if (Time.time - lastTimeRoar > currentTimeBetweenRoar)
        {
            AudioSource.PlayClipAtPoint(voiceAudioClips[Random.Range(0, voiceAudioClips.Length)], transform.position, voiceAudioVolume);
            lastTimeRoar = Time.time;
            currentTimeBetweenRoar = Random.Range(timeBetweenRoar - timeBetweenRoar * 0.3f, timeBetweenRoar + timeBetweenRoar * 0.3f);
        }
    }
}
