using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Transform _craftSlotParent;
    [SerializeField]
    private GameObject _craftSlotPrefab;
    
    [SerializeField]
    private List<ItemData_Equipment> _equipmentsToCraft;

    void Start()
    {
        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetupCraftList();
        SetupDefaultCraftWindow();
    }

    public void SetupCraftList()
    {
        for (int i = 0; i < _craftSlotParent.childCount; i++)
        {
            Destroy(_craftSlotParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < _equipmentsToCraft.Count; i++)
        {
            GameObject newSlot = Instantiate(_craftSlotPrefab, _craftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetupCraftSlot(_equipmentsToCraft[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetupCraftList();
    }

    public void SetupDefaultCraftWindow()
    {
        if (_equipmentsToCraft[0] != null)
            GetComponentInParent<UIManager>()._craftWindow.SetupCraftWindow(_equipmentsToCraft[0]);
    }
}
