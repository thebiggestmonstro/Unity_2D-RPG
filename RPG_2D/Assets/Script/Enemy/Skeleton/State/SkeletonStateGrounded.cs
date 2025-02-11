using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateGrounded : EnemyState
{
    // Skeleton의 State이므로 SkeletonContoller가 필요함
    // 추가적으로 Player를 탐지해야 하므로 Player를 갖고 있음
    protected SkeletonController _skeletonController;
    protected Transform _player;

    // 생성자에서는 추가적으로 SkeletonController를 설정함
    public SkeletonStateGrounded(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController)
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    public override void Enter()
    {
        base.Enter();

        // 마찬가지로, PlayerManager로부터 PlayerController를 가져옴
        _player = PlayerManager._playerManagerInstance._playerController.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    // StateGrounded에서는 매 프레임마다
    // 플레이어가 탐지되었는지 또는 플레이어와 Skeleton의 거리가 2보다 작은지를 판단하여
    // State를 EngageState로 변환함
    public override void Update()
    {
        base.Update();

        if (_skeletonController.DoDetectPlayer() || Vector2.Distance(_skeletonController.transform.position, _player.position) < 2)
            _enemyStateMachine.ChangeState(_skeletonController._engageState);
    }
}
