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
        CollectableData.instance.Save();
    }

    public void StopTimeButton()
    {
        stopTime.StopTimeButton();
        UIManager.instance.UpdateTimeStone(-4);
        CollectableData.instance.Save();
    }
}
