using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public static Gun instance;

    private float rotateOffset = 180f;

    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;

    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;

    [SerializeField] private int maxAmmo = 24;
    public int currentAmmo;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private int damage = 10;

    [Header("Gun Visual")]
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Sprite pistolSprite;
    [SerializeField] private Sprite rifleSprite;
    [SerializeField] private Sprite plasmaSprite;

    [Header("Gun Scale")]
    [SerializeField] private Vector3 pistolScale = new Vector3(0.6f, 0.64f, 1f);
    [SerializeField] private Vector3 rifleScale = new Vector3(0.28f, 0.28f, 1f);
    [SerializeField] private Vector3 plasmaScale = new Vector3(0.28f, 0.28f, 1f);

    private string currentGunId;
    private int equippedWeaponIndex;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        LoadEquippedWeapon();
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        Reload();
    }

    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        if (angle < -90 || angle > 90)
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 1);
        else
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 1);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;

            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, firePos.rotation);

            PlayerBullet b = bullet.GetComponent<PlayerBullet>();
            if (b != null)
            {
                b.SetDamage(damage);
                Debug.Log("Ban dan voi damage = " + damage + " | Gun = " + currentGunId);
            }

            currentAmmo--;
            UpdateAmmoText();

            if (audioManager != null)
                audioManager.PlayShootSound();
        }
    }

    void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();

            if (audioManager != null)
                audioManager.PlayReloadSound();
        }
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            if (currentAmmo > 0)
            {
                ammoText.text = currentAmmo.ToString();
            }
            else
            {
                ammoText .text = "Empty";
            }
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("Damage moi: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void LoadEquippedWeapon()
    {
        equippedWeaponIndex = PlayerPrefs.GetInt("equipped_weapon", 0);

        if (equippedWeaponIndex == 0)
        {
            currentGunId = "PistolDefault";
            damage = 10;

            if (gunSpriteRenderer != null)
                gunSpriteRenderer.sprite = pistolSprite;

            transform.localScale = pistolScale;
        }
        else if (equippedWeaponIndex == 1)
        {
            currentGunId = "RifleLv1";
            damage = 20;

            if (gunSpriteRenderer != null)
                gunSpriteRenderer.sprite = rifleSprite;

            transform.localScale = rifleScale;
        }
        else if (equippedWeaponIndex == 2)
        {
            currentGunId = "PlasmaGun";
            damage = 35;

            if (gunSpriteRenderer != null)
                gunSpriteRenderer.sprite = plasmaSprite;

            transform.localScale = plasmaScale;
        }
        else
        {
            currentGunId = "PistolDefault";
            damage = 10;

            if (gunSpriteRenderer != null)
                gunSpriteRenderer.sprite = pistolSprite;

            transform.localScale = pistolScale;
        }

        Debug.Log("Equipped Weapon Index = " + equippedWeaponIndex + " | Gun = " + currentGunId + " | Damage = " + damage);
    }
}