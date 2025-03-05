using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemCategory
{
    Any,
    Armor,
    Weapon,
    Helmet,
    Boots,
    Necklace,
    Ring
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public class Item
{
    public Sprite ItemSprite;

    public ItemCategory Category;
    public ItemRarity Rarity;
    public Dictionary<string, float> Stats = new Dictionary<string, float>();
    public string ItemName = "";

    public string CompleteName
    {
        get
        {
            return Rarity.ToString() + " " + ItemName.Insert(ItemName.Length-Category.ToString().Length, " ");
        }
    }

    public void Randomize()
    {
        Category = (ItemCategory)2;//(UnityEngine.Random.Range(0, 6)+1);
        Rarity = (ItemRarity)1;// UnityEngine.Random.Range(0, 5);

        Stats[Shortcuts.DEFENCE_STAT_KEY] = 1;// (float)UnityEngine.Random.Range(1, 10);
        Stats[Shortcuts.DAMAGE_STAT_KEY] = 1;//(float)UnityEngine.Random.Range(5, 20);
        Stats[Shortcuts.SPEED_STAT_KEY] = 1;//UnityEngine.Random.Range(0f, 1f)*20f;
    }

    public void LoadRelevantSprite()
    {
        try
        {
            string path = "";

            switch (Category)
            {
                case ItemCategory.Armor:
                    path = Shortcuts.ArmorSpritePath.Replace("{0}", ItemName);
                    break;
                case ItemCategory.Weapon:
                    path = Shortcuts.WeaponSpritePath.Replace("{0}", ItemName);
                    break;
                case ItemCategory.Helmet:
                    path = Shortcuts.HelmetSpritePath.Replace("{0}", ItemName);
                    break;
                case ItemCategory.Boots:
                    path = Shortcuts.BootsSpritePath.Replace("{0}", ItemName);
                    break;
                case ItemCategory.Ring:
                    path = Shortcuts.RingSpritePath.Replace("{0}", ItemName);
                    break;
                case ItemCategory.Necklace:
                    path = Shortcuts.NecklaceSpritePath.Replace("{0}", ItemName);
                    break;
            }
            ItemSprite = Resources.Load<Sprite>(path);
        }
        catch (Exception ex)
        {
            Debug.Log("Could not load item sprite.. please verify.");
        }
    }

    public float GetStat(string statKey)
    {
        if (Stats.ContainsKey(statKey))
        {
            return Stats[statKey];
        }
        else
        {
            return 0;
        }
    }
}
