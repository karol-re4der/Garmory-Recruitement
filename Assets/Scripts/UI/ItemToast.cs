using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToast : MonoBehaviour
{
    public Item ItemAssociated;
    public TextMeshProUGUI DefenceStatText;
    public TextMeshProUGUI DamageStatText;
    public TextMeshProUGUI SpeedStatText;
    public TextMeshProUGUI InfoText;
    public Image ItemIcon;
    public GameObject Content;
    public CanvasGroup CanvasGroup;

    public float FadeDuration = 1f;         //in seconds
    public float FadeSmoothness = 30f;      //updates per second

    private float _fadeProgress = 0;
    private float _fadeProgressPerTick;
    private Vector3 _toastShift;

    public void Show(Item item)
    {
        if (item == null) return;

        ItemAssociated = item;

        ItemIcon.sprite = item.ItemSprite;
        InfoText.text = item.CompleteName;

        string defenceValue = ((int)item.GetStat(Shortcuts.DEFENCE_STAT_KEY)).ToString();
        string damageValue = ((int)item.GetStat(Shortcuts.DAMAGE_STAT_KEY)).ToString();
        string speedValue = ((int)item.GetStat(Shortcuts.SPEED_STAT_KEY)).ToString();

        DefenceStatText.text = Shortcuts.DEFENCE_STAT_TEXT.Replace("{0}", defenceValue);
        DamageStatText.text = Shortcuts.DAMAGE_STAT_TEXT.Replace("{0}", damageValue);
        SpeedStatText.text = Shortcuts.SPEED_STAT_TEXT.Replace("{0}", speedValue);

        Content.SetActive(true);

        Vector2 mousePos = Input.mousePosition;
        Vector2 ToastDirection = new Vector2
        {
            x = (mousePos.x > Screen.width / 2) ? 1 : -1,
            y = (mousePos.y > Screen.height / 2) ? 1 : -1
        };
        Vector3 ToastSize = GetComponent<RectTransform>().sizeDelta;
        _toastShift = new Vector3
        {
            x = (ToastSize / 2).x * ToastDirection.x,
            y = (ToastSize / 2).y * ToastDirection.y,
            z = 0
        };



        BeginFadeIn();
    }

    public void Hide()
    {
        BeginFadeOut();
    }

    void Update()
    {
        if (ItemAssociated != null)
        {
            transform.position = Input.mousePosition - _toastShift;
        }
        
    }

    #region fade in/out
    public void BeginFadeIn()
    {
        CancelInvoke();

        _fadeProgressPerTick = 1f / (FadeDuration * FadeSmoothness);
        float tickDuration = 1f / FadeSmoothness;

        Content.SetActive(true);
        InvokeRepeating("_fadeIn", 0, tickDuration);
    }

    private void _fadeIn()
    {
        _fadeProgress += _fadeProgressPerTick;
        CanvasGroup.alpha = _fadeProgress;

        if (_fadeProgress >= 1)
        {
            _fadeProgress = 1;

            CancelInvoke();
        }
    }

    public void BeginFadeOut()
    {
        CancelInvoke();

        _fadeProgressPerTick = 1f / (FadeDuration * FadeSmoothness);
        float tickDuration = 1f / FadeSmoothness;

        InvokeRepeating("_fadeOut", 0, tickDuration);
    }

    private void _fadeOut()
    {
        _fadeProgress -= _fadeProgressPerTick*2; //fade out twice as fast
        CanvasGroup.alpha = _fadeProgress;

        if (_fadeProgress <= 0)
        {
            _fadeProgress = 0;
            Content.SetActive(false);
            CancelInvoke();
            ItemAssociated = null;
        }
    }
    #endregion
}
