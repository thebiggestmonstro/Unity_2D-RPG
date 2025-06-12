using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseCharacterStats>() != null)
            collision.GetComponent<BaseCharacterStats>().KillCharacter();
        else
            Destroy(collision.gameObject);
    }
}
