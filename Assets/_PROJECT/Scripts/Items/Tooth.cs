using UnityEngine;
using System;
using System.Collections;


public class Tooth : MonoBehaviour
{
   
    public static event Action OnCollectTooth;
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            OnCollectTooth?.Invoke();
            StartCoroutine(DestroyTooth());
        }
    }

    IEnumerator DestroyTooth()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
