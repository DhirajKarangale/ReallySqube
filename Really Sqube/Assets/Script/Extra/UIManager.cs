using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Text")]
    [SerializeField] Text txtRealityStoneCount;
    [SerializeField] Text txtTimeStoneCount;
    [SerializeField] Text txtCoinCount;
    [SerializeField] Text txtRealityStPause;
    [SerializeField] Text txtTimeStPause;
    public Text txtRealityStatus;

    [Header("Button")]
    [SerializeField] Button buttonStopTime;
    [SerializeField] Button buttonReverseTime;
    public Button buttonCheckReality;
    public Button buttonChangeReality;
    public Button buttonDown;

    [Header("Objects")]
    [SerializeField] GameObject objCoin;
    [SerializeField] GameObject objRealityStone;
    [SerializeField] GameObject objTimeStone;
    [SerializeField] GameObject pauseObj;
    [SerializeField] GameObject objDialogueManager;
    public GameObject objButtons;
    public GameObject objGameOver;
    public GameObject objUI;

    [HideInInspector] public bool isPause;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateCoin(0);
        UpdateRealityStone(0);
        UpdateTimeStone(0);
    }

    private IEnumerator IEUpdateTxt(int amount, int val1, int val2, Text txtAmount, GameObject objText, Button btn1, Button btn2)
    {
        txtAmount.transform.localScale = Vector3.one * 1.5f;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        txtAmount.transform.localScale = Vector3.one;

        string amountStr = amount.ToString();
        if (amountStr.Length > 5) txtAmount.text = amount.ToString("0,,.##M");
        else if (amountStr.Length > 3) txtAmount.text = amount.ToString("0,.##K");
        else txtAmount.text = amount.ToString();

        objText.SetActive(amount > 0);
        if (btn1) btn1.gameObject.SetActive(amount >= val1);
        if (btn2) btn2.gameObject.SetActive(amount >= val2);
    }

    public void UpdateTimeStone(int amount)
    {
        CollectableData.instance.timeStone += amount;
        StartCoroutine(IEUpdateTxt(CollectableData.instance.timeStone, 3, 5, txtTimeStoneCount, objTimeStone, buttonStopTime, buttonReverseTime));
    }

    public void UpdateRealityStone(int amount)
    {
        CollectableData.instance.realityStone += amount;
        StartCoroutine(IEUpdateTxt(CollectableData.instance.realityStone, 1, 2, txtRealityStoneCount, objRealityStone, buttonCheckReality, buttonChangeReality));
    }

    public void UpdateCoin(int amount)
    {
        CollectableData.instance.coin += amount;
        StartCoroutine(IEUpdateTxt(CollectableData.instance.coin, 1, 1, txtCoinCount, objRealityStone, null, null));
    }

    public void PauseButton()
    {
        isPause = true;
        txtRealityStPause.text = "Reality Stones        " + CollectableData.instance.realityStone.ToString();
        txtTimeStPause.text = "Time Stone            " + CollectableData.instance.timeStone.ToString();
        pauseObj.SetActive(true);
        objButtons.SetActive(false);
        objDialogueManager.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        isPause = false;
        Time.timeScale = 1;
        pauseObj.SetActive(false);
        objButtons.SetActive(true);
        objDialogueManager.SetActive(true);
    }

    public void CheckRealityButton()
    {
        ResumeButton();
        RealityStone.instance.CheckRealityButton();
    }

    public void ChangeRealityButton()
    {
        ResumeButton();
        RealityStone.instance.ChangeRealityButton();
    }

    public void ReverseTimeButton()
    {
        ResumeButton();
        TimeStone.instance.ReverseButton();
    }

    public void MoveButton(int value)
    {
        PlayerHealth.instance.playerMove.MoveButton(value);
    }

    public void JumpButton(bool isJump)
    {
        PlayerHealth.instance.playerMove.JumpButton(isJump);
    }

    public void DownButton()
    {
        PlayerHealth.instance.playerMove.DownButton();
    }

    public void RestartButton()
    {
        // PlaySound(buttonClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        // PlaySound(buttonClip);
        SceneManager.LoadScene(0);
    }

    public void ShopButton(bool isActive)
    {
        Shop.instance.ShopButton(isActive);
    }
}
