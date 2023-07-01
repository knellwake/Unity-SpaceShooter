using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // 对于不受支持的平台，此值将保持为 null。

    private void Start()
    {
        // 获取当前平台的 Ad Unit ID（广告单元 ID）：
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // 设置横幅广告位置：
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBannerAd();
    }

    /// <summary>
    /// 现一个在单击 Load Banner（加载横幅广告）按钮时调用的方法：
    /// </summary>
    public void LoadBannerAd()
    {
        if (Advertisement.isInitialized)
        {
            // 设置选项以将加载事件告知 SDK：
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // 向广告单元加载横幅广告内容：
            Advertisement.Banner.Load(_adUnitId, options);
        }
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

        Advertisement.Banner.Show(_adUnitId);
    }
}