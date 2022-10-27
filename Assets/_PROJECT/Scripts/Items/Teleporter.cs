using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private string objectTag;
	[SerializeField] private Transform destinationPad;

	[SerializeField] private float teleportationHeightOffset = 1;
	[SerializeField] private AudioSource teleportSound;

	[SerializeField] private bool teleportPadOn = true;

	private GameObject target;
	private bool needToTeleport = false;
	private void FixedUpdate()
	{
		if (needToTeleport)
		{
			target.transform.position = destinationPad.transform.position + new Vector3(0, teleportationHeightOffset, 0);
			needToTeleport = false;
		}
	}

	void OnTriggerStay(Collider trig)
	{
		if (!teleportPadOn) return;
		if (!string.IsNullOrEmpty(objectTag) && trig.gameObject.CompareTag(objectTag) && destinationPad)
		{
			teleportSound.Play();
			target = trig.gameObject;
			needToTeleport = true;
		}
	}
}
