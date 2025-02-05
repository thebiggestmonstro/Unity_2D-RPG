using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{ 
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SkillThrowingSword : SkillTemplate
{
    public SwordType _swordType = SwordType.Regular;

    [Header("Skill Info")]
    [SerializeField]
    private GameObject _swordPrefab;
    [SerializeField]
    private Vector2 _launchForce;
    [SerializeField]
    private float _swordGravity;
    [SerializeField]
    private float _freezeTimeDuration;
    [SerializeField]
    private float _returnSpeed;

    private Vector2 _finalDirection;

    [Header("Aim Dots")]
    [SerializeField]
    private int _numberOfDots;
    [SerializeField]
    private float _spaceBetweenDots;
    [SerializeField]
    private GameObject _dotPrefab;
    [SerializeField]
    private Transform _dotsParent;

    private GameObject[] _dots;

    [Header("Bounce Info")]
    [SerializeField]
    private int _bounceAmount;
    [SerializeField]
    private float _bounceGravity;
    [SerializeField]
    private float _bounceSpeed;

    [Header("Pierce Info")]
    [SerializeField]
    private int _pierceAmount;
    [SerializeField]
    private float _pierceGravity;

    [Header("Spin Info")]
    [SerializeField]
    private int _maxTravelDistance;
    [SerializeField]
    private float _spinDuration;
    [SerializeField]
    private float _spinGravity;
    [SerializeField]
    private float _hitCooldown = 0.35f;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetupSwordGravity();
    }

    private void SetupSwordGravity()
    {
        if (_swordType == SwordType.Bounce)
            _swordGravity = _bounceGravity;
        else if(_swordType == SwordType.Pierce)
            _swordGravity = _pierceGravity;
        else if(_swordType == SwordType.Spin)
            _swordGravity = _spinGravity;
    }

    protected override void Update()
    {
        if (_playerController._isThrowSwordClicked == false)
        {
            _finalDirection = new Vector2(
                    MakeAimDirection().normalized.x * _launchForce.x,
                    MakeAimDirection().normalized.y * _launchForce.y
                );
        }

        if (_playerController._isThrowSwordClicked)
        {
            for (int i = 0; i < _dots.Length; i++)
            {
                _dots[i].transform.position = SetDotsPosition(i * _spaceBetweenDots);  
            }
        }
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(
            _swordPrefab,
            _playerController.transform.position,
            transform.rotation
            );

        SkillThrowingSwordController throwingSwordController = newSword.GetComponent<SkillThrowingSwordController>();

        if (_swordType == SwordType.Bounce)
            throwingSwordController.SetupBounceSword(true, _bounceAmount, _bounceSpeed);
        else if (_swordType == SwordType.Pierce)
            throwingSwordController.SetupPierceSword(_pierceAmount);
        else if (_swordType == SwordType.Spin)
            throwingSwordController.SetupSpinSword(true, _maxTravelDistance, _spinDuration, _hitCooldown);
        

        throwingSwordController.SetUpSword(_finalDirection, _swordGravity, _playerController, _freezeTimeDuration, _returnSpeed);

        _playerController.AssignNewSword(newSword);

        DotsActive(false);
    }

    public Vector2 MakeAimDirection()
    {
        Vector2 playerPosition = _playerController.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < _dots.Length; i++)
        { 
            _dots[i].SetActive(isActive);
        }
    }


    private void GenerateDots()
    {
        _dots = new GameObject[_numberOfDots];
        for (int i = 0; i < _numberOfDots; i++)
        {
            _dots[i] = Instantiate(_dotPrefab, _playerController.transform.position, Quaternion.identity, _dotsParent);
            _dots[i].SetActive(false);
        }
    }

    private Vector2 SetDotsPosition(float arg)
    { 
        Vector2 position = (Vector2)_playerController.transform.position + 
            new Vector2(MakeAimDirection().normalized.x * _launchForce.x, MakeAimDirection().normalized.y * _launchForce.y) * arg + 
            0.5f * (Physics2D.gravity * _swordGravity) * (arg * arg);

        return position;
    }
}
