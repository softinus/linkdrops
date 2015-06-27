using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

namespace Assets.SCRIPT.Util
{
    /// <summary>
    /// This class defines our game's economy, which includes virtual goods, virtual currencies
    /// and currency packs, virtual categories
    /// </summary>
    public class IABStore : IStoreAssets
    {

        /// <summary>
        /// see parent.
        /// </summary>
        public int GetVersion()
        {
            return 0;
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualGood[] GetGoods()
        {
            return new VirtualGood[] {  };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] {  };
        }

        /// <summary>
        /// see parent.
        /// </summary>
        public VirtualCategory[] GetCategories()
        {
            return new VirtualCategory[] {  };
        }

        public static LifetimeVG[] GetLifetimeItems()
        {
            return new LifetimeVG[] { NO_ADS_LTVG };
        }

        /** Static Final Members **/
        public const string NO_ADS_LIFETIME_PRODUCT_ID = "remove_unityad";

        /** Virtual Currencies **/



        /** LifeTimeVGs **/
        // Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
        public static LifetimeVG NO_ADS_LTVG = new LifetimeVG(
            "No Ads", 														// name
            "No More Ads!",				 									// description
            "remove_unityad",														// item id
            new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
    }
}
