using UnityEngine;


public class Spike : MonoBehaviour
{
	[SerializeField] private LayerMask CollisionLayer;

	void OnTriggerEnter(Collider col)
	{
		if (!enabled) return;
		if (!((CollisionLayer.value & (1 << col.transform.gameObject.layer)) > 0))
		{
			return;
		}

		IDamagable<GameObject> damagableObject = col.GetComponent(typeof(IDamagable<GameObject>)) as IDamagable<GameObject>;
		if (damagableObject != null)
		{
			damagableObject.Hit(gameObject);
		}
	}
	
}
