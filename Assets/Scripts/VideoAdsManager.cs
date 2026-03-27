using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoAdsManager : MonoBehaviour
{
    public static VideoAdsManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject adsPanel;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private Button skipButton;

    [Header("Video")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Settings")]
    [SerializeField] private float skipDelay = 3f;
    [SerializeField] private bool rewardWhenSkipped = true;

    private bool canSkip = false;
    private bool rewardGranted = false;
    private Coroutine adCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (adsPanel != null)
            adsPanel.SetActive(false);

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.AddListener(OnClickSkip);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDestroy()
    {
        if (skipButton != null)
            skipButton.onClick.RemoveListener(OnClickSkip);

        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void ShowReviveAd()
    {
        if (adsPanel == null || countdownText == null || skipButton == null || videoPlayer == null)
        {
            Debug.LogError("VideoAdsManager: Chưa gán đủ reference trong Inspector.");
            return;
        }

        if (adCoroutine != null)
            StopCoroutine(adCoroutine);

        adCoroutine = StartCoroutine(PlayAdRoutine());
    }

    private IEnumerator PlayAdRoutine()
    {
        canSkip = false;
        rewardGranted = false;

        // Ẩn menu game over trong lúc xem quảng cáo
        if (GameManager.instance != null)
        {
            GameManager.instance.HideGameOverMenuOnly();
        }

        adsPanel.SetActive(true);
        skipButton.gameObject.SetActive(false);
        countdownText.text = "Đang tải quảng cáo...";

        videoPlayer.Stop();
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();

        float timer = skipDelay;

        while (timer > 0f && videoPlayer.isPlaying)
        {
            countdownText.text = "Có thể bỏ qua sau: " + Mathf.CeilToInt(timer) + " giây";
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }

        if (videoPlayer.isPlaying)
        {
            canSkip = true;
            skipButton.gameObject.SetActive(true);
            countdownText.text = "Bạn có thể bỏ qua";
        }

        adCoroutine = null;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        rewardGranted = true;
        CloseAdAndGiveReward();
    }

    private void OnClickSkip()
    {
        if (!canSkip)
            return;

        videoPlayer.Stop();

        if (rewardWhenSkipped)
        {
            rewardGranted = true;
            CloseAdAndGiveReward();
        }
        else
        {
            CloseAdWithoutReward();
        }
    }

    private void CloseAdAndGiveReward()
    {
        if (adsPanel != null)
            adsPanel.SetActive(false);

        if (skipButton != null)
            skipButton.gameObject.SetActive(false);

        canSkip = false;

        if (rewardGranted)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.RevivePlayer();
            }
            else
            {
                Debug.LogError("GameManager.instance đang null.");
            }
        }

        rewardGranted = false;
    }

    private void CloseAdWithoutReward()
    {
        if (adsPanel != null)
            adsPanel.SetActive(false);

        if (skipButton != null)
            skipButton.gameObject.SetActive(false);

        canSkip = false;
        rewardGranted = false;

        // Nếu không nhận thưởng thì hiện lại game over
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowGameOverMenuOnly();
        }
    }
}