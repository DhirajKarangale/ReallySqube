using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewards : MonoBehaviour
{
    private readonly string rewardSave = "ReawrdDelay";

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundButton;
    [SerializeField] AudioClip soundCoin;

    [SerializeField] GameObject rewardNotification;
    [SerializeField] GameObject noRewardPanel;
    [SerializeField] GameObject collectRewardPanel;
    [SerializeField] Text txtAmount;
    [SerializeField] Text txtMsg;

    // private double nextReawrdDelay = 30;
    private double nextReawrdDelay = 86400;
    private bool isRewardReady = false;
    private int rewardAmount;
    private int strinkeDays;

    private void Start()
    {
        rewardAmount = PlayerPrefs.GetInt("RewardAmonut", 200);
        strinkeDays = PlayerPrefs.GetInt("StrikeDays", 0);

        if (!PlayerPrefs.HasKey(rewardSave))
        {
            Debug.Log("Not Key");
            rewardAmount = 10;
            ActiveCollectRewards();
            PlayerPrefs.SetString(rewardSave, DateTime.Now.ToString());
        }

        StopAllCoroutines();
        StartCoroutine(IECheckReward());
    }

    IEnumerator IECheckReward()
    {
        while (true)
        {
            if (!isRewardReady)
            {
                DateTime currTime = DateTime.Now;
                DateTime rewardCollectedTime = DateTime.Parse(PlayerPrefs.GetString(rewardSave, currTime.ToString()));

                double timeDiffernce = (currTime - rewardCollectedTime).TotalSeconds;
                if (timeDiffernce > nextReawrdDelay) ActiveCollectRewards();
                else DesableCollectRewards();

            }
            yield return new WaitForSeconds(5);
        }
    }

    private void ActiveCollectRewards()
    {
        IncrementReward();

        rewardAmount = PlayerPrefs.GetInt("RewardAmonut", 200);
        isRewardReady = true;
        collectRewardPanel.SetActive(true);
        noRewardPanel.SetActive(false);
        rewardNotification.SetActive(true);

        txtAmount.text = string.Format("+{0}", rewardAmount);
    }

    private void DesableCollectRewards()
    {
        isRewardReady = false;
        collectRewardPanel.SetActive(false);
        noRewardPanel.SetActive(true);
        rewardNotification.SetActive(false);
    }

    private void IncrementReward()
    {
        DateTime currTime = DateTime.Now;
        DateTime rewardCollectedTime = DateTime.Parse(PlayerPrefs.GetString(rewardSave, currTime.ToString()));
        double timeDiffernce = (currTime - rewardCollectedTime).TotalSeconds;

        if ((timeDiffernce > nextReawrdDelay && timeDiffernce <= (2 * nextReawrdDelay)))
        {
            strinkeDays++;
            rewardAmount += 50;
            txtMsg.text = "Congrats!!! You have a strike of " + strinkeDays + " days";
        }
        else if (timeDiffernce > (2 * nextReawrdDelay))
        {
            txtMsg.text = "Sorry!!! Your strike is ended";
            rewardAmount = 25;
            strinkeDays = 0;
        }

        PlayerPrefs.SetInt("StrikeDays", strinkeDays);
        PlayerPrefs.SetInt("RewardAmonut", rewardAmount);
    }

    public void CollectButton()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        coin += rewardAmount;
        PlayerPrefs.SetInt("Coin", coin);

        PlayerPrefs.SetString(rewardSave, DateTime.Now.ToString());
        DesableCollectRewards();
        PlaySound(soundCoin);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
}
