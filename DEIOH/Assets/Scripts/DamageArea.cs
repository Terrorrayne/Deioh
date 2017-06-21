using UnityEngine;

public class DamageArea : MonoBehaviour
{
	public float damage = 10f;

	private void OnTriggerStay(Collider other)
	{
		other.GetComponent<IDamageable>().Damage(damage * Time.deltaTime);
	}
}
