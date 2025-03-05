using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour
{
    private ItemSlot _lastHover;
    public ItemSlot SlotHeld;
    public Image PointerImage;

    void Update()
    {
        if (SlotHeld != null)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void RegisterHover(ItemSlot slot)
    {
        _lastHover = slot;
    }

    public void ClearHover()
    {
        _lastHover = null;
    }

    public void Grab(ItemSlot slot)
    {
        PointerImage.enabled = true;
        SlotHeld = slot;
        SlotHeld.ItemImage.enabled = false;
        PointerImage.sprite = slot.ItemImage.sprite;
    }

    public void Drop()
    {
        if (SlotHeld != null)
        {
            if (_lastHover != null && _lastHover != SlotHeld)
            {
                if(_lastHover.IsEquipped && _lastHover.SlotCategory != SlotHeld.Item.Category)
                {
                    _lastHover = Shortcuts.INVENTORY.FindDestinationForCategory(SlotHeld.Item.Category);
                }
                SlotHeld.SwapItems(_lastHover);
            }
            else
            {
                SlotHeld.ItemImage.enabled = true;
            }

            SlotHeld = null;
            PointerImage.enabled = false;
            _lastHover = null;
        }
    }
}
