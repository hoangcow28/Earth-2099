using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Coin UI")]
    public TMP_Text coinText;

    [Header("Weapon Cards")]
    public WeaponCardUI[] weaponCards;

    private List<WeaponData> weapons = new List<WeaponData>();

    private void Start()
    {
        LoadData();
        SetupCards();
        RefreshAllUI();

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinChanged += OnCoinChanged;
        }
    }

    private void OnDestroy()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinChanged -= OnCoinChanged;
        }
    }
    private void OnCoinChanged()
    {
        RefreshAllUI();
    }

    void LoadData()
    {
        int equippedIndex = PlayerPrefs.GetInt("equipped_weapon", 0);

        weapons = new List<WeaponData>()
        {
            new WeaponData
            {
                weaponId = "pistol",
                weaponName = "Pistol Default",
                price = 0,
                damage = 10,
                isUnlocked = PlayerPrefs.GetInt("weapon_pistol_unlocked", 1) == 1,
                isEquipped = equippedIndex == 0
            },
            new WeaponData
            {
                weaponId = "rifle",
                weaponName = "Rifle Lv1",
                price = 50,
                damage = 20,
                isUnlocked = PlayerPrefs.GetInt("weapon_rifle_unlocked", 0) == 1,
                isEquipped = equippedIndex == 1
            },
            new WeaponData
            {
                weaponId = "plasma",
                weaponName = "Plasma Gun",
                price = 120,
                damage = 35,
                isUnlocked = PlayerPrefs.GetInt("weapon_plasma_unlocked", 0) == 1,
                isEquipped = equippedIndex == 2
            }
        };
    }

    void SetupCards()
    {
        for (int i = 0; i < weaponCards.Length; i++)
        {
            weaponCards[i].Setup(weapons[i], this);
        }
    }

    // ================= REFRESH UI =================
    public void RefreshAllUI()
    {
        if (coinText != null && CoinManager.Instance != null)
        {
            coinText.text = "Coin: " + CoinManager.Instance.totalCoin;
        }

        foreach (var card in weaponCards)
        {
            card.RefreshUI();
        }
    }

    // ================= BUY =================
    public void BuyWeapon(string weaponId)
    {
        WeaponData weapon = weapons.Find(w => w.weaponId == weaponId);
        if (weapon == null) return;
        if (weapon.isUnlocked) return;

        // 🔥 Dùng CoinManager duy nhất
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoin(weapon.price))
        {
            weapon.isUnlocked = true;

            SaveWeaponUnlock(weapon.weaponId, true);

            RefreshAllUI();
        }
        else
        {
            Debug.Log("Not enough coin!");
        }
    }

    // ================= EQUIP =================
    public void EquipWeapon(string weaponId)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].isEquipped = false;
        }

        WeaponData selectedWeapon = weapons.Find(w => w.weaponId == weaponId);
        if (selectedWeapon != null)
        {
            selectedWeapon.isEquipped = true;
        }

        SaveEquippedWeapon();
        RefreshAllUI();

        Gun gun = FindObjectOfType<Gun>();
        if (gun != null)
        {
            gun.LoadEquippedWeapon();
        }
    }

    // ================= SAVE =================
    void SaveWeaponUnlock(string weaponId, bool unlocked)
    {
        PlayerPrefs.SetInt("weapon_" + weaponId + "_unlocked", unlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    void SaveEquippedWeapon()
    {
        int equippedIndex = 0;

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].isEquipped)
            {
                equippedIndex = i;
                break;
            }
        }

        PlayerPrefs.SetInt("equipped_weapon", equippedIndex);
        PlayerPrefs.Save();
    }

    // ================= OPTIONAL: GỌI KHI MỞ SHOP =================
    public void OpenShop()
    {
        LoadData();
        RefreshAllUI();
    }
}