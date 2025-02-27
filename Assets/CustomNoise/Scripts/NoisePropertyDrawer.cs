using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Noise))]
public class NoisePropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pPosition, SerializedProperty pProperty, GUIContent pLabel)
	{
		EditorGUI.BeginProperty(pPosition, pLabel, pProperty);

		//COMMON
		SerializedProperty lNoiseType = pProperty.FindPropertyRelative("NoiseType");
		SerializedProperty lProcessType = pProperty.FindPropertyRelative("ProcessType");
		SerializedProperty lFrequency = pProperty.FindPropertyRelative("Frequency");
		SerializedProperty lSeed = pProperty.FindPropertyRelative("RandomSeed");
		SerializedProperty lScale = pProperty.FindPropertyRelative("Scale");

		//EXCEPT VORONOI
		SerializedProperty lNoiseQuality = pProperty.FindPropertyRelative("NoiseQuality");
		SerializedProperty lLacunarity = pProperty.FindPropertyRelative("Lacunarity");
		SerializedProperty lPersistence = pProperty.FindPropertyRelative("Persistence");
		SerializedProperty lOctaveCount = pProperty.FindPropertyRelative("OctaveCount");

		//VORONOI
		SerializedProperty lDisplacement = pProperty.FindPropertyRelative("Displacement");
		SerializedProperty lEnableDistance = pProperty.FindPropertyRelative("EnableDistance");

		//RIDGED
		SerializedProperty lSpectralWeights = pProperty.FindPropertyRelative("PSpectralWeights");

		Rect lEnumRect = new Rect(pPosition.x, pPosition.y, pPosition.width, EditorGUIUtility.singleLineHeight);

		float lHeightStep = 20f;

		EditorGUI.PropertyField(lEnumRect, lNoiseType);
		lEnumRect.y += lHeightStep;

		EditorGUI.PropertyField(lEnumRect, lProcessType);
		lEnumRect.y += lHeightStep;

		EditorGUI.PropertyField(lEnumRect, lFrequency);
		lEnumRect.y += lHeightStep;

		EditorGUI.PropertyField(lEnumRect, lSeed);
		lEnumRect.y += lHeightStep;

		EditorGUI.PropertyField(lEnumRect, lScale);
		lEnumRect.y += lHeightStep;

		switch ((ENoiseType)lNoiseType.enumValueIndex)
		{
			case ENoiseType.PERLIN:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lPersistence);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y += lHeightStep;

				break;
			case ENoiseType.VORONOI:
				EditorGUI.PropertyField(lEnumRect, lDisplacement);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lEnableDistance);
				lEnumRect.y += lHeightStep;

				break;
			case ENoiseType.BILLOW:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lPersistence);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y += lHeightStep;

				break;
			case ENoiseType.RIDGED:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y += lHeightStep;

				EditorGUI.PropertyField(lEnumRect, lSpectralWeights);
				lEnumRect.y += lHeightStep;

				break;
			default:
				break;
		}

		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 16f;
	}
}
