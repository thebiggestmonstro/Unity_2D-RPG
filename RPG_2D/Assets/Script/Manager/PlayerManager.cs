using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _playerManagerInstance;
    public PlayerController _playerController;

    private void Awake()
    {
        // _playerManagerInstance가 이미 존재하다면, 해당 컴포넌트를 지닌 게임 오브젝트 파괴
        if (_playerManagerInstance != null)
            Destroy(_playerManagerInstance.gameObject);
        
        // _playerManagerInstance를 재설정
        _playerManagerInstance = this;
    }
}
