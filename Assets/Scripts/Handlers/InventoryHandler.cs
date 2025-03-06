using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
using System.Globalization;
using Newtonsoft.Json.Linq;

public class InventoryHandler : MonoBehaviour
{
    public Transform BackpackGrid;

    public List<ItemSlot> EquipementSlots;

    public GameObject ItemSlotPrefab;
    public TextMeshProUGUI DefenceStatText;
    public TextMeshProUGUI DamageStatText;
    public TextMeshProUGUI SpeedStatText;
    public CharacterHandler Character;
    public LoadingCover Loader;

    private List<ItemSlot> _itemSlots = new List<ItemSlot>();
    private bool _itemsUpToDate = false;

    void Start()
    {
        _fillWithSpaces();
        Shortcuts.NETWORK.UpdateItemList();
        RefreshStats();
    }

    void Update()
    {
        if (!_itemsUpToDate && Shortcuts.NETWORK.ResponseUpToDate)
        {
            List<Item> items = _extractItemsFromJson(Shortcuts.NETWORK.ServerResponse);
            _fillWithItems(items);
            _itemsUpToDate = true;
            Loader.Hide();
        }
    }

    private void _fillWithSpaces()
    {
        for(int i = 0; i< Shortcuts.INVENTORY_SIZE; i++)
        {
            ItemSlot newSpace = GameObject.Instantiate(ItemSlotPrefab, BackpackGrid).GetComponent<ItemSlot>();

            _itemSlots.Add(newSpace);
        }
    }

    private void _fillWithItems(List<Item> items)
    {
        foreach(Item item in items)
        {
            ItemSlot emptySlot = FindEmptySlot();
            if (emptySlot == null)
            {
                Debug.Log("No empty slot available for item.. please check");
                break;
            }

            emptySlot.LinkItem(item);
        }
    }

    private List<Item> _extractItemsFromJson(string json)
    {
        List<Item> items = new List<Item>();
        try
        {
            JObject jobj = JObject.Parse(json);

            foreach (JObject jItem in jobj["Items"])
            {
                Item item = new Item();
                item.ItemName = (string)jItem["Name"];
                item.Rarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)jItem["Rarity"]);
                item.Category = (ItemCategory)Enum.Parse(typeof(ItemCategory), (string)jItem["Category"]);
                item.Stats["Damage"] = (float)jItem["Damage"];
                item.Stats["HealthPoints"] = (float)jItem["HealthPoints"];
                item.Stats["Defense"] = (float)jItem["Defense"];
                item.Stats["LifeSteal"] = (float)jItem["LifeSteal"];
                item.Stats["CriticalStrikeChance"] = (float)jItem["CriticalStrikeChance"];
                item.Stats["AttackSpeed"] = (float)jItem["AttackSpeed"];
                item.Stats["MovementSpeed"] = (float)jItem["MovementSpeed"];
                item.Stats["Luck"] = (float)jItem["Luck"];
                item.LoadRelevantSprite();
                items.Add(item);
            }

        }
        catch(Exception ex)
        {
            Debug.Log("Could not parse json string of items.. was server response invalid?");
        }
        return items;
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
