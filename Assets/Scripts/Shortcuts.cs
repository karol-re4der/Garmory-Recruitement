using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shortcuts
{
    private static Inventory _inv;
    public static Inventory Inventory
    {
        get
        {
            if (_inv == null)
            {
                _inv = GameObject.Find("MainMenu/Content/Inventory/")?.GetComponent<Inventory>();
            }
            return _inv;
        }
    }

    public static string RaritySpritePath = "Textures/Items/Rarity/ItemRarity{0}";
    public static string CategorySpritePath = "Textures/Items/Rarity/CategoryIcon{0}";
    public static string WeaponSpritePath = "Textures/Items/Weapon/{0}Weapon";
    public static string ArmorSpritePath = "Textures/Items/Armor/{0}Armor";
    public static string HelmetSpritePath = "Textures/Items/Helmet/{0}Helmet";
    public static string RingSpritePath = "Textures/Items/Ring/{0}Ring";
    public static string NecklaceSpritePath = "Textures/Items/Necklace/{0}Necklace";
    public static string BootsSpritePath = "Textures/Items/Boots/{0}Boots";

}
