using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_CheckPoint : MonoBehaviour
{
    private Animator _animator;
    public string _checkPointId;
    public bool _isActivating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()
    {
        _checkPointId = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        if (_isActivating == false)
            AudioManager._audioManagerInstance.PlaySFX(5, transform);

        _isActivating = true;
        _animator.SetBool("Active", true);
    }
}
