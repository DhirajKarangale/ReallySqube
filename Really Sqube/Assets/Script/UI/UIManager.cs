using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
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
    public Text txtStoneUseStatus;

    [Header("Button")]
    public Button buttonStopTime;
    public Button buttonReverseTime;
    public Button buttonCheckReality;
    public Button buttonChangeReality;
    public Button buttonDown;
    [SerializeField] EventTrigger buttonLeftMove;
    [SerializeField] EventTrigger buttonRightMove;
    [SerializeField] EventTrigger buttonJump;

    [Header("Slider")]
    public Slider healthSlider;

    [Header("Objects")]
    [SerializeField] GameObject objCoin;
    [SerializeField] GameObject objRealityStone;
    [SerializeField] GameObject objTimeStone;
    [SerializeField] GameObject pauseObj;
    [SerializeField] GameObject objDialogueManager;
    public GameObject objButtons;
    public GameObject objGameOver;
    public GameObject objUI;
    public GameObject buttonOverReverse;
    public GameObject buttonOverReverseAD;
    public GameObject imgRopeActive;
    public GameObject imgAgentActive;

    [HideInInspector] public bool isPause;
    [HideInInspector] public bool isButtonDesable;

    [Header("Sound")]
    [SerializeField] AudioSource soundButton;

    private CollectableData collectableData;
    private PlayerMove playerMove;
    private TimeStone timeStone;
    private RealityStone realityStone;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        collectableData = CollectableData.instance;
        playerMove = PlayerHealth.instance.playerMove;
        timeStone = TimeStone.instance;
        realityStone = RealityStone.instance;

        UpdateCoin(0);
        UpdateRealityStone(0);
        UpdateTimeStone(0);
        ResumeButton();
    }

    private IEnumerator IEUpdateTxt(int amount, int val1, int val2, Text txtAmount, GameObject objText, Button btn1, Button btn2)
    {
        // if (btn1) btn1.gameObject.SetActive(amount >= val1);
        // if (btn2) btn2.gameObject.SetActive(amount >= val2);
        if (btn1 && !isButtonDesable) btn1.interactable = amount >= val1;
        if (btn2 && !isButtonDesable) btn2.interactable = amount >= val2;

        txtAmount.text = PrintAmount(amount);
        objText.SetActive(amount > 0);

        txtAmount.transform.localScale = Vector3.one * 1.5f;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        txtAmount.transform.localScale = Vector3.one;
    }

    private IEnumerator DesableCollectableButtons(float time)
    {
        isButtonDesable = true;
        buttonCheckReality.interactable = false;
        buttonChangeReality.interactable = false;
        buttonStopTime.interactable = false;
        buttonReverseTime.interactable = false;

        yield return new WaitForSecondsRealtime(time);

        isButtonDesable = false;
        buttonCheckReality.interactable = true;
        buttonChangeReality.interactable = true;
        buttonStopTime.interactable = true;
        buttonReverseTime.interactable = true;
    }

    private IEnumerator IEResetButton()
    {
        buttonLeftMove.enabled = false;
        buttonRightMove.enabled = false;
        buttonJump.enabled = false;

        yield return new WaitForSecondsRealtime(0.16f);

        buttonLeftMove.enabled = true;
        buttonRightMove.enabled = true;
        buttonJump.enabled = true;
    }

    private string PrintAmount(int amount)
    {
        string amountStr = amount.ToString();
        if (amountStr.Length > 5) return amount.ToString("0,,.##M");
        else if (amountStr.Length > 3) return amount.ToString("0,.##K");
        else return amount.ToString();
    }

    public void UpdateTimeStone(int amount)
    {
        collectableData.timeStone += amount;
        StartCoroutine(IEUpdateTxt(collectableData.timeStone, 4, 6, txtTimeStoneCount, objTimeStone, buttonStopTime, buttonReverseTime));
    }

    public void UpdateRealityStone(int amount)
    {
        collectableData.realityStone += amount;
        StartCoroutine(IEUpdateTxt(collectableData.realityStone, 1, 2, txtRealityStoneCount, objRealityStone, buttonCheckReality, buttonChangeReality));
    }

    public void UpdateCoin(int amount)
    {
        collectableData.coin += amount;
        StartCoroutine(IEUpdateTxt(collectableData.coin, 1, 1, txtCoinCount, objCoin, null, null));
    }

    public void PauseButton()
    {
        UpdateCoin(0);
        UpdateRealityStone(0);
        UpdateTimeStone(0);

        soundButton.Play();
        isPause = true;

        txtRealityStPause.text = "Reality Stones        " + PrintAmount(collectableData.realityStone);
        txtTimeStPause.text = "Time Stone            " + PrintAmount(collectableData.timeStone);

        pauseObj.SetActive(true);
        objButtons.SetActive(false);
        objDialogueManager.SetActive(false);
        objUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        soundButton.Play();
        isPause = false;
        Time.timeScale = 1;
        pauseObj.SetActive(false);
        objButtons.SetActive(true);
        objDialogueManager.SetActive(true);
        objUI.SetActive(true);
        objGameOver.SetActive(false);
        UpdateCoin(0);
        UpdateRealityStone(0);
        UpdateTimeStone(0);
    }

    public void CheckRealityButton()
    {
        realityStone.CheckRealityButton();
        ResumeButton();
        StartCoroutine(DesableCollectableButtons(20));
    }

    public void ChangeRealityButton()
    {
        realityStone.ChangeRealityButton();
        ResumeButton();
        StartCoroutine(DesableCollectableButtons(26));
    }

    public void ReverseTimeButton(bool isUseStone)
    {
        ResumeButton();
        StartCoroutine(DesableCollectableButtons(6f));
        timeStone.ReverseButton(isUseStone);
    }

    public void StopTimeButton()
    {
        ResumeButton();
        StartCoroutine(DesableCollectableButtons(31f));
        timeStone.StopTimeButton();
    }

    public void MoveButton(int value)
    {
        playerMove.MoveButton(value);
    }

    public void JumpButton(bool isJump)
    {
        playerMove.JumpButton(isJump);
    }

    public void DownButton()
    {
        playerMove.DownButton();
    }

    public void RestartButton()
    {
        soundButton.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        soundButton.Play();
        SceneManager.LoadScene(0);
    }

    public void ShopButton(bool isActive)
    {
        Shop.instance.ShopButton(isActive);
    }

    public void ResetButton()
    {
        StartCoroutine(IEResetButton());
    }
}
