using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private const string ropeTimeSave = "RopeTime";
    private const string agentTimeSave = "AgentTime";
    private UIManager uiManager;
    private CollectableData collectableData;

    [SerializeField] GameObject objShop;
    [SerializeField] GameObject healthPack;
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

    private void Start()
    {
        collectableData = CollectableData.instance;
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        CheckAgentTime();
        CheckRopeTime();
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
        string ropeTimeStr = PlayerPrefs.GetString(ropeTimeSave, string.Empty);
        if (!string.IsNullOrEmpty(ropeTimeStr))
        {
            DateTime ropeTime = DateTime.Parse(ropeTimeStr);
            double timeDiffernce = (DateTime.Now - ropeTime).TotalSeconds;
            if (timeDiffernce > 0 && timeDiffernce <= 600)
            {
                isRopeActive = true;
                buttonRope.interactable = false;
                txtRopeBuy.alignment = TextAnchor.MiddleCenter;
                txtRopeBuy.text = "Active";
                if (uiManager.imgRopeActive) uiManager.imgRopeActive.SetActive(true);
                imageRopeBuy.SetActive(false);
            }
            else
            {
                isRopeActive = false;
                buttonRope.interactable = true;
                txtRopeBuy.alignment = TextAnchor.MiddleRight;
                txtRopeBuy.text = "700";
                uiManager.imgRopeActive.SetActive(false);
                imageRopeBuy.SetActive(true);
            }
        }
    }

    private void CheckAgentTime()
    {
        string agentTimeStr = PlayerPrefs.GetString(agentTimeSave, string.Empty);
        if (!string.IsNullOrEmpty(agentTimeStr))
        {
            DateTime agentTime = DateTime.Parse(agentTimeStr);
            double timeDiffernce = (DateTime.Now - agentTime).TotalSeconds;
            if (timeDiffernce > 0 && timeDiffernce <= 900)
            {
                isAgentActive = true;
                buttonAgent.interactable = false;
                txtAgentBuy.alignment = TextAnchor.MiddleCenter;
                txtAgentBuy.text = "Active";
                if (uiManager.imgAgentActive) uiManager.imgAgentActive.SetActive(true);
                imageAgentBuy.SetActive(false);
            }
            else
            {
                isAgentActive = false;
                buttonAgent.interactable = transform;
                txtAgentBuy.alignment = TextAnchor.MiddleRight;
                txtAgentBuy.text = "1000";
                if (uiManager.imgAgentActive) uiManager.imgAgentActive.SetActive(false);
                imageAgentBuy.SetActive(true);
            }
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
        yield return new WaitForSecondsRealtime(0.02f * 5);
        txt.transform.localScale = Vector3.one;
        txt.text = CntTxtSuffix(amount);
    }



    public void ShopButton(bool isActive)
    {
        audioSource.PlayOneShot(soundButton);
        objShop.SetActive(isActive);

        if (uiManager) uiManager.ResumeButton();
        if (isActive) Time.timeScale = 0;
        StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));
        StartCoroutine(IEUpdateTxt(txtRealityStoneCnt, collectableData.realityStone));
        StartCoroutine(IEUpdateTxt(txtTimeStoneCnt, collectableData.timeStone));
    }






    public void CoinButton()
    {
        audioSource.PlayOneShot(soundBuy);
        switch (UnityEngine.Random.Range(1, 4))
        {
            case 1:
                DisplayMsg("Come here any time when you want");
                break;
            case 2:
                DisplayMsg("Congratulations you got coin");
                break;
            case 3:
                DisplayMsg("What you are going to do with this money");
                break;
        }

        uiManager.UpdateCoin(200);
        StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));
        collectableData.Save();
    }

    public void HealthPackButton()
    {
        if (!PlayerHealth.instance)
        {
            DisplayMsg("Player is not present");
            return;
        }

        if (collectableData.coin >= 400)
        {
            audioSource.PlayOneShot(soundBuy);
            uiManager.UpdateCoin(-400);
            uiManager.UpdateRealityStone(5);

            switch (UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    DisplayMsg("You got Health Pack");
                    break;
                case 2:
                    DisplayMsg("Congratulations for Health Pack");
                    break;
                case 3:
                    DisplayMsg("Improve your hygiene now");
                    break;
            }

            StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));

            if (PlayerHealth.instance)
            {
                Instantiate(healthPack, PlayerHealth.instance.transform.position + new Vector3(3, 5, 0), Quaternion.identity);
            }

            collectableData.Save();
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }

    public void RealityStoneButton()
    {
        if (collectableData.coin >= 100)
        {
            audioSource.PlayOneShot(soundBuy);
            uiManager.UpdateCoin(-100);
            uiManager.UpdateRealityStone(5);

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

            StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));
            StartCoroutine(IEUpdateTxt(txtRealityStoneCnt, collectableData.realityStone));

            collectableData.Save();
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
        if (collectableData.coin >= 500)
        {
            audioSource.PlayOneShot(soundBuy);
            uiManager.UpdateCoin(-500);
            uiManager.UpdateTimeStone(5);

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

            StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));
            StartCoroutine(IEUpdateTxt(txtTimeStoneCnt, collectableData.timeStone));

            collectableData.Save();
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
        if (collectableData.coin >= 700)
        {
            audioSource.PlayOneShot(soundBuy);
            uiManager.UpdateCoin(-700);
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

            StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));

            collectableData.Save();
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
        if (collectableData.coin >= 1000)
        {
            audioSource.PlayOneShot(soundBuy);
            uiManager.UpdateCoin(-1000);
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

            StartCoroutine(IEUpdateTxt(txtCoinCnt, collectableData.coin));
            PlayerHealth.instance.psUpgrade.Play();

            collectableData.Save();
        }
        else
        {
            NotEnoughCoinMsg();
            StartCoroutine(IEVibrateTxt());
            audioSource.PlayOneShot(soundBuyFail);
        }
    }


    // For Item Decription Button - Use in Shop Dec Script
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(soundButton);
    }
}
