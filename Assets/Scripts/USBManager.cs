//using System;
//using UnityEngine;

//public class USBManager : MonoBehaviour
//{
//    public static USBManager Instance;

//    public int usbInRun = 0;
//    public int totalUSB = 0;

//    public event Action OnUSBChanged; 

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            LoadTotalUSB();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void AddUSBInRun(int amount)
//    {
//        usbInRun += amount;
//        Debug.Log("USB in run = " + usbInRun);
//    }

//    public void SaveRunUSBToTotal()
//    {
//        Debug.Log("Saving USB. usbInRun = " + usbInRun);

//        PlayerPrefs.SetInt("LastRunUSB", usbInRun);

//        totalUSB += usbInRun;
//        PlayerPrefs.SetInt("TotalUSB", totalUSB);
//        PlayerPrefs.Save();

//        Debug.Log("Saved TotalUSB = " + totalUSB);

//        usbInRun = 0;

//        OnUSBChanged?.Invoke(); // 🔥 QUAN TRỌNG
//    }

//    public void LoadTotalUSB()
//    {
//        totalUSB = PlayerPrefs.GetInt("TotalUSB", 0);
//        Debug.Log("Loaded TotalUSB = " + totalUSB);
//    }

//    public void ForceUpdateUI()
//    {
//        OnUSBChanged?.Invoke();
//    }

//    public bool SpendUSB(int amount)
//    {
//        if (totalUSB >= amount)
//        {
//            totalUSB -= amount;
//            PlayerPrefs.SetInt("TotalUSB", totalUSB);
//            PlayerPrefs.Save();

//            OnUSBChanged?.Invoke(); // 🔥 QUAN TRỌNG

//            return true;
//        }

//        return false;
//    }
//}
using System;
using UnityEngine;

public class USBManager : MonoBehaviour
{
    public static USBManager Instance;

    public int usbInRun = 0;
    public int totalUSB = 0;

    public event Action OnUSBChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadTotalUSB();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddUSBInRun(int amount)
    {
        usbInRun += amount;
        Debug.Log("USB in run = " + usbInRun);

        OnUSBChanged?.Invoke(); 
    }

    public void SaveRunUSBToTotal()
    {
        Debug.Log("Saving USB. usbInRun = " + usbInRun);

        PlayerPrefs.SetInt("LastRunUSB", usbInRun);

        totalUSB += usbInRun;
        PlayerPrefs.SetInt("TotalUSB", totalUSB);
        PlayerPrefs.Save();

        Debug.Log("Saved TotalUSB = " + totalUSB);

        usbInRun = 0;

        OnUSBChanged?.Invoke();
    }

    public void LoadTotalUSB()
    {
        totalUSB = PlayerPrefs.GetInt("TotalUSB", 0);
        Debug.Log("Loaded TotalUSB = " + totalUSB);
    }

    public void ForceUpdateUI()
    {
        OnUSBChanged?.Invoke();
    }

    public bool SpendUSB(int amount)
    {
        if (totalUSB >= amount)
        {
            totalUSB -= amount;
            PlayerPrefs.SetInt("TotalUSB", totalUSB);
            PlayerPrefs.Save();

            OnUSBChanged?.Invoke();

            return true;
        }

        return false;
    }
    public bool UseUSB(int amount)
    {
        if (usbInRun >= amount)
        {
            usbInRun -= amount;

            // gọi event update UI
            OnUSBChanged?.Invoke();

            return true;
        }

        return false;
    }
}