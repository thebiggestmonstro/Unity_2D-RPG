using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_SurrounedArea : MonoBehaviour
{
    [SerializeField] 
    private int _areaSoundIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
            AudioManager._audioManagerInstance.PlaySFX(_areaSoundIndex, null);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
            AudioManager._audioManagerInstance.StopSFXWithDelay(_areaSoundIndex);
    }
}
