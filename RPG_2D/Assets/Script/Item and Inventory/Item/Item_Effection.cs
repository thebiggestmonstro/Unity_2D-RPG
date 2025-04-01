using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect")]
public class Item_Effection : ScriptableObject
{
    public virtual void ExecuteEffect()
    {
        Debug.Log("Effect Executed");
    }
}
