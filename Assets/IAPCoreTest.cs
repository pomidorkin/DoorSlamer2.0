using System;
using UnityEngine;
using UnityEngine.Purchasing; //���������� � ���������, ����� �������� ����� ���������� �������
//using UnityEngine.UI;

public class IAPCoreTest : MonoBehaviour, IStoreListener //��� ��������� ��������� �� Unity Purchasing
{
    private static IStoreController m_StoreController;          //������ � ������� Unity Purchasing
    private static IExtensionProvider m_StoreExtensionProvider; // ���������� ������� ��� ���������� ���������

    public static string noads = "noads"; //����������� - nonconsumable
    public static string gems100 = "gems100"; //������������ - consumable
    public static string gems300 = "gems300";
    public static string gems1000 = "gems1000";

    SaveManager saveManager;
    //[SerializeField] BalanceDisplay balanceDisplay;
    //private BalanceDisplay balanceDisplay;
    //[SerializeField] Button disableAdsButton;
    //[SerializeField] MarketManager marketManager;

    void Start()
    {
        //saveManager = SaveManager.Instance;
        //SaveManager.Instance.Load();
        if (m_StoreController == null) //���� ��� �� ���������������� ������� Unity Purchasing, ����� ��������������
        {
            InitializePurchasing();
        }

    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) //���� ��� ���������� � ������� - ������� �� �������
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //����������� ���� ������ ��� ���������� � ������
        builder.AddProduct(noads, ProductType.NonConsumable);
        builder.AddProduct(gems100, ProductType.Consumable);
        builder.AddProduct(gems300, ProductType.Consumable);
        builder.AddProduct(gems1000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void Buy_noads()
    {
        BuyProductID(noads);
    }

    public void Buy_gems100()
    {
        BuyProductID(gems100);
    }

    public void Buy_gems300()
    {
        BuyProductID(gems300);
    }

    public void Buy_gems1000()
    {
        BuyProductID(gems1000);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized()) //���� ������� ���������������� 
        {
            Product product = m_StoreController.products.WithID(productId); //������� ������� ������� 

            if (product != null && product.availableToPurchase) //���� ������� ������ � ����� ��� �������
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product); //��������
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) //�������� �������
    {
        if (String.Equals(args.purchasedProduct.definition.id, noads, StringComparison.Ordinal)) //��� �������� ��� ID
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //saveManager.State.removeAddPurchased = true;
            //SaveManager.Instance.Save();
            //disableAdsButton.interactable = false; // ?


        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems100, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //�������� ��� �������
            //saveManager.State.gems += 100;
            //SaveManager.Instance.Save();
            //FindObjectOfType<BalanceDisplay>().DisplayBalance();
            //FindObjectOfType<MarketManager>().ManageButtons(); // ?
            //marketManager.ManageButtons();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems300, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //�������� ��� �������
            //saveManager.State.gems += 300;
            //SaveManager.Instance.Save();
            //FindObjectOfType<BalanceDisplay>().DisplayBalance();
            //FindObjectOfType<MarketManager>().ManageButtons(); // ?
            //marketManager.ManageButtons();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems1000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            //�������� ��� �������
            //saveManager.State.gems += 1000;
            //SaveManager.Instance.Save();
            //FindObjectOfType<BalanceDisplay>().DisplayBalance();
            //FindObjectOfType<MarketManager>().ManageButtons(); // ?
            //marketManager.ManageButtons();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }

    public void RestorePurchases() //�������������� ������� (������ ��� Apple). � ���� ��� �������������� �������.
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer) //���� ��������� �� ��� ����������
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }



}