using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeStone : MonoBehaviour
{
    public static TimeStone instance;
    [SerializeField] Text stoneTxt;
    [SerializeField] Button slowsTimeButton;
    [SerializeField] Button reverseTimeButton;
    [HideInInspector] public int stones;
    private Vector2 originalTxtScale;

    private void Awake()
    {
        instance = this;
        originalTxtScale = stoneTxt.transform.localScale;
        StartCoroutine(IEUpdateStoneTxt());
    }

    public void ChangeStone(int stone)
    {
        stones += stone;
        StartCoroutine(IEUpdateStoneTxt());
    }

    IEnumerator IEUpdateStoneTxt()
    {
        stoneTxt.transform.localScale = originalTxtScale * 0.5f;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        stoneTxt.transform.localScale = originalTxtScale;

        stoneTxt.text = stones.ToString();

        if (stones >= 4)
        {
            reverseTimeButton.gameObject.SetActive(true);
            slowsTimeButton.gameObject.SetActive(true);
            stoneTxt.gameObject.SetActive(true);
        }
        else if (stones == 2)
        {
            reverseTimeButton.gameObject.SetActive(false);
            slowsTimeButton.gameObject.SetActive(true);
            stoneTxt.gameObject.SetActive(true);
        }
        else
        {
            reverseTimeButton.gameObject.SetActive(false);
            slowsTimeButton.gameObject.SetActive(false);
            stoneTxt.gameObject.SetActive(false);
        }
    }

}
