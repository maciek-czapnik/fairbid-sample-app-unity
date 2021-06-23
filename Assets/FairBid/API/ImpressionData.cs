using UnityEngine;

namespace Fyber
{
    [System.Serializable]

    /// <summary>
    /// Impression data contains detailed information for each impression. That includes which demand source served the ad,
    /// the expected or exact revenue associated with it as well as granular details that will allow you to analyse and,
    /// ultimately, optimize both your ad monetization and user acquisition strategies.
    /// </summary>
    public class ImpressionData
    {
        /// <summary>
        /// Accuracy of the netPayout value. May return one of the following:
        /// - UNDISCLOSED - When the netPayout is '0'.
        /// - PREDICTED - When Fyber's estimation of the impression value is based on historical data from non-programmatic mediated network's reporting APIs.
        /// - PROGRAMMATIC - When netPayout is the exact and committed value of the impression, available when impressions are won by programmatic buyers.
        /// </summary>
        public enum PriceAccuracy
        {
            Undisclosed = 0,
            Predicted = 1,
            Programmatic = 2
        }

        /// <summary>
        /// Type of the impression's placement.
        /// </summary>
        public enum PlacementType
        {
            Banner = 0,
            Interstitial = 1,
            Rewarded = 2
        }

        /// <summary>
        /// Advertiser's domain when available. Used as an identifier for a set of campaigns for the same advertiser.
        /// </summary>
        public string advertiserDomain;
        
        /// <summary>
        /// Campaign ID when available used as an identifier for a specific campaign of a certain advertiser.
        /// </summary>
        public string campaignId = null;
        
        /// <summary>
        /// Country location of the ad impression (in ISO country code).
        /// </summary>
        public string countryCode = null;
        
        /// <summary>
        /// Creative ID when available. Used as an identifier for a specific creative of a certain campaign.
        /// This is particularly useful information when a certain creative is found to cause user experience issues.
        /// </summary>
        public string creativeId = null;
        
        /// <summary>
        /// Currency of the payout.
        /// </summary>
        public string currency = null;
        
        /// <summary>
        /// Demand Source name is the name of the buy-side / demand-side entity that purchased the impression.
        /// When mediated networks win an impression, you'll see the mediated network's name. When a DSP buying
        /// through the programmatic marketplace wins the impression, you'll see the DSP's name.
        /// </summary>
        public string demandSource = null;

        /// <summary>
        /// The amount of impressions in current session for the given Placement Type
        /// </summary>
        public int impressionDepth;

        /// <summary>
        /// A unique identifier for a specific impression.
        /// </summary>
        public string impressionId = null;
        
        /// <summary>
        /// Net payout for an impression. The value accuracy is returned in the priceAccuracy field.
        /// The value is provided in units returned in the currency field.
        /// </summary>
        public string netPayout = null;
        
        /// <summary>
        /// The mediated ad network's original Placement/Zone/Location/Ad Unit ID that you created in their dashboard.
        /// For ads shown by the Fyber Marketplace the networkInstanceId is the Placement ID you created in the Fyber console.
        /// </summary>
        public string networkInstanceId = null;
        
        /// <summary>
        /// Name of the SDK rendering the ad.
        /// </summary>
        public string renderingSDK = null;
        
        /// <summary>
        /// Version of the SDK rendering the ad.
        /// </summary>
        public string renderingSDKVersion = null;
        
        /// <summary>
        /// Accuracy of the netPayout value.
        /// </summary>
        public PriceAccuracy priceAccuracy;
        
        /// <summary>
        /// Type of the impression's placement.
        /// </summary>
        public PlacementType placementType;
        
        /// <summary>
        /// The variant id can represent a group in a Multi-Testing experiment.
        /// </summary>
        public string variantId = null;
        
        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
