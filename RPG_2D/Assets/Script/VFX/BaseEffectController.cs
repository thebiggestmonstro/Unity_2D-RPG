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

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public IEnumerator DoMakeFlashFX()
    {
        _spriteRenderer.material = _hitMaterial;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.material = _defaultMaterial;
    }

    void RedColorBlink()
    { 
        if(_spriteRenderer.color != Color.white)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.red;
    }

    void CancelRedColorBlink()
    {
        CancelInvoke();
        _spriteRenderer.color= Color.white;
    }
}
