using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Shortcuts
{
    #region shortcuts
    private static InventoryHandler _inv;
    public static InventoryHandler INVENTORY
    {
        get
        {
            if (_inv == null)
            {
                _inv = GameObject.Find("MainMenu/Content/Inventory/")?.GetComponent<InventoryHandler>();
            }
            return _inv;
        }
    }

    private static CharacterHandler _char;
    public static CharacterHandler CHARACTER
    {
        get
        {
            if (_char == null)
            {
                _char = Camera.main.GetComponent<CharacterHandler>();
            }
            return _char;
        }
    }

    private static DragAndDropHandler _dad;
    public static DragAndDropHandler DRAG_AND_DROP
    {
        get
        {
            if (_dad == null)
            {
                _dad = GameObject.Find("Canvas/DragAndDropPointer/")?.GetComponent<DragAndDropHandler>();
            }
            return _dad;
        }
    }

    private static ItemToast _toast;
    public static ItemToast TOAST
    {
        get
        {
            if (_toast == null)
            {
                _toast = GameObject.Find("Canvas/ItemToast/")?.GetComponent<ItemToast>();
            }
            return _toast;
        }
    }

    private static NetworkHandler _net;
    public static NetworkHandler NETWORK
    {
        get
        {
            if (_net == null)
            {
                _net = GameObject.Find("Network/")?.GetComponent<NetworkHandler>();
            }
            return _net;
        }
    }
    #endregion

    #region paths
    public static string RaritySpritePath = "Textures/Items/Rarity/ItemRarity{0}";
    public static string CategorySpritePath = "Textures/Items/Rarity/CategoryIcon{0}";
    public static string WeaponSpritePath = "Textures/Items/Weapon/{0}";
    public static string ArmorSpritePath = "Textures/Items/Armor/{0}";
    public static string HelmetSpritePath = "Textures/Items/Helmet/{0}";
    public static string RingSpritePath = "Textures/Items/Ring/{0}";
    public static string NecklaceSpritePath = "Textures/Items/Necklace/{0}";
    public static string BootsSpritePath = "Textures/Items/Boots/{0}";
    #endregion

    #region settings
    public static TimeSpan DOUBLE_CLICK_SPEED = TimeSpan.FromSeconds(0.5);
    public static int INVENTORY_SIZE = 100;
    public static TimeSpan TOAST_DELAY = TimeSpan.FromSeconds(0.5);
    #endregion

    #region stats
    public static string DEFENCE_STAT_TEXT = "DEFENCE: {0}";
    public static string DAMAGE_STAT_TEXT = "DAMAGE: {0}";
    public static string SPEED_STAT_TEXT = "SPEED: {0}%";

    public static string DEFENCE_STAT_KEY = "Defence";
    public static string DAMAGE_STAT_KEY = "Damage";
    public static string SPEED_STAT_KEY = "Speed";
    #endregion
}
