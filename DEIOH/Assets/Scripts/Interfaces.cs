using UnityEngine;

public interface IButton
{
	void ButtonPress(PlayerButtonInteraction player);
	bool ButtonIsActive(); // some buttons will only ever return true
}

public interface IDamageable // this goes on anthing that reacts to being attacked
{
	void Damage(float dmg, Vector3 pushDir, Vector3 atkPos);
}