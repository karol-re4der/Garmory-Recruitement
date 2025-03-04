using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemSlot : MonoBehaviour
{
    public Item Item;
    public Image RarityImage;
    public Image ItemImage;
    public Image CategoryImage;
    public bool IsEquipped;

    private DateTime _lastClickTime = DateTime.MinValue;

    public void ClearSlot()
    {

    }

    public void LinkItem(Item item)
    {
        Item = item;
        try
        {
            string path = Shortcuts.RaritySpritePath.Replace("{0}", item.Rarity.ToString());
            RarityImage.sprite = Resources.Load<Sprite>(path);
        }
        catch (Exception ex)
        { 
            Debug.Log("Could not find sprites for the item slot. Please check!");
        }

        ItemImage.sprite = item.ItemSprite;
        ItemImage.enabled = true;
    }

    public void UnlinkItem()
    {
        Item = null;
        ItemImage.enabled = false;

        try
        {
            string path = Shortcuts.RaritySpritePath.Replace("{0}", ((ItemRarity)0).ToString());
            RarityImage.sprite = Resources.Load<Sprite>(path);
        }
        catch (Exception ex)
        {
            Debug.Log("Could not find sprites for the item slot. Please check!");
        }
    }

    public void OnClick()
    {
        DateTime now = DateTime.Now;
        if ((now - _lastClickTime)<Shortcuts.DoubleClickSpeed)
        {
            if (Item != null)
            {
                if (IsEquipped)
                {
                    UnequipItem();
                }
                else
                {
                    EquipItem();
                }
            }
            _lastClickTime = DateTime.MinValue;
        }
        else
        {
            _lastClickTime = DateTime.Now;
        }
    }

    public void UnequipItem()
    {
        Shortcuts.Inventory.UnequipFromSlot(this);
    }

    public void EquipItem()
    {
        Shortcuts.Inventory.EquipFromSlot(this);
    }
}
