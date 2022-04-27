using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private const string ropeTimeSave = "RopeTime";
    private const string agentTimeSave = "AgentTime";

    [SerializeField] GameObject objShop;
    [Header("Text")]
    [SerializeField] Text txtCoinCnt;
    [SerializeField] Text txtRealityStoneCnt;
    [SerializeField] Text txtTimeStoneCnt;
    [SerializeField] Text txtMsg;
    [SerializeField] Text txtRopeBuy;
    [SerializeField] Text txtAgentBuy;

    [Header("Image")]
    [SerializeField] GameObject imageRopeBuy;
    [SerializeField] GameObject imageAgentBuy;

    [Header("Buttons")]
    [SerializeField] Button buttonRope;
    [SerializeField] Button buttonAgent;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundBuy;
    [SerializeField] AudioClip soundBuyFail;
    [SerializeField] AudioClip soundButton;

    [Header("Item Status")]
    public bool isRopeActive;
    public bool isAgentActive;

    private void Awake()
    {
        instance = this;
        txtMsg.CrossFadeAlpha(0, 0.5f, false); // Text fade (Gone)
    }

    private void Update()
    {
        CheckRopeTime();
        CheckAgentTime();
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
        StopAllCoroutines();
        StartCoroutine(IEDisplayMsg());
    }

    private void NotEnoughCoinMsg()
    {
        switch (UnityEngine.Random.Range(1, 6))
        {
            case 1:
                DisplayMsg("Get Some more Coins Idiot");
                break;
            case 2:
                DisplayMsg("Not Enough Coins");
                break;
            case 3:
                DisplayMsg("Need more Coins");
                break;
            case 4:
                DisplayMsg("Ha ha Get more Coins first");
                break;
            case 5:
                DisplayMsg("Want more Coins");
                break;
        }
    }

    private void CheckRopeTime()
    {
        DateTime ropeTime = DateTime.Parse(PlayerPrefs.GetString(ropeTimeSave, "24-04-2002 11:11:11"));
        double timeDiffernce = (DateTime.Now - ropeTime).TotalSeconds;
        if (timeDiffernce > 0 && timeDiffernce <= 600)
        {
            isRopeActive = true;
            buttonRope.interactable = false;
            txtRopeBuy.alignment = TextAnchor.MiddleCenter;
            txtRopeBuy.text = "Active";
            imageRopeBuy.SetActive(false);
        }
        else
        {
            isRopeActive = false;
            buttonRope.interactable = true;
            txtRopeBuy.alignment = TextAnchor.MiddleRight;
            txtRopeBuy.text = "700";
            imageRopeBuy.SetActive(true);
        }
    }

    private void CheckAgentTime()
    {
        DateTime agentTime = DateTime.Parse(PlayerPrefs.GetString(agentTimeSave, "24-04-2002 11:11:11"));
        double timeDiffernce = (DateTime.Now - agentTime).TotalSeconds;
        if (timeDiffernce > 0 && timeDiffernce <= 900)
        {
            isAgentActive = true;
            buttonAgent.interactable = false;
            txtAgentBuy.alignment = TextAnchor.MiddleCenter;
            txtAgentBuy.text = "Active";
            imageAgentBuy.SetActive(false);
        }
        else
        {
            isAgentActive = false;
            buttonAgent.interactable = transform;
            txtAgentBuy.alignment = TextAnchor.MiddleRight;
            txtAgentBuy.text = "1000";
            imageAgentBuy.SetActive(true);
        }
    }


    private IEnumerator IEVibrateTxt()
    {
        txtCoinCnt.color = Color.red;
        txtCoinCnt.transform.localScale = Vector3.one * 1.1f;
        txtCoinCnt.transform.localPosition = txtCoinCnt.transform.localPosition + new Vector3(-40, 0, 0);
        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 7);
        txtCoinCnt.transform.localPosition = txtCoinCnt.transform.localPosition + new Vector3(80, 0, 0);
        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 7);
        txtCoinCnt.transform.localPosition = txtCoinCnt.transform.localPosition + new Vector3(-40, 0, 0);
        txtCoinCnt.transform.localScale = Vector3.one;
        txtCoinCnt.color = Color.yellow;
    }

    private IEnumerator IEDisplayMsg()
    {
        txtMsg.CrossFadeAlpha(1, 0.5f, true); // Text come 
        yield return new WaitForSecondsRealtime(3);
        txtMsg.CrossFadeAlpha(0, 0.5f, true); // Text fade (Gone)
    }

    private IEnumerator IEUpdateTxt(Text txt, int amount)
    {
        txt.transform.localScale = Vector3.one * 1.5f;
        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 5);
        txt.transform.localScale = Vector3.one;
        txt.text = CntTxtSuffix(amount);
    }



    public void ShopButton(bool isActive)
    {
        audioSource.PlayOneShot(soundButton);
        objShop.SetActive(isActive);

        if (UIManager.instance) UIManager.instance.ResumeButton();
        if (isActive) Time.timeScale = 0;
        StartCoroutine(IEUpdateTxt(txtCoinCnt, CollectableData.instance.coin));
        StartCoroutine(IEUpdateTxt(txtRealityStoneCnt, CollectableData.instance.realityStone));
        StartCoroutine(IEUpdateTxt(txtTimeStoneCnt, CollectableData.instance.timeStone));
    }



    public void RealityStoneButton()
    {
        if (CollectableData.instance.coin >= 100)
        {
            audioSource.PlayOneShot(soundBuy);
            UIManager.instance.UpdateCoin(-100);
            UIManager.instance.UpdateRealityStone(5);

            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    DisplayMsg("You got 5 Reality Stones");
                    break;
                case 2:
                    DisplayMsg("Congratulations for 5 Reality Stones");
                    break;
                case 3:
                    DisplayMsg("OOo ho 5 Reality Stones");
                    break;
            }

            StartCoroutine(IEUpdateTxt(txtCoinCnt, CollectableData.instance.coin));
            StartCoroutine(IEUpdateTxt(txtRealityStoneCnt, CollectableData.instance.realityStone));
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }

    public void TimeStoneButton()
    {
        if (CollectableData.instance.coin >= 300)
        {
            audioSource.PlayOneShot(soundBuy);
            UIManager.instance.UpdateCoin(-100);
            UIManager.instance.UpdateTimeStone(5);

            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    DisplayMsg("You got 5 Time Stones");
                    break;
                case 2:
                    DisplayMsg("Congratulations for 5 Time Stones");
                    break;
                case 3:
                    DisplayMsg("OOo ho 5 Time Stones");
                    break;
            }

            StartCoroutine(IEUpdateTxt(txtCoinCnt, CollectableData.instance.coin));
            StartCoroutine(IEUpdateTxt(txtTimeStoneCnt, CollectableData.instance.timeStone));
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }

    public void RopeButton()
    {
        if (CollectableData.instance.coin >= 700)
        {
            audioSource.PlayOneShot(soundBuy);
            UIManager.instance.UpdateCoin(-700);
            PlayerPrefs.SetString(ropeTimeSave, DateTime.Now.ToString());

            isRopeActive = true;
            buttonRope.interactable = false;
            txtRopeBuy.alignment = TextAnchor.MiddleCenter;
            txtRopeBuy.text = "Active";
            imageRopeBuy.SetActive(false);

            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    DisplayMsg("Now Rope is yours (for 10 min)");
                    break;
                case 2:
                    DisplayMsg("Congratulations you got Rope");
                    break;
                case 3:
                    DisplayMsg("Double Tap in game to use rope now");
                    break;
            }

            StartCoroutine(IEUpdateTxt(txtCoinCnt, CollectableData.instance.coin));
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }

    public void AgentButton()
    {
        if (CollectableData.instance.coin >= 1000)
        {
            audioSource.PlayOneShot(soundBuy);
            UIManager.instance.UpdateCoin(-1000);
            PlayerPrefs.SetString(agentTimeSave, DateTime.Now.ToString());

            isAgentActive = true;
            buttonAgent.interactable = false;
            txtAgentBuy.alignment = TextAnchor.MiddleCenter;
            txtAgentBuy.text = "Active";
            imageAgentBuy.SetActive(false);

            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    DisplayMsg("Wow you are agent now (for 15 min)");
                    break;
                case 2:
                    DisplayMsg("Congratulations you got Agent Mode");
                    break;
                case 3:
                    DisplayMsg("Kill them & show who are you, Agent");
                    break;
            }

            StartCoroutine(IEUpdateTxt(txtCoinCnt, CollectableData.instance.coin));
            PlayerHealth.instance.psUpgrade.Play();
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }
}
