using UnityEngine;

public class DummyHealth : CharacterHealth
{
	Material myMat;
	Vector3 startpos;
	// Use this for initialization
	void Start()
	{
		myMat = GetComponent<MeshRenderer>().material;
		startpos = transform.position;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		myMat.color = Color.Lerp(myMat.color, Color.white, 5f * Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, startpos, 10f * Time.deltaTime);
	}

	public override void Damage(float dmg, Vector3 pushDir, Vector3 atkPos)
	{
		base.Damage(dmg, pushDir, atkPos);
		print(dmg);
		myMat.color = Color.red;

		// recoil
		transform.position += pushDir.normalized * dmg * 0.1f;
	}
}
