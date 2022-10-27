using UnityEngine;
using System;

public class HealthController : MonoBehaviour, IDamagable<GameObject>
{
    public static event Action OnHealthChanged;

    [SerializeField] private int healthMax;
    public int HealthMax
    {
        get => healthMax;
    }

    [SerializeField] private AudioClip HurtAudioClip;



    private int healthCurrent = 1;
    public int HealthCurrent
    {
        get => healthCurrent;
    }

    void Start()
    {
        healthCurrent = healthMax;
    }

    public void Hit(GameObject hitObject)
    {
        healthCurrent--;
        AudioSource.PlayClipAtPoint(HurtAudioClip, transform.position); 

        if (gameObject.tag.Contains("Player"))
        {
            OnHealthChanged?.Invoke();
        }
        return;
    }
 }
