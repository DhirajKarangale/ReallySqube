using UnityEngine;

public class TimeStone : MonoBehaviour
{
    public static TimeStone instance;
    [HideInInspector] public bool isTimeStoped;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopTime();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueTime();
        }
    }

    public void ContinueTime()
    {
        isTimeStoped = false;

        StopTimeObj[] timeObjects = FindObjectsOfType<StopTimeObj>();  //Find Every object with the Timebody Component
        foreach (StopTimeObj timeObject in timeObjects)
        {
            timeObject.GetComponent<StopTimeObj>().ContinueTime(); //continue time in each of them
        }
    }

    public void StopTime()
    {
        isTimeStoped = true;
    }

    public void ReverseButton()
    {
        if (GameManager.instance.isGameOver) GameManager.instance.RespwanPlayer();
        PlayerHealth.instance.reverse.StartRewind();
        UIManager.instance.UpdateTimeStone(-5);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
    }
}
