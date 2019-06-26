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
        var size  = _brushSize * _snowball.GetComponent<SnowballController>().size;
        float raycastDistance = _snowball.GetComponent<SnowballController>().size / 2 + 0.2f;
        if (Physics.Raycast(_snowball.position, -Vector3.up, out _groundHit, raycastDistance, _layerMask))
        {
            _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
            _drawMaterial.SetFloat("_Strength", _brushStrength);
            _drawMaterial.SetFloat("_Size", size);
            RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatmap, temp);
            Graphics.Blit(temp, _splatmap, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);

            //RenderTexture temppix = RenderTexture.GetTemporary(1, 1, 0, RenderTextureFormat.ARGBFloat);
            //Graphics.Blit(_splatmap, temppix, new Vector2(1, 1), _groundHit.textureCoord);
            //Texture2D fn = new Texture2D(1,1);
            //RenderTexture.active = temppix;
            //fn.ReadPixels(new Rect(0, 0, 1, 1), 0, 0);
            //fn.Apply();
            //RenderTexture.active = null;

            //Color pi = fn.GetPixel(0, 0);
            //if (pi == Color.black)
            //{
                _spawn.GetComponent<SnowballMover>()._increaseBallSize();
            //}
        }
    }
}
