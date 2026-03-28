using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f;
    protected Player player;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float enterDamage = 10f; // khi cham vao player
    [SerializeField] protected float stayDamage = 1f;  // khi cham roi ma player khong chay
    private EnenmySpawner spawner;
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
        UpdateHpBar();
    }
    protected virtual void Update()
    {
        MoveToPlayer();
    }
    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected void FlipEnemy() //lat
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1); // neu player nam ben trai enemy thi scale la -1 khong thi la 1
        }
    }
    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Enemy TakeDamage | damage = " + damage + " | HP truoc = " + currentHp);

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);

        Debug.Log("Enemy HP sau = " + currentHp);

        UpdateHpBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDied(); // báo về spawner
        }
        GameManager.instance.AddScore(10);
        Destroy(gameObject);
    }
    public void Init(EnenmySpawner spawnerRef) // biết ai tạo ra
    {
        spawner = spawnerRef;
    }
    protected void UpdateHpBar()
    {
        if(hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
}


