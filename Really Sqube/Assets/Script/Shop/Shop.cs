using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private readonly string ropeTimeSave = "RopeTime";
    private readonly string agentTimeSave = "AgentTime";

    [SerializeField] GameObject objShop;
    [Header("Text")]
    [SerializeField] Text txtCoinCnt;
    [SerializeField] Text txtRealityStoneCnt;
    [SerializeField] Text txtTimeStoneCnt;
    [SerializeField] Text txtMsg;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundButton;
    [SerializeField] AudioClip soundBuyed;

    [Header("Item Status")]
    public bool isRopeActive;
    public bool isAgentActive;

    private void Awake()
    {
        instance = this;

        StartCoroutine(IECheckRope());
        StartCoroutine(IECheckAgent());
    }

    private void UpdateCollectableCntTxt()
    {
        txtCoinCnt.text = CntTxtSuffix(CollectableData.instance.coin);
        txtRealityStoneCnt.text = CntTxtSuffix(CollectableData.instance.realityStone);
        txtTimeStoneCnt.text = CntTxtSuffix(CollectableData.instance.timeStone);
    }

    private string CntTxtSuffix(int amount)
    {
        string amountStr = amount.ToString();
        if (amountStr.Length > 5) return amount.ToString("0,,.##M");
        else if (amountStr.Length > 3) return amount.ToString("0,.##K");
        else return amount.ToString();
    }

    private void DisplayMsg(string msg)
    {
        txtMsg.text = msg;
        StartCoroutine(IEFadeTxt());
    }

    private IEnumerator IEFadeTxt()
    {
        txtMsg.CrossFadeAlpha(1, 0.5f, false); // Text come 
        yield return new WaitForSeconds(3);
        txtMsg.CrossFadeAlpha(0, 0.5f, false); // Text fade (Gone)
    }




    public void ShopButton(bool isActive)
    {
        audioSource.PlayOneShot(soundButton);
        objShop.SetActive(isActive);
        UpdateCollectableCntTxt();
    }




    public void RealityStoneButton()
    {
        if (CollectableData.instance.coin >= 100)
        {
            audioSource.PlayOneShot(soundBuyed);
            UIManager.instance.UpdateCoin(-100);
            UIManager.instance.UpdateRealityStone(5);
            UpdateCollectableCntTxt();
        }
        else
        {
            DisplayMsg("Get some coins idiot");
        }
    }

    public void TimeStoneButton()
    {
        if (CollectableData.instance.coin >= 300)
        {
            audioSource.PlayOneShot(soundBuyed);
            UIManager.instance.UpdateCoin(-100);
            UIManager.instance.UpdateTimeStone(5);
            UpdateCollectableCntTxt();
        }
        else
        {
            DisplayMsg("Get some coins idiot");
        }
    }

    public void RopeButton()
    {
        if (CollectableData.instance.coin >= 700)
        {
            audioSource.PlayOneShot(soundBuyed);
            UIManager.instance.UpdateCoin(-700);
            PlayerPrefs.SetString(ropeTimeSave, DateTime.Now.ToString());
            isRopeActive = true;
            UpdateCollectableCntTxt();
        }
        else
        {
            DisplayMsg("Get some coins idiot");
        }
    }

    private IEnumerator IECheckRope()
    {
        while (true)
        {
            DateTime ropeTime = DateTime.Parse(PlayerPrefs.GetString(ropeTimeSave, "24-04-2002 11:11:11"));
            double timeDiffernce = (DateTime.Now - ropeTime).TotalSeconds;
            if (timeDiffernce > 0 && timeDiffernce <= 600) isRopeActive = true;
            else isRopeActive = false;

            yield return new WaitForSeconds(5);
        }
    }

    public void AgentButton()
    {
        if (CollectableData.instance.coin >= 1000)
        {
            audioSource.PlayOneShot(soundBuyed);
            UIManager.instance.UpdateCoin(-1000);
            PlayerPrefs.SetString(agentTimeSave, DateTime.Now.ToString());
            isAgentActive = true;
            UpdateCollectableCntTxt();
        }
        else
        {
            DisplayMsg("Get some coins idiot");
        }
    }

    private IEnumerator IECheckAgent()
    {
        while (true)
        {
            DateTime agentTime = DateTime.Parse(PlayerPrefs.GetString(agentTimeSave, "24-04-2002 11:11:11"));
            double timeDiffernce = (DateTime.Now - agentTime).TotalSeconds;
            if (timeDiffernce > 0 && timeDiffernce <= 600) isAgentActive = true;
            else isAgentActive = false;

            yield return new WaitForSeconds(5);
        }
    }
}
