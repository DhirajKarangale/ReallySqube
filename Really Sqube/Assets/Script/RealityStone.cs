using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RealityStone : MonoBehaviour
{
    public static RealityStone instance;

    [SerializeField] Text stoneTxt;
    [SerializeField] SpriteRenderer bg;
    [SerializeField] Text realityTxt;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCap;
    [SerializeField] GameObject downButton;
    [SerializeField] Button checkButton;
    [SerializeField] Button changeButton;
    [SerializeField] AudioSource soundReality;
    private int stones;
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

        if (stones >= 1) checkButton.gameObject.SetActive(true);
        else checkButton.gameObject.SetActive(false);

        if (stones >= 2) changeButton.gameObject.SetActive(true);
        else changeButton.gameObject.SetActive(false);

        if (stones > 0) stoneTxt.gameObject.SetActive(true);
        else stoneTxt.gameObject.SetActive(false);
    }

    private IEnumerator IECheckReality()
    {
        ChangeStone(-1);
        changeButton.interactable = false;
        checkButton.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        // bg.color = Color.Lerp(Color.white, Color.red, Time.deltaTime * smooth);
        bg.color = Color.red;
        realityTxt.text = "Reality";
        realityTxt.color = Color.yellow;
        realityTxt.gameObject.SetActive(true);

        GameObject[] fakeObjs = GameObject.FindGameObjectsWithTag("Fake");
        foreach (GameObject fakeObj in fakeObjs)
        {
            Color temp = fakeObj.GetComponent<SpriteRenderer>().color;
            temp.a = 0.2f;
            fakeObj.GetComponent<SpriteRenderer>().color = temp;
        }

        yield return new WaitForSeconds(20);

        soundReality.Stop();

        changeButton.interactable = true;
        checkButton.interactable = true;
        realityTxt.gameObject.SetActive(false);
        bg.color = Color.white;

        foreach (GameObject fakeObj in fakeObjs)
        {
            Color temp = fakeObj.GetComponent<SpriteRenderer>().color;
            temp.a = 1;
            fakeObj.GetComponent<SpriteRenderer>().color = temp;
        }
    }

    private IEnumerator IEChangeReality()
    {
        ChangeStone(-2);
        changeButton.interactable = false;
        checkButton.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        bg.color = Color.yellow;
        realityTxt.text = "Reality Changed";
        realityTxt.color = Color.red;
        realityTxt.gameObject.SetActive(true);
        playerCap.SetActive(true);

        downButton.SetActive(true);
        player.GetComponent<BoxCollider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;

        SpriteRenderer[] playerVisuals = player.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer playerVisual in playerVisuals)
        {
            Color temp = playerVisual.color;
            temp.a = 0.2f;
            playerVisual.color = temp;
        }

        yield return new WaitForSeconds(25);

        soundReality.Stop();

        changeButton.interactable = true;
        checkButton.interactable = true;
        realityTxt.gameObject.SetActive(false);
        bg.color = Color.white;
        playerCap.SetActive(false);

        downButton.SetActive(false);
        player.GetComponent<BoxCollider2D>().isTrigger = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 8;

        foreach (SpriteRenderer playerVisual in playerVisuals)
        {
            Color temp = playerVisual.color;
            temp.a = 1;
            playerVisual.color = temp;
        }
    }

    public void CheckRealityButton()
    {
        StartCoroutine(IECheckReality());
    }

    public void ChangeRealityButton()
    {
        StartCoroutine(IEChangeReality());
    }
}
