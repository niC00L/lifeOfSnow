using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTracks : MonoBehaviour
{
    public Transform _snowball;
    public Shader _drawShader;
    public Transform _spawn;
    [Range(1,10)]
    public float _brushSize = 5f;
    [Range(0,1)]
    public float _brushStrength = 1;

    int _layerMask;
    private RenderTexture _splatmap;
    private Material _snowMaterial, _drawMaterial;
    private RaycastHit _groundHit;

    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        _snowMaterial = GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatmap);
    }

    void FixedUpdate()
    {
        var size  = _brushSize * (_snowball.GetComponent<SnowballController>().size / 1.8f);
        float raycastDistance = _snowball.GetComponent<SnowballController>().size / 2 + 0.2f;
        if (Physics.Raycast(_snowball.position, -Vector3.up, out _groundHit, raycastDistance, _layerMask))
        {
            increaseSnowballSizeIfNeccessary();
            _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
            _drawMaterial.SetFloat("_Strength", _brushStrength);
            _drawMaterial.SetFloat("_Size", size);
            RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatmap, temp);
            Graphics.Blit(temp, _splatmap, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);            
        }
    }

    private void increaseSnowballSizeIfNeccessary()
    {
        var size = Mathf.Max(1, _brushSize * (_snowball.GetComponent<SnowballController>().size / 1.8f) * 5);
        var halfsize = size / 2;
        var ff = _groundHit.textureCoord * 1024;
        Texture2D fn = new Texture2D((int)size, (int)size);
        RenderTexture.active = _splatmap;
        fn.ReadPixels(new Rect(ff.x - (size/2), 1024 - (ff.y - (size / 2)), fn.width, fn.height), 0, 0);
        fn.Apply();
        RenderTexture.active = null;

        Color pi = AverageColorFromTexture(fn);
        if (pi.r < 0.3)
        {
            _spawn.GetComponent<SnowballMover>()._increaseBallSize();
        } 
    }

    private static Color AverageColorFromTexture(Texture2D tex)
    {

        Color[] texColors = tex.GetPixels();

        int total = texColors.Length;

        float r = 0;
        float g = 0;
        float b = 0;
        float a = 0;

        for (int i = 0; i < total; i++)
        {

            r += texColors[i].r;

            g += texColors[i].g;

            b += texColors[i].b;

            a += texColors[i].a;

        }

        return new Color(r / total, g / total, b / total, a / total);

    }
}
