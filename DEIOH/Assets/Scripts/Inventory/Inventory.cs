using UnityEngine;

public class Inventory : MonoBehaviour
{

	// based off https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/inventory?playlist=44381

	public int numItemSlots = 8;
	public InventoryItem[] items;

	public void Start()
	{
		items = new InventoryItem[numItemSlots];
	}

	public void AddItem(InventoryItem itemToAdd)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == null)
			{
				items[i] = itemToAdd;
				return;
			}
		}
	}
	public void RemoveItem(InventoryItem itemToRemove)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == itemToRemove)
			{
				items[i] = null;
				return;
			}
		}
	}
}
