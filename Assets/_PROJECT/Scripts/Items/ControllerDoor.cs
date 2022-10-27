using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDoor : MonoBehaviour
{
	[SerializeField] private string[] objectTag;
	
	public UnityEvent OnDoorOpen;
	public UnityEvent OnDoorClose;

	private List<GameObject> objectsNearTheDoor;
	private bool doorOpened = false;


    private void Start()
    {
		objectsNearTheDoor = new List<GameObject>();
	}

    void OnTriggerEnter(Collider trig)
	{
		for (int i = 0; i < objectTag.Length; i++)
        {
			if (!string.IsNullOrEmpty(objectTag[i]) && trig.gameObject.CompareTag(objectTag[i]))
			{
				if (!objectsNearTheDoor.Contains(trig.gameObject))
				{
					objectsNearTheDoor.Add(trig.gameObject);
				}
				if (!doorOpened)
                {
					OnDoorOpen.Invoke();
					doorOpened = true;
				}
			}
        }
	}
	
	void OnTriggerExit(Collider trig)
	{
		for (int i = 0; i < objectTag.Length; i++)
		{
			if (!string.IsNullOrEmpty(objectTag[i]) && trig.gameObject.CompareTag(objectTag[i]))
			{
				if (objectsNearTheDoor.Contains(trig.gameObject))
				{
					objectsNearTheDoor.Remove(trig.gameObject);
				}
				if (objectsNearTheDoor.Count == 0)
				{
					OnDoorClose.Invoke();
					doorOpened = false;
				}
			}
		}
	}

}