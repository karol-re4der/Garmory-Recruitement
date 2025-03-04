using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform BackpackGrid;
    public Transform CharacterGrid;

    public GameObject ItemSlotPrefab;

    private List<ItemSlot> _itemSlots = new List<ItemSlot>();

    void Start()
    {
        FillWithSpaces();
        FillWithItems();
    }


    public void FillWithSpaces()
    {
        for(int i = 0; i<100; i++)
        {
            ItemSlot newSpace = GameObject.Instantiate(ItemSlotPrefab, BackpackGrid).GetComponent<ItemSlot>();

            _itemSlots.Add(newSpace);
        }
    }

    public void CreateSlot()
    {

    }

    public void FillWithItems()
    {
        for(int i = 0; i<_itemSlots.Count-1; i++)
        {
            Item newItem = new Item();
            newItem.Randomize();
            _itemSlots[i].LinkItem(newItem);
        }
    }
}
