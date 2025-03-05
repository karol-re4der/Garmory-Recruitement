using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IInitializePotentialDragHandler, IDragHandler
{
    public Item Item;
    public Image RarityImage;
    public Image ItemImage;
    public Image CategoryImage;
    public bool IsEquipped;
    public ItemCategory SlotCategory = ItemCategory.Any;

    private DateTime _lastClickTime = DateTime.MinValue;

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

    public void SwapItems(ItemSlot targetSlot)
    {
        Item localItem = Item;
        Item remoteItem = targetSlot.Item;

        if (localItem != null) UnlinkItem();
        if (remoteItem != null) targetSlot.UnlinkItem();

        if (remoteItem != null) LinkItem(remoteItem);
        if (localItem != null) targetSlot.LinkItem(localItem);
    }

    #region events
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            Shortcuts.DragAndDropHandler.Grab(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Shortcuts.DragAndDropHandler.Drop();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Shortcuts.DragAndDropHandler.RegisterHover(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Shortcuts.DragAndDropHandler.ClearHover();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Item != null)
        {
            //Double click
            DateTime now = DateTime.Now;
            if ((now - _lastClickTime) < Shortcuts.DoubleClickSpeed)
            {
                if (IsEquipped)
                {
                    Shortcuts.Inventory.FindEmptySlot().SwapItems(this);
                }
                else
                {
                    Shortcuts.Inventory.FindDestinationForCategory(Item.Category).SwapItems(this);
                }
                _lastClickTime = DateTime.MinValue;
            }
            else
            {
                _lastClickTime = DateTime.Now;
            }
        }
    }

    #endregion
}
