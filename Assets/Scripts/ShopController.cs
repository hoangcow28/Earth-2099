using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject weaponPanel; // 🔥 thêm dòng này

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        weaponPanel.SetActive(false); // 🔥 tắt kho vũ khí
    }

    public void CloseShop()
    {
        Debug.Log("Clicked X");
        shopPanel.SetActive(false);
    }

    // 🔥 thêm hàm này
    public void OpenWeaponPanel()
    {
        weaponPanel.SetActive(true);
        shopPanel.SetActive(false); // tắt shop
    }

    public void CloseWeaponPanel()
    {
        weaponPanel.SetActive(false);
    }
}