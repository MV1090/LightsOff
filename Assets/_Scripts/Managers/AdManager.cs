using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : Singleton<AdManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string GAME_ID = "5845686";
    private const string GAME_ID_IOS = "5845687";
    [SerializeField] bool testMode = true;
    private string _gameId;

    private string BANNER_PLACEMENT = "Banner_Android";
    private const string BANNER_PLACEMENT_IOS = "Banner_iOS";

    private string VIDEO_PLACEMENT = "Interstitial_Android";
    private const string VIDEO_PLACEMENT_IOS = "Interstitial_iOS";

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            Debug.Log(Application.platform + " supported by Advertisement");
            if (Application.platform != RuntimePlatform.Android)
            {
                BANNER_PLACEMENT = BANNER_PLACEMENT_IOS;
                VIDEO_PLACEMENT = VIDEO_PLACEMENT_IOS;
                GAME_ID = GAME_ID_IOS;
            }
            Advertisement.Initialize(GAME_ID, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void ShowBannerAD()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(BANNER_PLACEMENT);
    }

    public void HideBannerAD()
    {
        Advertisement.Banner.Hide(false);
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load ad {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed for {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ad show started for {placementId}");
        Time.timeScale = 0.0f;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ad clicked: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad show completed for {placementId} with state {showCompletionState}");
        Time.timeScale = 1.0f;
    }
}
