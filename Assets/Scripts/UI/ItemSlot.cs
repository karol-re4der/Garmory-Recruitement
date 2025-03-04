using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemSlot : MonoBehaviour
{
    private Item _item;
    public Image RarityImage;
    public Image ItemImage;
    public Image CategoryImage;

    public void ClearSlot()
    {

    }

    public void LinkItem(Item item)
    {
        _item = item;
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
}
