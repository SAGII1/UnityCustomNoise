# Unity Custom Noise

I made a basic Unity custom noise tool, easy to use for procedural generation, and easy to customize.

## How to use

In the NoiseInstance prefab, you can find a component named "CustomNoise".
Inside the CustomNoise script, a list of noises is used to generate your noise.
To watch what your noise looks like, there is a debug size you can set and an editor button to generate the texture.

![Tuto0](https://github.com/SAGII1/UnityCustomNoise/blob/main/media/UnityCustomNoiseTuto0.png)

With iterations, you can achieve interesting noise like this one.

![Tuto1](https://github.com/SAGII1/UnityCustomNoise/blob/main/media/UnityCustomNoiseTuto1.png)

Finally, your CustomNoise has a public function "GetValue" made for any procedural generation application.
