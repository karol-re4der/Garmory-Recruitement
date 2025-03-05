using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterHandler : MonoBehaviour
{
    public InventoryHandler Inventory;
    public Dictionary<string, float> Stats = new Dictionary<string, float>();

    public void UpdateStats()
    {
        Stats = new Dictionary<string, float>();

        foreach (Item item in Inventory.EquipementSlots.Where(x=>x.Item!=null).Select(x=>x.Item))
        {
            foreach(string statKey in item.Stats.Keys)
            {
                if (!Stats.ContainsKey(statKey))
                {
                    Stats[statKey] = 0;
                }
                Stats[statKey] += item.Stats[statKey];
            }
        }
    }

    public float GetStatValue(string statKey)
    {
        if (Stats.ContainsKey(statKey))
        {
            return Stats[statKey];
        }
        return 0;
    }
}
