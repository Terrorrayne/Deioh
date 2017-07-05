using System.Collections;
using UnityEngine;

public class DummyShooting : MonoBehaviour
{

	public GameObject Bullet;
	public float rate = 20f;
	bool ready = true;

	private void Update()
	{
		if (ready)
		{
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot()
	{
		ready = false;
		Instantiate(Bullet, transform.position + transform.forward * 1.1f, transform.rotation);
		yield return new WaitForSeconds(60f / rate);
		ready = true;
	}

}
