using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public string Title;
    public int MaxHealth;
    public int CurrentHealth;
    public int ScoreValue = 0;

    public TextMeshPro TitleText;
    public TextMeshPro HealthText;
    public Animator EnemyAnimator;

    public void Randomize()
    {
        string rarity = "";
        string suffix = "";
        string prefix = "";

        switch(UnityEngine.Random.Range(0, 5))
        {
            case 0:
                rarity = "Common";
                MaxHealth = 20;
                ScoreValue = 1;
                break;
            case 1:
                rarity = "Uncommon";
                MaxHealth = 40;
                ScoreValue = 2;
                break;
            case 2:
                rarity = "Rare";
                MaxHealth = 60;
                ScoreValue = 3;
                break;
            case 3:
                rarity = "Epic";
                MaxHealth = 80;
                ScoreValue = 4;
                break;
            case 4:
                rarity = "Legendary";
                MaxHealth = 100;
                ScoreValue = 5;
                break;
        }

        switch (UnityEngine.Random.Range(0, 5))
        {
            case 0:
                prefix = "Angry";
                break;
            case 1:
                prefix = "Evil";
                break;
            case 2:
                prefix = "Mad";
                break;
            case 3:
                prefix = "Hostile";
                break;
            case 4:
                prefix = "Monstrous";
                break;
        }

        switch (UnityEngine.Random.Range(0, 5))
        {
            case 0:
                suffix = "Container";
                break;
            case 1:
                suffix = "Barrel";
                break;
            case 2:
                suffix = "Storage";
                break;
            case 3:
                suffix = "Keg";
                break;
            case 4:
                suffix = "Tank";
                break;
        }

        Title = rarity + " " + prefix + " " + suffix;
        CurrentHealth = MaxHealth;
        _refreshHealthBar();
    }

    void Start()
    {
        Randomize();
        TitleText.text = Title;
    }

    public void OnHit(int damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;

            _refreshHealthBar();
            if (CurrentHealth <= 0)
            {
                OnDeath();
            }
            else
            {
                EnemyAnimator.SetTrigger("Hit");
            }
        }
    }

    public void OnDeath()
    {
        EnemyAnimator.SetTrigger("Death");
        TitleText.gameObject.SetActive(false);
        HealthText.gameObject.SetActive(false);
        Shortcuts.MAIN_HANDLER.Score += ScoreValue;
        Invoke("Destroy", 1);
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void _refreshHealthBar()
    {
        HealthText.text = Shortcuts.ENEMY_HP_TEXT.Replace("{0}", CurrentHealth.ToString()).Replace("{1}", MaxHealth.ToString());
    }
}
