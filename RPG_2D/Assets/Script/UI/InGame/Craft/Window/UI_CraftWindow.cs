using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemDescription;
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private Button _craftButton;

    [SerializeField]
    private Image[] _materialsImage;

    public void SetupCraftWindow(ItemData_Equipment data)
    {
        _craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < _materialsImage.Length; i++)
        {
            _materialsImage[i].color = Color.clear;
            _materialsImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;   
        }

        for (int i = 0; i < data._craftMaterials.Count; i++)
        {
            _materialsImage[i].sprite = data._craftMaterials[i]._itemData._itemIcon;
            _materialsImage[i].color = Color.white;

            TextMeshProUGUI materialText = _materialsImage[i].GetComponentInChildren<TextMeshProUGUI>();
            materialText.text = data._craftMaterials[i]._stackSize.ToString();
            materialText.color = Color.white;
        }

        _itemImage.sprite = data._itemIcon;
        _itemName.text = data._itemName;
        _itemDescription.text = data.GetDescription();

        _craftButton.onClick.AddListener(() => InventoryManager._inventoryManagerInstance.CraftEquipment(data, data._craftMaterials));
    }
}
