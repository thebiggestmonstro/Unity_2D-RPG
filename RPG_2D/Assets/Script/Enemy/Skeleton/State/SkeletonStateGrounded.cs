using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateGrounded : EnemyState
{
    // Skeleton�� State�̹Ƿ� SkeletonContoller�� �ʿ���
    // �߰������� Player�� Ž���ؾ� �ϹǷ� Player�� ���� ����
    protected SkeletonController _skeletonController;
    protected Transform _player;

    // �����ڿ����� �߰������� SkeletonController�� ������
    public SkeletonStateGrounded(EnemyController enemyBaseController, EnemyStateMachine enemyStateMachine, string animatorBoolParamName, SkeletonController enemyController)
        : base(enemyBaseController, enemyStateMachine, animatorBoolParamName)
    {
        this._skeletonController = enemyController;
    }

    // StateGrouned�� ���� Player�� Ž���Ͽ� ������ ����
    // �׷��� GameObject.Find�� ���� ������ �ƴϹǷ� �Ŀ� ������ ����
    public override void Enter()
    {
        base.Enter();
        _player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    // StateGrounded������ �� �����Ӹ���
    // �÷��̾ Ž���Ǿ����� �Ǵ� �÷��̾�� Skeleton�� �Ÿ��� 2���� �������� �Ǵ��Ͽ�
    // State�� EngageState�� ��ȯ��
    public override void Update()
    {
        base.Update();

        if (_skeletonController.DoDetectPlayer() || Vector2.Distance(_skeletonController.transform.position, _player.position) < 2)
            _enemyStateMachine.ChangeState(_skeletonController._engageState);
    }
}
