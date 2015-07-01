using UnityEngine;
using System.Collections;
using OnePF;
using System.Collections.Generic;

public class openIAB : MonoBehaviour {

    static public string SKU_ads = "remove_unityad"; 

    void Awake()
    {
        // Subscribe to all billing events
        OpenIABEventManager.billingSupportedEvent += OnBillingSupported;
        OpenIABEventManager.billingNotSupportedEvent += OnBillingNotSupported;
        OpenIABEventManager.queryInventorySucceededEvent += OnQueryInventorySucceeded;
        OpenIABEventManager.queryInventoryFailedEvent += OnQueryInventoryFailed;
        OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceded;
        OpenIABEventManager.purchaseFailedEvent += OnPurchaseFailed;
        OpenIABEventManager.consumePurchaseSucceededEvent += OnConsumePurchaseSucceeded;
        OpenIABEventManager.consumePurchaseFailedEvent += OnConsumePurchaseFailed;
        OpenIABEventManager.transactionRestoredEvent += OnTransactionRestored;
        OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceeded;
        OpenIABEventManager.restoreFailedEvent += OnRestoreFailed;
    }


	// Use this for initialization
    void Start()
    {
        // SKU's for iOS MUST be mapped. Mappings for other stores are optional
        //OpenIAB.mapSku(SKU_REPAIR_KIT, OpenIAB_iOS.STORE, "30_real");

        var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAizHCUk+1IniOkGJfOBew+IecoE+o1qO+NwwFt8oFQQ+XKw8OTEhyckNd/qMSBFewsshySeKLzma95AByEayC/O/ZcW9zQG4H65y0id8+k4JUiCnVGAiyZzDttRb3bidSU/5rG0mQo5lQKBcmRp/9H/PPQmTDh6XOCeEEn9aE2nn4X9UrCxD/rUnXIwz2Op7af9p1RtLnsJLCHTqlhepU2WerMlZTeFPOhFnE6DwvPRFSIBK+ffiOkB81k4rJ3hawpojCCC82khXJ8Q+D0bOkVfupwAE2Z9Ml3ylKWlFGk3GdwaHd/7trqAQO5s5xnyHbzvOoicOoDd/QbBhgeUwIOwIDAQAB";

        // Map SKUs for Google Play
        OpenIAB.mapSku(SKU_ads, OpenIAB_Android.STORE_GOOGLE, "remove_unityad");

        var options = new OnePF.Options();

        options.checkInventory = false;
        options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
        options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
        options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_GOOGLE, googlePublicKey } };
        //options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.verifyMode = OptionsVerifyMode.VERIFY_ONLY_KNOWN;
        options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
        // Add Google Play public key
        //options.storeKeys.Add(OpenIAB_Android.STORE_GOOGLE,
        //"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAizHCUk+1IniOkGJfOBew+IecoE+o1qO+NwwFt8oFQQ+XKw8OTEhyckNd/qMSBFewsshySeKLzma95AByEayC/O/ZcW9zQG4H65y0id8+k4JUiCnVGAiyZzDttRb3bidSU/5rG0mQo5lQKBcmRp/9H/PPQmTDh6XOCeEEn9aE2nn4X9UrCxD/rUnXIwz2Op7af9p1RtLnsJLCHTqlhepU2WerMlZTeFPOhFnE6DwvPRFSIBK+ffiOkB81k4rJ3hawpojCCC82khXJ8Q+D0bOkVfupwAE2Z9Ml3ylKWlFGk3GdwaHd/7trqAQO5s5xnyHbzvOoicOoDd/QbBhgeUwIOwIDAQAB");
        
        OpenIAB.init(options);


    }


    // Verifies the developer payload of a purchase.
    bool VerifyDeveloperPayload(string developerPayload)
    {
        /*
         * TODO: verify that the developer payload of the purchase is correct. It will be
         * the same one that you sent when initiating the purchase.
         * 
         * WARNING: Locally generating a random string when starting a purchase and 
         * verifying it here might seem like a good approach, but this will fail in the 
         * case where the user purchases an item on one device and then uses your app on 
         * a different device, because on the other device you will not have access to the
         * random string you originally generated.
         *
         * So a good developer payload has these characteristics:
         * 
         * 1. If two different users purchase an item, the payload is different between them,
         *    so that one user's purchase can't be replayed to another user.
         * 
         * 2. The payload must be such that you can verify it even when the app wasn't the
         *    one who initiated the purchase flow (so that items purchased by the user on 
         *    one device work on other devices owned by the user).
         * 
         * Using your own server to store and verify developer payloads across app
         * installations is recommended.
         */
        return true;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void RemoveAds()
    {
        OpenIAB.purchaseProduct(openIAB.SKU_ads);
    }

    private void OnBillingSupported()
    {
        Debug.Log("Billing is supported");
        OpenIAB.queryInventory(new string[] { SKU_ads });
    }
    private void OnBillingNotSupported(string error)
    {
        Debug.Log("Billing not supported: " + error);
    }

    private void OnQueryInventorySucceeded(Inventory inventory)
    {
        Debug.Log("Query inventory Succeeded: " + inventory);

        List<string> arrOwned= inventory.GetAllOwnedSkus();
        for(int i=0; i<arrOwned.Count; ++i)
        {
            if (arrOwned[i] == "remove_unityad")
                PlayerPrefs.SetInt("no_ads", 1);    // remove ads
        }

        CheckAD_toUI();
    }

    private void CheckAD_toUI()
    {
        GameObject gBuyNoAds = GameObject.Find("noadsPurchase");
        int NoADs = PlayerPrefs.GetInt("no_ads", 0);    // 0:default, 1:removed
        if (NoADs == 1)
            gBuyNoAds.SetActive(false);
    }

    private void OnQueryInventoryFailed(string error)
    {
        Debug.Log("Query inventory failed: " + error);
    }

    private void OnPurchaseSucceded(Purchase purchase)
    {
        Debug.Log("Purchase succeded: " + purchase.Sku + "; Payload: " + purchase.DeveloperPayload);
        if (!VerifyDeveloperPayload(purchase.DeveloperPayload))
            return;

        if (purchase.Sku == SKU_ads)
        {
            OpenIAB.consumeProduct(purchase);

            PlayerPrefs.SetInt("no_ads", 1);    // remove ads

            CheckAD_toUI();
        }
        else
        {
            Debug.LogWarning("UnknownSKU:" + purchase.Sku);
        }
    }

    private void OnPurchaseFailed(int errorCode, string error)
    {
        Debug.Log("Purchase failed: " + error);
    }

    private void OnConsumePurchaseSucceeded(Purchase purchase)
    {
        // 구매 완료시
        PlayerPrefs.SetInt("no_ads", 1);    // remove ads
        CheckAD_toUI();

        Debug.Log("Consume purchase succeded: " + purchase.ToString());
    }

    private void OnConsumePurchaseFailed(string error)
    {
        Debug.Log("Consume purchase failed: " + error);
    }

    private void OnTransactionRestored(string sku)
    {
        Debug.Log("Transaction restored: " + sku);
    }

    private void OnRestoreSucceeded()
    {
        Debug.Log("Transactions restored successfully");
    }

    private void OnRestoreFailed(string error)
    {
        Debug.Log("Transaction restore failed: " + error);
    }

    private void OnDestory()
    {
        // Unsubscribe to avoid nasty leaks
        OpenIABEventManager.billingSupportedEvent -= OnBillingSupported;
        OpenIABEventManager.billingNotSupportedEvent -= OnBillingNotSupported;
        OpenIABEventManager.queryInventorySucceededEvent -= OnQueryInventorySucceeded;
        OpenIABEventManager.queryInventoryFailedEvent -= OnQueryInventoryFailed;
        OpenIABEventManager.purchaseSucceededEvent -= OnPurchaseSucceded;
        OpenIABEventManager.purchaseFailedEvent -= OnPurchaseFailed;
        OpenIABEventManager.consumePurchaseSucceededEvent -= OnConsumePurchaseSucceeded;
        OpenIABEventManager.consumePurchaseFailedEvent -= OnConsumePurchaseFailed;
        OpenIABEventManager.transactionRestoredEvent -= OnTransactionRestored;
        OpenIABEventManager.restoreSucceededEvent -= OnRestoreSucceeded;
        OpenIABEventManager.restoreFailedEvent -= OnRestoreFailed;
    }
}
