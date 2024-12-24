using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] Text dmgText;

    private void Awake()
    {
        dmgText = GetComponentInChildren<Text>();
    }

    public void OnInit(float damage)
    {
        dmgText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
