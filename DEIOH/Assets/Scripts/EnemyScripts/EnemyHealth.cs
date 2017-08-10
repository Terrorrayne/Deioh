using UnityEngine;

public class EnemyHealth : CharacterHealth
{
	Material myMat;

	private void Start()
	{
		myMat = GetComponent<MeshRenderer>().material;
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		myMat.color = Color.Lerp(myMat.color, Color.white, 5f * Time.deltaTime);

	}

	public override void Damage(float dmg, Vector3 pushDir, Vector3 atkPos)
	{
		base.Damage(dmg, pushDir, atkPos);
		print(dmg);
		myMat.color = Color.red;
	}
}
