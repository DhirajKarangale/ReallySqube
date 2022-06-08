using UnityEngine;

public class TimeStone : MonoBehaviour
{
    public static TimeStone instance;
    [SerializeField] StopTime stopTime;

    private void Awake()
    {
        instance = this;
    }

    public void ReverseButton(bool isUseStone)
    {
        if (stopTime.isTimeStopped) stopTime.Continue();
        if (GameManager.instance.isGameOver) GameManager.instance.RespwanPlayer();
        PlayerHealth.instance.reverse.StartRewind();
        if (isUseStone) UIManager.instance.UpdateTimeStone(-6);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
    }

    public void StopTimeButton()
    {
        stopTime.StopTimeButton();
        UIManager.instance.UpdateTimeStone(-4);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
    }
}
