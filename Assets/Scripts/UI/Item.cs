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
    public ItemCategory Category;
    public ItemRarity Rarity;
    public Sprite ItemSprite;
    public Dictionary<string, float> Stats = new Dictionary<string, float>();
    public string ItemName = "Dragon";

    public void Randomize()
    {
        Category = (ItemCategory) (UnityEngine.Random.Range(0, 6)+1);
        Rarity = (ItemRarity) UnityEngine.Random.Range(0, 5);

        Stats[Shortcuts.DEFENCE_STAT_KEY] = (float)UnityEngine.Random.Range(1, 10);
        Stats[Shortcuts.DAMAGE_STAT_KEY] = (float)UnityEngine.Random.Range(5, 20);
        Stats[Shortcuts.SPEED_STAT_KEY] = UnityEngine.Random.Range(0f, 1f)*20f;

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
        catch(Exception ex)
        {

        }
    }
}
