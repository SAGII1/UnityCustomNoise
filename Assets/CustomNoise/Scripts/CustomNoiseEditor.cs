using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomNoise))]
public class CustomNoiseEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CustomNoise lCustomNoise = (CustomNoise)target;

		if (GUILayout.Button("Generate Noise Texture"))
		{
			lCustomNoise.DisplayNoise();
		}
	}
}
