using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffectController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [Header("Flash FX")]
    [SerializeField]
    private float _flashDuration;
    private Material _defaultMaterial;
    [SerializeField]
    private Material _hitMaterial;

    [Header("Aliment Colors")]
    [SerializeField]
    private Color[] _ignitedColor;
    [SerializeField]
    private Color[] _freezedColor;
    [SerializeField]
    private Color[] _shockedColor;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public void MakeTransparent(bool _isTransparent)
    {
        if (_isTransparent)
            _spriteRenderer.color = Color.clear;
        else
            _spriteRenderer.color = Color.white;
    }

    public IEnumerator DoMakeFlashFX()
    {
        _spriteRenderer.material = _hitMaterial;
        Color currentClor = _spriteRenderer.color;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.color = currentClor;
        _spriteRenderer.material = _defaultMaterial;
    }

    void RedColorBlink()
    { 
        if(_spriteRenderer.color != Color.white)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.red;
    }

    void CancelColorChange()
    {
        CancelInvoke();
        _spriteRenderer.color= Color.white;
    }

    public void PaintIgnitedColorFX(float seconds)
    {
        InvokeRepeating("MakeIgnitedColorFX", 0, 0.3f);
        Invoke("CancelColorChange", seconds);
    }

    private void MakeIgnitedColorFX()
    { 
        if(_spriteRenderer.color != _ignitedColor[0])
            _spriteRenderer.color=_ignitedColor[0];
        else
            _spriteRenderer.color = _ignitedColor[1];
    }

    public void PaintFreezedColorFX(float seconds)
    {
        InvokeRepeating("MakeFreezedColorFX", 0, 0.3f);
        Invoke("CancelColorChange", seconds);
    }

    private void MakeFreezedColorFX()
    {
        if (_spriteRenderer.color != _freezedColor[0])
            _spriteRenderer.color = _freezedColor[0];
        else
            _spriteRenderer.color = _freezedColor[1];
    }

    public void PaintShockedColorFX(float seconds)
    {
        InvokeRepeating("MakeShockedColorFX", 0, 0.3f);
        Invoke("CancelColorChange", seconds);
    }

    private void MakeShockedColorFX()
    {
        if (_spriteRenderer.color != _shockedColor[0])
            _spriteRenderer.color = _shockedColor[0];
        else
            _spriteRenderer.color = _shockedColor[1];
    }
}
