using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeallhBarController : MonoBehaviour
{
    public Image HealthBar;
    private PlayerHealth Health;

    private void Awake()
    {
        Health = GetComponent<PlayerHealth>();
        Health.OnDamageEvent += OnHealthBarDamaged;
        if (HealthBar)
        {
            HealthBar.fillAmount = 1;
        }
    }

    private void OnHealthBarDamaged(float percent)
    {
        StopAllCoroutines();
        if (HealthBar)
        {
            HealthBar.fillAmount = percent;
        }
        StartCoroutine(ShowHealthBar(2f));
    }

    private IEnumerator ShowHealthBar(float HideDelay)
    {
        var healthBarHideColor = HealthBar.color;
        healthBarHideColor.a = 0;
        HealthBar.color = healthBarHideColor;
        yield return new WaitForSeconds(HideDelay);
        var healthBarShowColor = HealthBar.color;
        healthBarShowColor.a = 1;
        HealthBar.color = healthBarShowColor;
    }
}
