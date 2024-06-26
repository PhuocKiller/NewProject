using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBar : Bars
{
    public bool isFullBar, isUpgradeFill;
    public ParticleSystem rageBarPar;
    private void Update()
    {
        if (!isFullBar)
        {
            if (isUpgradeFill)
            {
                fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b, 0.5f);
                isUpgradeFill = false;
            }
            else
            {
                fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b,
                    Mathf.Min( fillBar.color.a + 5 * Time.deltaTime,1)); //số a có thể tăng lớn hơn 1
            }
        }
        else //FullBar
        {
            if (isUpgradeFill)   //giảm sáng
            {
                if (PlayerController.instance.isRage) //bật Rage
                {
                    fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b, fillBar.color.a - 15 * Time.deltaTime);
                    if (fillBar.color.a < 0.2f)
                    {
                        isUpgradeFill = false;
                    }
                }
                else
                {
                    fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b, fillBar.color.a - 5 * Time.deltaTime);
                    if (fillBar.color.a < 0.5f)
                    {
                        isUpgradeFill = false;
                    }
                }
            }
            else   // tăng sáng
            {
                if (PlayerController.instance.isRage) //bật Rage
                {
                    fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b, Mathf.Min(fillBar.color.a + 15 * Time.deltaTime, 1));
                }
                else
                {
                    fillBar.color = new Color(fillBar.color.r, fillBar.color.g, fillBar.color.b, Mathf.Min(fillBar.color.a + 5 * Time.deltaTime, 1));
                }
                if(fillBar.color.a==1)
                {
                    isUpgradeFill = true;
                }
            }
        }
    }
    public override void UpdateBar(float currentValue, float maxValue)
    {
        base.UpdateBar( currentValue, maxValue);
        rageBarPar.startLifetime = fillBar.fillAmount * 0.25f;
        if (fillBar.fillAmount==1)
        {
            isFullBar = true;
        }
        else if (fillBar.fillAmount ==0)
        {
            isFullBar = false;
            rageBarPar.gameObject.SetActive(false);
        }
    }
    public void UseRage()
    {
       if (fillBar.fillAmount == 1)
        {
            PlayerController.instance.isRage = true;
            PlayerAnimation.instance.state=State.Rage;
            rageBarPar.gameObject.SetActive(true);
        }
       else
        {
            AudioManager.instance.PlaySound(AudioManager.instance.error);
        }
    }
}
