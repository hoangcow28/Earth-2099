using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
     private Button defaultGunButton;
     private Button gunLv1Button;

     private TMP_Text defaultGunStatusText;
     private TMP_Text gunLv1StatusText;
     private TMP_Text currentGunText;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void SelectDefaultGun()
    {
        int unlocked = PlayerPrefs.GetInt("Gun_Default_Unlocked", 1);
        if (unlocked == 1)
        {
            PlayerPrefs.SetString("CurrentGun", "DefaultGun");
            PlayerPrefs.Save();
            RefreshUI();
        }
    }

    public void SelectGunLv1()
    {
        int unlocked = PlayerPrefs.GetInt("GunLv1_Bought", 0);
        if (unlocked == 1)
        {
            PlayerPrefs.SetString("CurrentGun", "GunLv1");
            PlayerPrefs.Save();
            RefreshUI();
        }
        else
        {
            Debug.Log("Chua mua GunLv1");
        }
    }

    public void RefreshUI()
    {
        string currentGun = PlayerPrefs.GetString("CurrentGun", "DefaultGun");
        int gunLv1Bought = PlayerPrefs.GetInt("GunLv1_Bought", 0);

        if (defaultGunStatusText != null)
        {
            defaultGunStatusText.text = currentGun == "DefaultGun" ? "Dang dung" : "San sang";
        }

        if (gunLv1StatusText != null)
        {
            if (gunLv1Bought == 1)
            {
                gunLv1StatusText.text = currentGun == "GunLv1" ? "Dang dung" : "Da mo khoa";
            }
            else
            {
                gunLv1StatusText.text = "Chua mua";
            }
        }

        if (gunLv1Button != null)
        {
            gunLv1Button.interactable = (gunLv1Bought == 1);
        }

        if (currentGunText != null)
        {
            currentGunText.text = "Current Gun: " + currentGun;
        }
    }
}