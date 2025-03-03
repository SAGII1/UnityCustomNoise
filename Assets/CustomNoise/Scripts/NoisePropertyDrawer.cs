using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Noise))]
public class NoisePropertyDrawer : PropertyDrawer
{
	private float _rectOffsetPerElement = 20f;
	private Dictionary<string, int> _elementCounts = new Dictionary<string, int>();

	public override void OnGUI(Rect pPosition, SerializedProperty pProperty, GUIContent pLabel)
	{
		EditorGUI.BeginProperty(pPosition, pLabel, pProperty);

		string lPropertyPath = pProperty.propertyPath;

		if (!_elementCounts.ContainsKey(lPropertyPath))
		{
			_elementCounts[lPropertyPath] = 1;
		}

		int lElementCount = 2;

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

		EditorGUI.PropertyField(lEnumRect, lNoiseType);
		lEnumRect.y +=  _rectOffsetPerElement;

		EditorGUI.PropertyField(lEnumRect, lProcessType);
		lEnumRect.y +=  _rectOffsetPerElement;

		EditorGUI.PropertyField(lEnumRect, lFrequency);
		lEnumRect.y +=  _rectOffsetPerElement;

		EditorGUI.PropertyField(lEnumRect, lSeed);
		lEnumRect.y +=  _rectOffsetPerElement;

		EditorGUI.PropertyField(lEnumRect, lScale);
		lEnumRect.y +=  _rectOffsetPerElement;

		lElementCount += 5;

		switch ((ENoiseType)lNoiseType.enumValueIndex)
		{
			case ENoiseType.PERLIN:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lPersistence);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y +=  _rectOffsetPerElement;

				lElementCount += 4;

				break;
			case ENoiseType.VORONOI:
				EditorGUI.PropertyField(lEnumRect, lDisplacement);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lEnableDistance);
				lEnumRect.y +=  _rectOffsetPerElement;

				lElementCount += 2;

				break;
			case ENoiseType.BILLOW:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lPersistence);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y +=  _rectOffsetPerElement;

				lElementCount += 4;

				break;
			case ENoiseType.RIDGED:
				EditorGUI.PropertyField(lEnumRect, lNoiseQuality);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lLacunarity);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lOctaveCount);
				lEnumRect.y +=  _rectOffsetPerElement;

				EditorGUI.PropertyField(lEnumRect, lSpectralWeights);
				lEnumRect.y +=  _rectOffsetPerElement;

				lElementCount += 7;

				if (lSpectralWeights.isExpanded)
				{
					lElementCount += lSpectralWeights.arraySize;
				}

				break;
			default:
				break;
		}

		_elementCounts[lPropertyPath] = lElementCount;

		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty pProperty, GUIContent pLabel)
	{
		string lPropertyPath = pProperty.propertyPath;

		if (_elementCounts.ContainsKey(lPropertyPath))
		{
			return EditorGUIUtility.singleLineHeight * _elementCounts[lPropertyPath];
		}
		return EditorGUIUtility.singleLineHeight;
	}
}
