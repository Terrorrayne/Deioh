using UnityEngine;

public class BulletScript : MonoBehaviour
{

	public float speed = 8f;
	public float damageToInflict = 4.5f;


	private void Start()
	{
		Destroy(gameObject, 10f);

	}

	private void Update()
	{
		transform.Translate(transform.forward * speed * Time.deltaTime);
	}


	private void OnTriggerEnter(Collider other)
	{
		// kill me, damage you, fuck this
		IDamageable whatsYourDamage = other.GetComponent<IDamageable>();

		if (whatsYourDamage != null)
		{
			Vector3 dir = other.transform.position - transform.position;
			dir.Normalize();

			whatsYourDamage.Damage(damageToInflict, transform.forward + dir, transform.position);
		}

		Destroy(gameObject);
	}

}
