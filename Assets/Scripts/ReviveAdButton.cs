using UnityEngine;

public class ReviveAdButton : MonoBehaviour
{
    public void OnClickWatchAd()
    {
        VideoAdsManager.Instance.ShowReviveAd();
    }
}