using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-2204003876045412/3076150127"; // Replace with your ad unit id.

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        // Optional: Add event handlers
        // this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Subscribe to additional events as needed.
    }

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
        // Code to execute when the banner ad is loaded
    }

    // Make sure to implement OnDestroy method and call bannerView.Destroy()
    // to avoid memory leaks when the ad or the game object is destroyed.
    public void OnDestroy()
    {
        bannerView.Destroy();
    }
}