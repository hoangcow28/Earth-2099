using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponCardUI : MonoBehaviour
{
    [Header("UI References")]
    public Image gunIcon;
    public TMP_Text gunNameText;
    public TMP_Text damageText;
    public TMP_Text priceText;
    public Button actionButton;
    public TMP_Text actionButtonText;

    [HideInInspector] public WeaponData weaponData;
    [HideInInspector] public ShopManager shopManager;

    public void Setup(WeaponData data, ShopManager manager)
    {
        weaponData = data;
        shopManager = manager;

        gunNameText.text = weaponData.weaponName;
        damageText.text = "Damage: " + weaponData.damage;
        priceText.text = "Price: " + weaponData.price + " coin";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnClickActionButton);

        RefreshUI();
    }

    public void RefreshUI()
    {
        // 🔥 Lấy coin trực tiếp từ CoinManager
        int currentCoin = 0;

        if (CoinManager.Instance != null)
        {
            currentCoin = CoinManager.Instance.totalCoin;
        }

        if (!weaponData.isUnlocked)
        {
            if (currentCoin >= weaponData.price)
            {
                actionButtonText.text = "Buy";
                actionButton.interactable = true;
            }
            else
            {
                actionButtonText.text = "Locked";
                actionButton.interactable = false;
            }
        }
        else
        {
            if (weaponData.isEquipped)
            {
                actionButtonText.text = "Equipped";
                actionButton.interactable = false;
            }
            else
            {
                actionButtonText.text = "Equip";
                actionButton.interactable = true;
            }
        }
    }

    private void OnClickActionButton()
    {
        if (!weaponData.isUnlocked)
        {
            if (shopManager != null)
            {
                shopManager.BuyWeapon(weaponData.weaponId);
            }
        }
        else if (!weaponData.isEquipped)
        {
            if (shopManager != null)
            {
                shopManager.EquipWeapon(weaponData.weaponId);
            }
        }
    }
}