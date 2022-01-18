using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewAd : MonoBehaviour
{
    private string rewardedUnityId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;
    [SerializeField] Building building;
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    [SerializeField] GameManagerScript gameManagerScript;

    private void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardedUnityId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward; // Dosmotrel li
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        Debug.Log("Add Watched");
        building.rewardedAddWatched = true;
        int health = building.FillHealth();
        healthDisplay.SetDisplay(health);

        gameOverMenu.gameObject.SetActive(false);
        hoodMenu.gameObject.SetActive(true);
        gameManagerScript.ResumeGame();
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
}
