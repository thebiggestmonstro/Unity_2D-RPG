using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{ 
    Material,
    Equipment,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public string _itemName;
    public Sprite _itemIcon;
    public string _itemId;

    [Range(0, 100)]
    public float _dropChance;

    protected StringBuilder _stringBuilder = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        _itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
