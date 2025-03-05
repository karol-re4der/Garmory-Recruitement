using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    public Transform BackpackGrid;
    public Transform CharacterGrid;

    public List<ItemSlot> EquipementSlots;

    public GameObject ItemSlotPrefab;

    private List<ItemSlot> _itemSlots = new List<ItemSlot>();

    void Start()
    {
        FillWithSpaces();
        FillWithItems();
    }

    public void FillWithSpaces()
    {
        for(int i = 0; i<20; i++)
        {
            ItemSlot newSpace = GameObject.Instantiate(ItemSlotPrefab, BackpackGrid).GetComponent<ItemSlot>();

            _itemSlots.Add(newSpace);
        }
    }

    public void FillWithItems()
    {
        for(int i = 0; i<_itemSlots.Count-10; i++)
        {
            Item newItem = new Item();
            newItem.Randomize();
            _itemSlots[i].LinkItem(newItem);
        }
    }

    public ItemSlot FindDestinationForCategory(ItemCategory cat)
    {
        ItemSlot destination = EquipementSlots.First(x => x.SlotCategory == cat);

        if (destination == null)
        {
            throw new Exception("Not slot found for this type of equipement!");
        }

        return destination;
    }

    public ItemSlot FindEmptySlot()
    {
        return _itemSlots.First(x => x.Item == null);
    }
}
