using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public Transform BackpackGrid;
    public Transform CharacterGrid;
    public ItemSlot WeaponSlot;
    public ItemSlot ArmorSlot;
    public ItemSlot HelmetSlot;
    public ItemSlot RingSlot;
    public ItemSlot NecklaceSlot;
    public ItemSlot BootsSlot;


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

    public void FillWithItems()
    {
        for(int i = 0; i<_itemSlots.Count-1; i++)
        {
            Item newItem = new Item();
            newItem.Randomize();
            _itemSlots[i].LinkItem(newItem);
        }
    }

    public void EquipFromSlot(ItemSlot slot)
    {
        ItemSlot targetSlot = null;
        switch (slot.Item.Category)
        {
            case ItemCategory.Weapon:
                targetSlot = WeaponSlot;
                break;
            case ItemCategory.Armor:
                targetSlot = ArmorSlot;
                break;
            case ItemCategory.Helmet:
                targetSlot = HelmetSlot;
                break;
            case ItemCategory.Boots:
                targetSlot = BootsSlot;
                break;
            case ItemCategory.Ring:
                targetSlot = RingSlot;
                break;
            case ItemCategory.Necklace:
                targetSlot = NecklaceSlot;
                break;
        }

        Item oldItem = targetSlot.Item;
        Item newItem = slot.Item;

        targetSlot.UnlinkItem();
        targetSlot.LinkItem(newItem);
        slot.UnlinkItem();
        if (oldItem != null)
        {
            FindEmptySlot().LinkItem(oldItem);
        }
    }

    public void UnequipFromSlot(ItemSlot slot)
    {
        FindEmptySlot().LinkItem(slot.Item);
        slot.UnlinkItem();
    }

    public ItemSlot FindEmptySlot()
    {
        return _itemSlots.First(x => x.Item == null);
    }
}
