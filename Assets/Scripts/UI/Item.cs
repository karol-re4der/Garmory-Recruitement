using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemCategory
{
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

public class Item : MonoBehaviour
{
    public ItemCategory Category;
    public ItemRarity Rarity;
    public Sprite ItemSprite;
    public List<Tuple<string, float>> Stats;
    public string ItemName = "Dragon";

    public void Randomize()
    {
        Category = (ItemCategory) UnityEngine.Random.Range(0, 6);
        Rarity = (ItemRarity) UnityEngine.Random.Range(0, 5);

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
