using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string interstitialUnityId = "ca-app-pub-7318566780860459/4457945179";
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    [SerializeField] GameManagerScript gameManagerScript;

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnityId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
        interstitialAd.OnAdClosed += HandleAdClosed;
    }

    private void HandleAdClosed(object sender, EventArgs e)
    {
        gameOverMenu.gameObject.SetActive(true);
        hoodMenu.gameObject.SetActive(false);
    }

    public void ShowAd()
    {
        gameManagerScript.PauseGame();
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
