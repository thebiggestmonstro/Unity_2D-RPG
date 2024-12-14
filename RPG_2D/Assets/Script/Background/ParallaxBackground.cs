using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] 
    private float _parallaxEffect;
    private float _xPosition;
    private GameObject _camera;
    private float _spriteLength;

    void Start()
    {
        _camera = GameObject.Find("Main Camera");

        _spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
        _xPosition = gameObject.transform.position.x;
    }

    void Update()
    {
        float _distanceToMoved = _camera.transform.position.x * (1 - _parallaxEffect);
        float _distanceToMove = _camera.transform.position.x * _parallaxEffect;

        gameObject.transform.position = new Vector3(_xPosition + _distanceToMove, gameObject.transform.position.y);

        if (_distanceToMoved > _xPosition + _spriteLength)
            _xPosition += _spriteLength;
        else if (_distanceToMoved < _xPosition - _spriteLength)
            _xPosition -= _spriteLength;
    }
}
