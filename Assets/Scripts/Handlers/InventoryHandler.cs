using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
using System.Globalization;

public class InventoryHandler : MonoBehaviour
{
    public Transform BackpackGrid;

    public List<ItemSlot> EquipementSlots;

    public GameObject ItemSlotPrefab;
    public TextMeshProUGUI DefenceStatText;
    public TextMeshProUGUI DamageStatText;
    public TextMeshProUGUI SpeedStatText;
    public CharacterHandler Character;

    private List<ItemSlot> _itemSlots = new List<ItemSlot>();

    void Start()
    {
        FillWithSpaces();
        FillWithItems();
        RefreshStats();
    }

    public void FillWithSpaces()
    {
        for(int i = 0; i< Shortcuts.INVENTORY_SIZE; i++)
        {
            ItemSlot newSpace = GameObject.Instantiate(ItemSlotPrefab, BackpackGrid).GetComponent<ItemSlot>();

            _itemSlots.Add(newSpace);
        }
    }

    public void FillWithItems()
    {
        for(int i = 0; i<UnityEngine.Random.Range(5, 99); i++)
        {
            Item newItem = new Item();
            newItem.Randomize();
            _itemSlots[i].LinkItem(newItem, false);
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

    public void RefreshStats()
    {
        try
        {
            Character.UpdateStats();

            string defenceValue = ((int)Character.GetStatValue(Shortcuts.DEFENCE_STAT_KEY)).ToString();
            string damageValue = ((int)Character.GetStatValue(Shortcuts.DAMAGE_STAT_KEY)).ToString();
            string speedValue = (Character.GetStatValue(Shortcuts.SPEED_STAT_KEY) + 100f).ToString();

            DefenceStatText.text = Shortcuts.DEFENCE_STAT_TEXT.Replace("{0}", defenceValue);
            DamageStatText.text = Shortcuts.DAMAGE_STAT_TEXT.Replace("{0}", damageValue);
            SpeedStatText.text = Shortcuts.SPEED_STAT_TEXT.Replace("{0}", speedValue);
        }
        catch(Exception ex)
        {
            Debug.Log("Could not update stats.. are some stats invalid? Please verify");
        }
    }
}
