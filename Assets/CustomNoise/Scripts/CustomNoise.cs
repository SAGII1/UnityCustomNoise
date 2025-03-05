using noise;
using noise.module;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomNoise : MonoBehaviour
{
	[Header("Noise Parameters")]
	[SerializeField] private List<Noise> _noises;

	[Header("Debug Display")]
	[SerializeField] private int _debugWidth;
	[SerializeField] private int _debugHeight;

	private SpriteRenderer _spriteRenderer;
	private Texture2D _texture;
	private List<Module> _modules;

	private void Awake()
	{
		Init();

		//Destoy debug texture
		_spriteRenderer.sprite = null;
		_spriteRenderer.enabled = false;
		Destroy(_spriteRenderer);
	}

	private void Init()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();

		_modules = new List<Module>();

		Noise lNoise;

		Perlin lPerlin;
		Voronoi lVoronoi;
		Billow lBillow;
		RidgedMulti lRidged;

		for (int i = 0; i < _noises.Count; i++)
		{
			lNoise  = _noises[i];

			switch (lNoise.NoiseType)
			{
				case ENoiseType.PERLIN:
					lPerlin = new Perlin();

					lPerlin.Frequency = lNoise.Frequency;
					lPerlin.Seed = lNoise.RandomSeed ? GetRandomSeed() : 0;
					lPerlin.NoiseQuality = lNoise.NoiseQuality;
					lPerlin.Lacunarity = lNoise.Lacunarity;
					lPerlin.Persistence = lNoise.Persistence;
					lPerlin.OctaveCount = lNoise.OctaveCount;

					_modules.Add(lPerlin);

					break;
				case ENoiseType.VORONOI:
					lVoronoi = new Voronoi();

					lVoronoi.Frequency = lNoise.Frequency;
					lVoronoi.Seed = lNoise.RandomSeed ? GetRandomSeed() : 0;
					lVoronoi.Displacement = lNoise.Displacement;
					lVoronoi.EnableDistance = lNoise.EnableDistance;

					_modules.Add(lVoronoi);

					break;
				case ENoiseType.BILLOW:
					lBillow = new Billow();

					lBillow.Frequency = lNoise.Frequency;
					lBillow.Seed = lNoise.RandomSeed ? GetRandomSeed() : 0;
					lBillow.NoiseQuality = lNoise.NoiseQuality;
					lBillow.Lacunarity = lNoise.Lacunarity;
					lBillow.Persistence = lNoise.Persistence;
					lBillow.OctaveCount = lNoise.OctaveCount;

					_modules.Add(lBillow);

					break;
				case ENoiseType.RIDGED:
					lRidged = new RidgedMulti();

					lRidged.Frequency = lNoise.Frequency;
					lRidged.Seed = lNoise.RandomSeed ? GetRandomSeed() : 0;
					lRidged.NoiseQuality = lNoise.NoiseQuality;
					lRidged.Lacunarity = lNoise.Lacunarity;
					lRidged.OctaveCount = lNoise.OctaveCount;
					lRidged.PSpectralWeights = lNoise.PSpectralWeights;

					_modules.Add(lRidged);

					break;
				default:
					break;
			}
		}
	}

	public float GetValue(float pX, float pY, float pZ)
	{
		float lValue = 1f;
		Noise lNoise;
		Module lModule;

		for (int i = 0; i < _modules.Count; i++)
		{
			lNoise = _noises[i];
			lModule = _modules[i];

			pX *= lNoise.Scale;
			pY *= lNoise.Scale;

			switch (lNoise.ProcessType)
			{
				case EProcessType.ADD:
					lValue = lValue + (float)lModule.GetValue(pX, pY, pZ);
					break;
				case EProcessType.SUBTRACT:
					lValue = lValue - (float)lModule.GetValue(pX, pY, pZ);
					break;
				case EProcessType.MULTIPLY:
					lValue *= (float)lModule.GetValue(pX, pY, pZ);
					break;
				case EProcessType.DIVIDE:
					lValue = lValue / (float)lModule.GetValue(pX, pY, pZ);
					break;
				case EProcessType.POWER:
					lValue = MathF.Pow(lValue, (float)lModule.GetValue(pX, pY, pZ));
					break;
				case EProcessType.MAX:
					lValue = MathF.Max(lValue, (float)lModule.GetValue(pX, pY, pZ));
					break;
				case EProcessType.MIN:
					lValue = MathF.Min(lValue, (float)lModule.GetValue(pX, pY, pZ));
					break;
				default:
					break;
			}
		}

		return lValue;
	}

	public void DisplayNoise()
	{
		Init();

		DestroyImmediate(_texture);

		float lCoordX;
		float lCoordY;
		float lNoiseValue;
		Color lColor;

		_texture = new Texture2D(_debugWidth, _debugHeight);

		for (int y = 0; y < _debugHeight; y++)
		{
			for (int x = 0; x < _debugWidth; x++)
			{
				lCoordX = (float)x / _debugWidth;
				lCoordY = (float)y / _debugHeight;

				lNoiseValue = (float)(GetValue(lCoordX, lCoordY, 0f) + 1.0) / 2.0f;

				lColor = new Color(lNoiseValue, lNoiseValue, lNoiseValue);

				_texture.SetPixel(x, y, lColor);
			}
		}

		_texture.Apply();

		_spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0, 0, _debugWidth, _debugHeight), new Vector2(0.5f, 0.5f));
	}

	private int GetRandomSeed()
	{
		return Random.Range(0, 1000000);
	}
}

[Serializable]
public class Noise
{
	public ENoiseType NoiseType;
	public EProcessType ProcessType;

	//COMMON
	public float Frequency;
	public bool RandomSeed = true;
	public float Scale;

	//EXCEPT VORONOI
	public NoiseQuality NoiseQuality;
	public float Lacunarity;
	public int OctaveCount;
	public float Persistence; //EXCEPT RIDGED

	//VORONOI
	public float Displacement;
	public bool EnableDistance;

	//RIDGED
	public double[] PSpectralWeights;

}

public enum ENoiseType
{
	PERLIN,
	VORONOI,
	BILLOW,
	RIDGED
}

public enum EProcessType
{
	ADD,
	SUBTRACT,
	MULTIPLY,
	DIVIDE,
	POWER,
	MAX,
	MIN
}
