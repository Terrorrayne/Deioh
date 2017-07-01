using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
	[Tooltip("Item's name in the game")]
	public string displayName;
	[Tooltip("Sprite used in the inventory")]
	public Sprite itemSprite;
	[Tooltip("The prefab to use when placed in world, this prefab in turn should have a reference to this InventoryItem")]
	public GameObject prefab;
}



