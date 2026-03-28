using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUpgradeUI : MonoBehaviour
{
    private Gun gun;

    public Image gunImage;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textUSB;

    [SerializeField] private Button upgradeButton;

    public GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI textUSBPlayer;
    [SerializeField] private TextMeshProUGUI textUSBNeed;
 

    void OnEnable()
{
    gun = Gun.instance;
    gun.LoadEquippedWeapon();

    // 🔥 cập nhật lại dữ liệu từ PlayerPrefs
    if (USBManager.Instance != null)
    {
        USBManager.Instance.LoadTotalUSB();   // load lại
        USBManager.Instance.ForceUpdateUI(); // bắn event
    }

    UpdateUI();

    if (USBManager.Instance != null)
    {
        USBManager.Instance.OnUSBChanged += UpdateUI;
    }
}

    void OnDisable()
    {
        if (USBManager.Instance != null)
        {
            USBManager.Instance.OnUSBChanged -= UpdateUI;
        }
    }
    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
    }
    void UpdateUI()
    {
        int level = gun.GetGunLevel();

        textName.text = gun.GetGunName();
        gunImage.sprite = gun.GetGunSprite();

        textLevel.text = "Level: " + level;
        textDamage.text = "Damage: " + gun.GetDamage();

        int need = gun.GetUpgradeCost();
        int currentUSB = USBManager.Instance.totalUSB;

        textUSBNeed.text = "Need: " + need;
        textUSBPlayer.text = $"{currentUSB}";

        upgradeButton.interactable = currentUSB >= need;
    }
    public void Upgrade()
    { 
        int need = gun.GetUpgradeCost();

        if (USBManager.Instance != null && USBManager.Instance.totalUSB >= need)
        {
            USBManager.Instance.SpendUSB(need);
            gun.UpgradeGun();
            UpdateUI();
        }
        else
        {
            Debug.Log("Không đủ USB!");
        }
    }
}