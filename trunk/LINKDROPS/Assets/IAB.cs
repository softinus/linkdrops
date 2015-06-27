using UnityEngine;
using System.Collections;
using Soomla.Store;
using Assets.SCRIPT.Util;
using System.Collections.Generic;
using System;

public class IAB : MonoBehaviour
{
    LifetimeVG[] buyNonADs = null;
    //public static List<LifetimeVG> NonConsumableItems = null;
	// Use this for initialization
	void Start ()
    {
        // Start Iab Service
        SoomlaStore.StartIabServiceInBg();

        StoreEvents.OnSoomlaStoreInitialized += OnSoomlaStoreInitialized;
        SoomlaStore.Initialize(new IABStore());
	}

    public void OnSoomlaStoreInitialized()
    {
        buyNonADs = IABStore.GetLifetimeItems();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        //GUILayout.Space(100);
        //if (GUI.Button(new Rect(0,0,500,500),"no Ads"))
        //{
        //    try
        //    {
        //        StoreInventory.BuyItem(buyNonADs[0].ItemId);
        //    }
        //    catch(Exception e)
        //    {
        //        GUILayout.Label(e.Message, GUILayout.Width(800));
        //        Debug.Log("purchase error : " + e.Message);
        //    }
        //}
    }

}
