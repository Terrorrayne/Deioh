// Creates a simple wizard that lets you create a Light GameObject
// or if the user clicks in "Apply", it will set the color of the currently
// object selected to red

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class InventoryItemCreationWizard : ScriptableWizard
{
	public enum ItemType
	{
		generic, weapon_melee, weapon_ranged, consumable
	}

	public string itemName = "";
	public string displayName = "";
	[Tooltip("Image used in the inventory window")]
	public Sprite itemSprite;
	[Tooltip("which template class to extend item behavior from. select generic if unsure")]
	public ItemType itemType = ItemType.generic;

	string stringbean =
		"using System;" + System.Environment.NewLine +
		"using UnityEngine;" + System.Environment.NewLine +
		System.Environment.NewLine +
		"public class ";

	public TextAsset template;

	[MenuItem("DIEOH/Create New Inventory Item")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard<InventoryItemCreationWizard>("Create Inventory Item", "Create");
		//If you don't want to use the secondary button simply leave it out:
		//ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
	}

	void OnWizardCreate()
	{
		//create new InventoryItem
		//create new ItemBehavior extention
		//create new equippable and world prefabs
		//apply ib to equippable
		//do somethign to world prefab
		//assign linkage to everyhting


		//InventoryItem item = new InventoryItem();
		//item.displayName = displayName;
		//item.itemSprite = itemSprite;


		// first decide on names/make sure there aren't duplicates

		// create prefab

		InventoryItem myItem = ScriptableObjectUtility.CreateInventoryItemAsset(displayName);
		myItem.displayName = displayName != "" ? displayName : itemName;
		myItem.itemSprite = itemSprite;

		// create object that will be our prefab
		GameObject prefab = new GameObject(itemName);
		// add all necessary components and stuff

		string myTypeString = WriteScript();
		Debug.Log(myTypeString);
		AssetDatabase.Refresh();
		Type mytype = GetType(myTypeString); // Type.GetType(myTypeString);
		Debug.Log(mytype);
		prefab.AddComponent(mytype);
		// save prefab

		//myItem.prefab = PrefabUtility.CreatePrefab("Assets/Prefabs/Items/" + itemName + ".prefab", prefab);

		//GameObject prefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/Items/" + itemName + ".prefab") as GameObject;

		AssetDatabase.Refresh(); // update so unity shows the file we just created





		//ItemBehavior itemBehavior = Activator.CreateInstance(Type.GetType(WriteScript())) as ItemBehavior;
	}

	void OnWizardUpdate()
	{
		//helpString = "Please set the color of the light!";
	}

	string WriteScript() // your god kneels before my shitty coding skillz
	{
		// get our current directory
		// set destination folder
		// fucking, name the goddamned file
		// read teh template
		// replace class name with goddamned file name
		// that should be it?

		string path = Application.dataPath + "/Scripts/ItemBehaviors";
		path += "/" + "IB_" + itemName + ".cs";
		Debug.Log(path);
		// write a new file named "itemname.cs" with contents of template
		string outputFile = stringbean + "IB_" + itemName + template.text;

		System.IO.File.WriteAllText(path, outputFile);
		AssetDatabase.Refresh(); // update so unity shows the file we just created

		// return the name of our new class
		return "IB_" + itemName;
	}

	public static Type GetType(string typeName)
	{
		var type = Type.GetType(typeName);
		if (type != null) return type;
		foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
		{
			type = a.GetType(typeName);
			if (type != null)
				return type;
		}
		return null;
	}
}

public static class ScriptableObjectUtility
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static InventoryItem CreateInventoryItemAsset(string name)
	{
		InventoryItem asset = ScriptableObject.CreateInstance<InventoryItem>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets/InventoryItems";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
		return asset;
	}
}