using System.Collections;
using UnityEngine;

public class DummyShooting : MonoBehaviour
{
	public bool autoshoot = false;
	public GameObject Bullet;
	public float rate = 20f;
	bool ready = true;

	private void Update()
	{
		if (autoshoot && ready)
		{
			StartCoroutine(AutoShoot());
		}
	}

	IEnumerator AutoShoot()
	{
		ready = false;
		Instantiate(Bullet, transform.position + transform.forward * 1.1f, transform.rotation);
		yield return new WaitForSeconds(60f / rate);
		ready = true;
	}

	public void SHOOT(float randomness, Vector3 targetPosition)
	{
		// get player position
		// get dir
		Vector3 dir = targetPosition - transform.position;
		// add randomness
		dir = dir.normalized + Random.insideUnitSphere * randomness;

		dir.y = 0;

		Quaternion rot = Quaternion.LookRotation(dir);

		// create bullet
		GameObject go = Instantiate(Bullet, transform.position, rot) as GameObject;

	}
}
