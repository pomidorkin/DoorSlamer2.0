using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreController;

    public static string noads = "noads"; //nonconsumable
    public static string gems100 = "gems100"; //consumable
    public static string gems300 = "gems300";
    public static string gems1000 = "gems1000";

    SaveManager saveManager;
    [SerializeField] MarketManager marketManager;
    [SerializeField] Button disableAdsButton;

    private void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        InitializePurchasing();

        if (saveManager.State.firstStart)
        {
            saveManager.State.firstStart = false;
            SaveManager.Instance.Save();
            RestoreMyProduct();
        }
    }

    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(gems100, ProductType.Consumable);
        builder.AddProduct(gems300, ProductType.Consumable);
        builder.AddProduct(gems1000, ProductType.Consumable);
        builder.AddProduct(noads, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(string productName)
    {
        m_StoreController.InitiatePurchase(productName);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;
        if (product.definition.id == gems100)
        {
            Gems100();
        }
        if (product.definition.id == gems300)
        {
            Gems300();
        }
        if (product.definition.id == gems1000)
        {
            Gems1000();
        }
        if (product.definition.id == noads)
        {
            NoAds();
        }

        return PurchaseProcessingResult.Complete;
    }

    private void NoAds()
    {
        saveManager.State.removeAddPurchased = true;
        SaveManager.Instance.Save();
        disableAdsButton.interactable = false;
    }

    private void Gems1000()
    {
        saveManager.State.gems += 1000;
        SaveManager.Instance.Save();
        FindObjectOfType<MarketManager>().ManageButtons();
        marketManager.ManageButtons();
    }

    private void Gems300()
    {
        saveManager.State.gems += 300;
        SaveManager.Instance.Save();
        FindObjectOfType<MarketManager>().ManageButtons();
        marketManager.ManageButtons();
    }

    private void Gems100()
    {
        saveManager.State.gems += 100;
        SaveManager.Instance.Save();
        FindObjectOfType<MarketManager>().ManageButtons();
        marketManager.ManageButtons();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //throw new System.NotImplementedException();
        Debug.Log("IAP initialized successfully");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        //throw new System.NotImplementedException();
    }

    public void RestoreMyProduct()
    {
        if (CodelessIAPStoreListener.Instance.StoreController.products.WithID(noads).hasReceipt)
        {
            NoAds();
        }
    }

}
