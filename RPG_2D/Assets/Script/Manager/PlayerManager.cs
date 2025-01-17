using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _playerManagerInstance;
    public PlayerController _playerController;

    private void Awake()
    {
        // _playerManagerInstance�� �̹� �����ϴٸ�, �ش� ������Ʈ�� ���� ���� ������Ʈ �ı�
        if (_playerManagerInstance != null)
            Destroy(_playerManagerInstance.gameObject);
        
        // _playerManagerInstance�� �缳��
        _playerManagerInstance = this;
    }
}
