using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    public float FadeIntensity = 0.5f;      //percentage of full color
    public float FadeDuration = 1f;         //in seconds
    public float FadeSmoothness = 30f;      //updates per second
    public Color FadeColor = Color.black;

    public Image Fade;
    public GameObject Content;
    public bool ActiveOnStartup;

    private float _fadeProgress = 0;
    private Color _fadeStartingColor;
    private Color _fadeTargetColor;
    private float _fadeProgressPerTick;
    public bool IsFadedIn;

    public void Switch()
    {
        if (IsFadedIn)
        {
            Exit();
        }
        else
        {
            Enter();
        }
    }

    void Awake()
    {
        if (ActiveOnStartup)
        {
            Enter();
        }
        else 
        { 
            Exit();
        }
        _fadeProgress = 1f;
    }

    public void Enter()
    {
        CancelInvoke();

        _fadeStartingColor = FadeColor;
        _fadeStartingColor.a = _fadeProgress;

        _fadeTargetColor = FadeColor;
        _fadeTargetColor.a = FadeIntensity;

        _fadeProgressPerTick = 1f / (FadeDuration * FadeSmoothness);
        float tickDuration = 1f / FadeSmoothness;

        IsFadedIn = true;
        _onFadedIn();
        Content.SetActive(true);
        Fade.gameObject.SetActive(true);
        InvokeRepeating("_fadeIn", 0, tickDuration);
    }

    private void _fadeIn()
    {
        _fadeProgress += _fadeProgressPerTick;
        Fade.color = Color.Lerp(_fadeStartingColor, _fadeTargetColor, _fadeProgress);

        if (_fadeProgress>=1)
        {
            _fadeProgress = 1;

            CancelInvoke();
        }
    }

    public void Exit()
    {
        CancelInvoke();

        _fadeStartingColor = FadeColor;
        _fadeStartingColor.a = _fadeProgress*FadeIntensity;

        _fadeTargetColor = Color.clear;

        _fadeProgressPerTick = 1f / (FadeDuration * FadeSmoothness);
        float tickDuration = 1f / FadeSmoothness;

        IsFadedIn = false;
        _onFadedOut();
        Content.SetActive(false);
        Fade.gameObject.SetActive(true);
        InvokeRepeating("_fadeOut", 0, tickDuration);
    }

    private void _fadeOut()
    {
        _fadeProgress -= _fadeProgressPerTick;
        Fade.color = Color.Lerp(_fadeTargetColor, _fadeStartingColor, _fadeProgress);

        if (_fadeProgress <= 0)
        {
            _fadeProgress = 0;
            Fade.gameObject.SetActive(false);
            CancelInvoke();
        }
    }

    protected virtual void _onFadedIn()
    {

    }

    protected virtual void _onFadedOut()
    {

    }
}