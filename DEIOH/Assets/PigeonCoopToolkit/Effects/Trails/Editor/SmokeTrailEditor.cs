using UnityEditor;


namespace PigeonCoopToolkit.Effects.Trails.Editor
{
	[CustomEditor(typeof(SmokeTrail))]
	[CanEditMultipleObjects]
	public class SmokeTrailEditor : TrailEditor_Base
	{
		protected override void DrawTrailSpecificGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomForceScale"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("MinVertexDistance"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxNumberOfPoints"));

			// DEIOH custom
			EditorGUILayout.PropertyField(serializedObject.FindProperty("usePerlinNoise"));
			if (((SmokeTrail)serializedObject.targetObject).usePerlinNoise)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("noiseScale"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("noiseSpeed"));

			}
			EditorGUILayout.PropertyField(serializedObject.FindProperty("useEjectionForce"));
			if (((SmokeTrail)serializedObject.targetObject).useEjectionForce)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("ejectionForceValue"));
			}
		}
	}
}
