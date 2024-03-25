using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizePerlin : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _perlinNoiseScale = 1;

    private void Awake()
    {
        if (_texture == null)
            _texture = new Texture2D(64, 64);

        _renderer.material.mainTexture = _texture;
    }

    private void Update()
    {
        for (float x = 0; x < 64; x++)
        {
            for (float y = 0; y < 64; y++)
            {
                Color color = Color.Lerp(Color.black, Color.white, Mathf.PerlinNoise(x * _perlinNoiseScale, y * _perlinNoiseScale));
               _texture.SetPixel((int)x, (int)y, color);
            }
        }

        _texture.Apply();
    }
}
