﻿using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OculusIAP : MonoBehaviourSingleton<OculusIAP>
{
    string[] skus;
    List<Sku> skusList;

    // Dictionary to keep track of SKUs being consumed
    private Dictionary<UInt64, string> skuDictionary = new Dictionary<UInt64, string>();
    void Start()
    {
        skusList = new List<Sku>();

        skusList.Add(new Sku("price_1", 1));
        skusList.Add(new Sku("price_2", 2));
        skusList.Add(new Sku("price_5", 5));
        skusList.Add(new Sku("price_10", 10));
        skusList.Add(new Sku("price_20", 20));
        skusList.Add(new Sku("price_30", 30));
        skusList.Add(new Sku("price_40", 40));
        skusList.Add(new Sku("price_50", 50));
        skusList.Add(new Sku("price_100", 100));
        skusList.Add(new Sku("price_150", 150));
        skusList.Add(new Sku("price_200", 200));

        skus = new string[skusList.Count];
        for (int i = 0; i < skusList.Count; i++)
        {
            skus[i] = skusList[i].price;
        }

        ///Đầu tiên gọi hàm này
        Core.AsyncInitialize().OnComplete(InitCallback);
        //GetPrices();
        //GetPurchases();

    }
    public Sku GetSku(string sku)
    {
        if (skusList.Exists(x => x.sku == sku))
            return skusList.Find(x => x.sku == sku);

        return null;
    }

    /// <summary>
    /// khởi tạo core flatfom xong
    /// </summary>
    /// <param name="msg"></param>
    private void InitCallback(Message<Oculus.Platform.Models.PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error initializing Oculus Platform: " + msg.GetError().Message);
            // Consider retrying initialization or disabling IAP functionality
        }
        else
        {
            Debug.Log("Oculus Platform initialized successfully.");
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCheckCallback);
        }
    }
    private void EntitlementCheckCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("User not entitled to application, cannot proceed.");
            // Application.Quit();
        }
        else
        {
            Debug.Log("User is entitled.");
            GetPrices();
            GetPurchases();
        }
    }
    private void GetPrices()
    {
        IAP.GetProductsBySKU(skus).OnComplete(GetPricesCallback);
    }

    private void GetPricesCallback(Message<ProductList> msg)
    {
        if (msg.IsError) return;
        foreach (var prod in msg.GetProductList())
        {
            //availableItems.text += $"{prod.Name} - {prod.FormattedPrice} \n";
        }
    }
    private void GetPurchases()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }
    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            // purchasedItems.text += $"{purch.Sku}-{purch.GrantTime} \n";
            //  AllocateCoins(purch.Sku);
            ConsumePurchase(purch.Sku);
        }
        CoinPurchaseDeductionCheck();
    }
    private void ConsumePurchase(string skuName)
    {
        //cosume without adding coins 
        //IAP.ConsumePurchase(skuName).OnComplete(ConsumePurchaseCallback);
        var request = IAP.ConsumePurchase(skuName);
        skuDictionary[request.RequestID] = skuName;
        request.OnComplete(ConsumePurchaseCallback);
    }

    private void ConsumePurchaseCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error consuming purchase: " + msg.GetError().Message);
        }
        else
        {
            if (skuDictionary.TryGetValue(msg.RequestID, out var sku))
            {
                Debug.Log($"Purchase consumed successfully for SKU: {sku}");
                AllocateCoins(sku); // Call AllocateCoins for each consumable purchase
                skuDictionary.Remove(msg.RequestID);
            }
            else
            {
                Debug.Log("Purchase consumed successfully, but SKU not found in dictionary.");
            }
        }
    }

    public void AllocateCoins(string input)
    {
        switch (input)
        {
            case "price_1":
                break;
            case "price_2":
                break;
            case "price_5":
                break;
            case "price_10":
                break;
            case "price_20":
                break;
            case "price_30":
                break;
            case "price_40":
                break;
            case "price_50":
                break;
            case "price_100":
                break;
            case "price_150":
                break;
            case "price_200":
                break;
            default:
                break;
        }
    }
    public void CoinPurchaseDeductionCheck()
    {
        string data = PlayerPrefs.GetString("PurchasedItem", "0");
        if (!string.IsNullOrEmpty(data))
        {
            string[] items = data.Split(new string[] { "@@" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                int value;
                if (int.TryParse(item, out value))
                {
                    Debug.Log("Retrieved value: " + value);


                    //CoinsCollected = CoinsCollected - airCraftPrice[value];
                }
                else
                {
                    Debug.LogError("Failed to parse item to integer: " + item);
                }
            }
        }
        else
        {
            Debug.Log("No data found in PlayerPrefs for 'PurchasedItem'.");
        }
    }

    public void BuyTurn(string skuName)
    {
#if UNITY_EDITOR
        AllocateCoins(skuName);
#else
        IAP.LaunchCheckoutFlow(skuName).OnComplete(BuyTurnCallBack);
#endif
    }
    private void BuyTurnCallBack(Message<Purchase> msg)
    {
        if (msg.IsError) return;

        //purchasedItems.text = string.Empty;
        GetPurchases();
    }
}
public class Sku
{
    public string sku;
    public int number;
    public Sku(string sku, int number)
    {
        this.sku = sku;
        this.number = number;
    }
    public string price
    {
        get
        {
            switch (sku)
            {
                case "price_2":
                    return "1.99";
                case "price_5":
                    return "4.99";
                case "price_10":
                    return "9.99";
                case "price_20":
                    return "19.99";
                case "price_50":
                    return "49.99";
                case "price_100":
                    return "99.99";
                case "price_200":
                    return "199.99";
                default:
                    return "0.00";
            }
        }

    }
}