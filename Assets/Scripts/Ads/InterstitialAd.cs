using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    [SerializeField] private BannerAd bannerAd;
    string _adUnitId;
    [SerializeField] private int timeToSkip = 2;

    void Awake()
    {
        // 获取当前平台的 Ad Unit ID（广告单元 ID）：
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        int skipNumber = PlayerPrefs.GetInt("Interstitial", timeToSkip);
        if (timeToSkip != 0)
        {
            skipNumber -= 1;
            PlayerPrefs.SetInt("Interstitial", skipNumber);
        }
        else
        {
            LoadAd();
            PlayerPrefs.SetInt("Interstitial", timeToSkip);
        }
    }

    public void LoadAd()
    {
        if (Advertisement.isInitialized)
            Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("show Failed");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("show Start");
        Advertisement.Banner.Hide();
        Time.timeScale = 0;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("show click");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("show Complete");
        Time.timeScale = 1;
        bannerAd.LoadBannerAd();
    }
}