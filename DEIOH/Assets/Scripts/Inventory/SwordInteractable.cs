using UnityEngine;

public class SwordInteractable : MonoBehaviour, IButton
{


	public InventoryItem item;

	// maybe make a base Interactable/World Inventory Object

	public bool ButtonIsActive()
	{
		// pick up
		return true;
	}


	public void ButtonPress(PlayerButtonInteraction player)
	{
		// pick up
		player.GetComponent<Inventory>().AddItem(item);
		player.GetComponent<PlayerItemUsage>().UpdateInventoryImages();
	}
}
