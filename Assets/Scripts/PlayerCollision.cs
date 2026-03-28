//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCollision : MonoBehaviour
//{
//    [SerializeField] private GameManager gameManager;
//    [SerializeField] private AudioManager audioManager;

//    private void awake() 
//        {
//        if (gameManager == null)
//        {
//            gameManager = FindObjectOfType<GameManager>();
//        }
//        if (audioManager == null)
//        {
//            audioManager = FindObjectOfType<AudioManager>();
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Coin"))
//        {
//            Destroy(collision.gameObject);
//            gameManager.AddScore(10);
//        }

//        else if (collision.CompareTag("EnemyBullet"))
//        {
//            Player player = GetComponent<Player>();
//            player.TakeDamage(10f);
//        }
//        else if (collision.CompareTag("Usb"))
//        {
//            gameManager.AddUSB(1);
//            Destroy(collision.gameObject);
//        }
//        else if (collision.CompareTag("Energy"))
//        {
//            gameManager.AddEnergy();
//            Destroy(collision.gameObject);
//            audioManager.PlaySEnergySound();
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;

    private void Awake() 
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null && !coin.Collected)
            {
                coin.Collected = true;
                gameManager.AddCoin(10);
                Destroy(coin.gameObject);
            }
        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(10f);
        }
        else if (collision.CompareTag("Usb"))
        {
            Debug.Log("NHẶT USB");
            gameManager.AddUSB(1);      
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
            audioManager.PlaySEnergySound();
        }
    }
}