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

    private void _reloadItemSprite()
    {
        ItemImage.sprite = Item.ItemSprite;
    }

    public void LinkItem(Item item, bool refreshStats = true)
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
        if (item.ItemSprite == null)
        {
            Invoke("_reloadItemSprite", 1);
        }

        ItemImage.enabled = true;

        if(refreshStats) Shortcuts.INVENTORY.RefreshStats();
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

    public void CallForToast()
    {
        Shortcuts.TOAST.Show(Item);
    }

    public void CallForToastHidding()
    {
        if (Shortcuts.TOAST.ItemAssociated == Item)
        {
            Shortcuts.TOAST.Hide();
        }
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
            Shortcuts.DRAG_AND_DROP.Grab(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Shortcuts.DRAG_AND_DROP.Drop();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Shortcuts.DRAG_AND_DROP.RegisterHover(this);

        if (Item != null)
        {
            if (Shortcuts.TOAST.ItemAssociated != null)
            {
                CallForToast();
            }
            else
            {
                float delay = (float)Shortcuts.TOAST_DELAY.TotalMilliseconds / 1000f;
                Invoke("CallForToast", delay);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke();
        Shortcuts.DRAG_AND_DROP.ClearHover();

        if (Item != null)
        {
            float delay = (float)Shortcuts.TOAST_DELAY.TotalMilliseconds / 2000f;
            Invoke("CallForToastHidding", delay);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Item != null)
        {
            //Double click
            DateTime now = DateTime.Now;
            if ((now - _lastClickTime) < Shortcuts.DOUBLE_CLICK_SPEED)
            {
                if (IsEquipped)
                {
                    Shortcuts.INVENTORY.FindEmptySlot().SwapItems(this);
                }
                else
                {
                    Shortcuts.INVENTORY.FindDestinationForCategory(Item.Category).SwapItems(this);
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
