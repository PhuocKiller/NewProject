using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    bool canTalk;
    public GameObject talkPanel, noKeyPanel;
    public Transform fightBossPos;
    Vector2 localScaleGuest;
    private void Start()
    {
        localScaleGuest=transform.localScale;
    }

    private void OnMouseDown()
    {
        if (canTalk &&  !talkPanel.activeInHierarchy&&!noKeyPanel.activeInHierarchy)
        {
            talkPanel.SetActive(true);
            AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        }
    }
    public void YesButton()
    {        UIManager.instance.CheckKeyInventory(this);
            talkPanel.SetActive(false);
    }
    public void ActiveNoKeyPanel()
    {
            noKeyPanel.SetActive(true);
            AudioManager.instance.PlaySound(AudioManager.instance.error);
    }
    public void NoButton()
    {
        talkPanel.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
            transform.localScale = 1.1f * localScaleGuest;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
            talkPanel.SetActive(false);
            noKeyPanel.SetActive(false);
            transform.localScale =localScaleGuest;
        }
    }
    public void OKButtonInKeyPanel ()
    {
        noKeyPanel.SetActive(false);
        talkPanel.SetActive(false);
    }
}
