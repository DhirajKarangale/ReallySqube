using UnityEngine;

public class TimeStone : MonoBehaviour
{
    public static TimeStone instance;
    [HideInInspector] public bool isTimeStoped;

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
}
