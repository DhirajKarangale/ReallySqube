using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] Text txtRealityStone;
    [SerializeField] Text txtTimeStone;
    [SerializeField] GameObject pauseObj;
    [SerializeField] GameObject buttonManagerObj;
    [SerializeField] GameObject dialogueManagerObj;

    public void PauseButton()
    {
        txtRealityStone.text = RealityStone.instance.stones.ToString();
        txtTimeStone.text = TimeStone.instance.stones.ToString();
        Time.timeScale = 0;
        pauseObj.SetActive(true);
        buttonManagerObj.SetActive(false);
        dialogueManagerObj.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseObj.SetActive(false);
        buttonManagerObj.SetActive(true);
        dialogueManagerObj.SetActive(true);
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



}
