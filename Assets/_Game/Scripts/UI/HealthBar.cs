using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] Vector3 offset;
    
    private float _hp;
    private float _hpMax;
    
    private Transform target;
    /*
    private void Awake()
    {
        Transform childTransform = transform.Find("Fill");
        imageFill = childTransform.GetComponentInChildren<Image>();
    }
    */

    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, _hp / _hpMax, Time.deltaTime * 5f);
        transform.position = target.position + offset;
    }

    public void OnInit(float hpMax, Transform target)
    {
        this.target = target;
        this._hpMax = hpMax;
        _hp = hpMax;
        imageFill.fillAmount = 1;
    }

    public void SetNewHealth(float hp)
    {
        Debug.Log("SetNewHealth");
        this._hp = hp;

        //imageFill.fillAmount = _hp / _hpMax;
    }
}