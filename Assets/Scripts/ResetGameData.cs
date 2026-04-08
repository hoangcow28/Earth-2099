using UnityEngine;

public class ResetGameData : MonoBehaviour
{
    public int resetCoin = 0;

    //public void ResetAll()
    //{
    //    PlayerPrefs.DeleteAll();

    //    PlayerPrefs.SetInt("TotalCoin", resetCoin);

    //    PlayerPrefs.SetInt("PistolDefault_Bought", 1);
    //    PlayerPrefs.SetInt("GunDamage_PistolDefault", 10);
    //    PlayerPrefs.SetString("CurrentGun", "PistolDefault");

    //    PlayerPrefs.Save();

    //    if (CoinManager.Instance != null)
    //    {
    //        CoinManager.Instance.totalCoin = resetCoin;
    //        CoinManager.Instance.coinInRun = 0;

    //        CoinManager.Instance.ForceUpdateUI();
    //    }
    //    Debug.Log("RESET XONG");
    //}
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();

        // ===== COIN =====
        PlayerPrefs.SetInt("TotalCoin", resetCoin);

        // ===== USB =====
        PlayerPrefs.SetInt("TotalUSB", 0);
        PlayerPrefs.SetInt("LastRunUSB", 0);

        // ===== GUN =====
        PlayerPrefs.SetInt("PistolDefault_Bought", 1);
        PlayerPrefs.SetInt("GunDamage_PistolDefault", 10);
        PlayerPrefs.SetString("CurrentGun", "PistolDefault");

        // reset level súng luôn (quan trọng)
        PlayerPrefs.SetInt("Pistol_level", 1);
        PlayerPrefs.SetInt("Rifle_level", 1);
        PlayerPrefs.SetInt("Plasma_level", 1);

        PlayerPrefs.Save();

        // ===== UPDATE COIN =====
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.totalCoin = resetCoin;
            CoinManager.Instance.coinInRun = 0;
            CoinManager.Instance.ForceUpdateUI();
        }

        // ===== UPDATE USB =====
        if (USBManager.Instance != null)
        {
            USBManager.Instance.totalUSB = 0;
            USBManager.Instance.usbInRun = 0;
            USBManager.Instance.ForceUpdateUI();
        }

        Debug.Log("RESET XONG TOÀN BỘ");
    }
}