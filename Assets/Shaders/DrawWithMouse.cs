using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public Transform _snowball;
    public Shader _drawShader;
    [Range(1,5)]
    public float _brushSize = 1;
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

    void Update()
    {
        var size  = _brushSize * _snowball.GetComponent<SnowballController>().size;
        if (Physics.Raycast(_snowball.position, -Vector3.up, out _groundHit, 1f, _layerMask))
        {
            _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
            _drawMaterial.SetFloat("_Strength", _brushStrength);
            _drawMaterial.SetFloat("_Size", _brushSize);
            RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatmap, temp);
            Graphics.Blit(temp, _splatmap, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
