using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewAd : MonoBehaviour
{
    private string rewardedUnityId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;
    private RewardedAd rewardedCoins;
    [SerializeField] Building building;
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    [SerializeField] GameManagerScript gameManagerScript;

    [SerializeField] int bonus = 3;
    [SerializeField] BalanceDisplay balanceDisplay;

    SaveManager saveManager;

    private void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardedUnityId);
        rewardedCoins = new RewardedAd(rewardedUnityId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward; // Dosmotrel li

        rewardedCoins.LoadAd(adRequest);
        rewardedCoins.OnUserEarnedReward += HandleUserEarnedCoins;
        rewardedCoins.OnAdClosed += HandleCoinsdAdClosed;

        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
    }

    private void HandleCoinsdAdClosed(object sender, EventArgs e)
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedCoins.LoadAd(adRequest);
    }

    private void HandleUserEarnedCoins(object sender, Reward e)
    {
        saveManager.State.gems += bonus;
        saveManager.Save();
        balanceDisplay.DisplayBalance();
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedCoins.LoadAd(adRequest);
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

    public void ShowCoinAd()
    {
        if (rewardedCoins.IsLoaded())
        {
            rewardedCoins.Show();
        }
        
    }
}
