using UnityEngine;

public class DamageArea : MonoBehaviour
{

	private void OnTriggerStay(Collider other)
	{
		other.GetComponent<IDamageable>().Damage(10 * Time.deltaTime);
	}
}
