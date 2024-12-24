using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    [SerializeField] private float hp;
    public bool isDead => hp <= 0; //Neu dead thi hp <= 0

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // Lấy Animator trong object con gắn trên Player
        /*GameObject targetObject = GameObject.Find("UI_Health");
        healthBar = targetObject.GetComponent<HealthBar>();*/
       combatTextPrefab = Resources.Load<CombatText>("Prefabs/UI_CombatTxt");
    }

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100f, transform);
    }

    public virtual void OnDespawn()
    {
    }

    public virtual void OnDeath()
    {
        animator.SetTrigger("die");

        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            if (isDead)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHealth(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }
}