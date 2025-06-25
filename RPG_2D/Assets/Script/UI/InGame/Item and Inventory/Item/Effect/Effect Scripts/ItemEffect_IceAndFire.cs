using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Ice And Fire")]
public class ItemEffect_IceAndFire : ItemEffect
{
    [SerializeField] 
    private GameObject _iceAndFirePrefab;
    [SerializeField] 
    private float _xVelocity;

    public override void ExecuteEffect(Transform _respawnPosition)
    {
        PlayerController player = PlayerManager._playerManagerInstance._playerController;

        bool isThirdAttack = player._priamaryAttackState._comboCounter == 2;

        if (isThirdAttack)
        {
            GameObject newIceAndFire = Instantiate(_iceAndFirePrefab, _respawnPosition.position, player.transform.rotation);
            newIceAndFire.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(_xVelocity * player._facingDir, 0);

            Destroy(newIceAndFire, 10);
        }
    }
}
