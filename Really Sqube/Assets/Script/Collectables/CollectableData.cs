using UnityEngine;

public class CollectableData : MonoBehaviour
{
    public static CollectableData instance;

    public int coin;
    public int realityStone;
    public int timeStone;

    private void Awake()
    {
        instance = this;
        GetData();
    }
    
    public void GetData()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        realityStone = PlayerPrefs.GetInt("RealityStone", 0);
        timeStone = PlayerPrefs.GetInt("TimeStone", 0);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("RealityStone", realityStone);
        PlayerPrefs.SetInt("TimeStone", timeStone);
    }
}
