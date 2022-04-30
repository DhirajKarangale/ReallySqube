using UnityEngine;

public class TimeStone : MonoBehaviour
{
    public static TimeStone instance;
    [SerializeField] StopTime stopTime;

    private void Awake()
    {
        instance = this;
    }

    public void ReverseButton()
    {
        if (GameManager.instance.isGameOver) GameManager.instance.RespwanPlayer();
        PlayerHealth.instance.reverse.StartRewind();
        UIManager.instance.UpdateTimeStone(-5);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
    }

    public void StopTimeButton()
    {
        stopTime.StopTimeButton();
        UIManager.instance.UpdateTimeStone(-3);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
    }
}
