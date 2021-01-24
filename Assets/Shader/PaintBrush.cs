using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PaintBrush : MonoBehaviour{
    public Camera camera;
    public Shader brushShader;
    public Color[] listPaintCalculator;
    public TextMeshProUGUI paintText;
    public GameObject restartButton;

    private RenderTexture _splatMap;
    private Material _groundMat, _drawMat;
    private RaycastHit _hit;
    private float _cdForColorLoop = 1f;
    private bool _boolForColorLoop;
    private int _ratioOfPainting;

    private void Start(){
        _drawMat = new Material(brushShader);
        _drawMat.SetVector("_Color", Color.red);

        _groundMat = GetComponent<MeshRenderer>().material;
        _splatMap = new RenderTexture(512,512,0,RenderTextureFormat.ARGBFloat);
        _groundMat.SetTexture("_Splat", _splatMap);
    }

    private void Update(){
        //Painting();
        if (_ratioOfPainting >= 100){
            restartButton.SetActive(true);
        }
    }

    private void OnMouseDrag(){
        Painting();
    }

    private void Painting(){
        if (Physics.Raycast(camera.ScreenPointToRay(Input.GetTouch(0).position), out _hit))//telefon için
        //if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out _hit))//bilgisayar için
        {
            _drawMat.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0,0));
            RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0,
                RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_splatMap, temp);
            Graphics.Blit(temp,_splatMap,_drawMat);
            RenderTexture.ReleaseTemporary(temp);
            _boolForColorLoop = true;

        }
        if (_boolForColorLoop){
            _cdForColorLoop -= Time.deltaTime;
            if (_cdForColorLoop<=0){
                _ratioOfPainting = PaintingCalculator();
                if (_ratioOfPainting>=99){
                    _ratioOfPainting = 100;
                }
                paintText.text = "PAINT %" + _ratioOfPainting;
                _cdForColorLoop = 1f;
                _boolForColorLoop = false;
            }
        }
    }

    private int PaintingCalculator(){
        Texture2D tex = new Texture2D(512,512,TextureFormat.RGB24, false);
        RenderTexture.active = _splatMap;
        tex.ReadPixels(new Rect(0,0,_splatMap.width,_splatMap.height),0,0 );
        listPaintCalculator = tex.GetPixels();
        int redColorNumber = 0;
        foreach (var x in listPaintCalculator){
            if (x == Color.red){
                redColorNumber += 1;
            }
        }

        return (int)((redColorNumber * 100f) / listPaintCalculator.Length);
    }
}