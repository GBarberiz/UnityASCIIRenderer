using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class ASCIIController : MonoBehaviour
{
    [SerializeField]
    private char[] _sortedCharacters = new char[] { ' ', '.', ':', '-', '=', '+', '*', '#', 'P', '@' };
    [SerializeField]
    private RenderTexture _mainTex;
    private Texture2D _main2dTex;
    [SerializeField]
    private AnimationCurve _animCurve;
    [SerializeField]
    private TextMeshProUGUI textmesh;
    
    private string _result;

    Color color;
    float grayScale;
    private void Awake()
    {
        _mainTex = Camera.main.targetTexture;
       
    }
    void Update()
    {
        RenderTexture.active = _mainTex;

        _main2dTex = new Texture2D(_mainTex.width, _mainTex.height, TextureFormat.RGB24, false);
        _main2dTex.ReadPixels(new Rect(0, 0, _mainTex.width, _mainTex.height), 0, 0);
        
        _main2dTex.Apply();
        
        _result = ParseTexture(_main2dTex);
        textmesh.text = _result;
        RenderTexture.active = null;
    }

    private string ParseTexture(Texture2D texture)
    {
        StringBuilder sb = new();
        
        for (int y = texture.height; y  >= 0; y--)
        {
            for (int x = 0; x < texture.height; x++)
            {
                color = texture.GetPixel(x, y);
                grayScale = _animCurve.Evaluate(color.grayscale);
                //grayScale = color.grayscale;
                sb.Append((GetCharFromGrayScale(grayScale)));
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }

    private char GetCharFromGrayScale(float grayScale)
    {
        grayScale = Mathf.Clamp01(grayScale);
        return _sortedCharacters[(int)(grayScale * _sortedCharacters.Length)];
    }
    
}
