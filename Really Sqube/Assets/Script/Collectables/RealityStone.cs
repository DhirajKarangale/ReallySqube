using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RealityStone : MonoBehaviour
{
    public static RealityStone instance;

    [SerializeField] Text txtStone;
    [SerializeField] Text txtReality;
    [SerializeField] SpriteRenderer bg;
    [SerializeField] GameObject downButton;
    [SerializeField] Button checkButton;
    [SerializeField] Button changeButton;
    [SerializeField] AudioSource soundReality;
    [HideInInspector] public int stones;
    private Vector2 originalTxtScale;

    private void Awake()
    {
        instance = this;
        originalTxtScale = txtStone.transform.localScale;
        StartCoroutine(IEUpdateStoneTxt());
    }

    public void UpdateStone(int stone)
    {
        stones += stone;
        StartCoroutine(IEUpdateStoneTxt());
    }

    IEnumerator IEUpdateStoneTxt()
    {
        txtStone.transform.localScale = originalTxtScale * 0.2f;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 7);
        txtStone.transform.localScale = originalTxtScale;

        txtStone.text = stones.ToString();

        if (stones >= 2)
        {
            changeButton.gameObject.SetActive(true);
            checkButton.gameObject.SetActive(true);
            txtStone.gameObject.SetActive(true);
        }
        else if (stones == 1)
        {
            changeButton.gameObject.SetActive(false);
            checkButton.gameObject.SetActive(true);
            txtStone.gameObject.SetActive(true);
        }
        else
        {
            changeButton.gameObject.SetActive(false);
            checkButton.gameObject.SetActive(false);
            txtStone.gameObject.SetActive(false);
        }
    }

    private IEnumerator IECheckReality()
    {
        UpdateStone(-1);
        changeButton.interactable = false;
        checkButton.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        // bg.color = Color.Lerp(Color.white, Color.red, Time.deltaTime * smooth);
        bg.color = Color.red;
        txtReality.text = "Reality";
        txtReality.color = Color.yellow;
        txtReality.gameObject.SetActive(true);

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
        txtReality.gameObject.SetActive(false);
        bg.color = Color.white;

        foreach (GameObject fakeObj in fakeObjs)
        {
            Color temp = fakeObj.GetComponent<SpriteRenderer>().color;
            temp.a = 0.8f;
            fakeObj.GetComponent<SpriteRenderer>().color = temp;
        }
    }

    private IEnumerator IEChangeReality()
    {
        UpdateStone(-2);
        changeButton.interactable = false;
        checkButton.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        bg.color = Color.yellow;
        txtReality.text = "Reality Changed";
        txtReality.color = Color.red;
        txtReality.gameObject.SetActive(true);

        downButton.SetActive(true);
        if (PlayerHealth.instance)
        {
            PlayerHealth.instance.GetComponent<BoxCollider2D>().isTrigger = true;
            PlayerHealth.instance.GetComponent<Rigidbody2D>().gravityScale = 0;

            Color temp = PlayerHealth.instance.GetComponent<SpriteRenderer>().color;
            temp.a = 0.2f;
            PlayerHealth.instance.GetComponent<SpriteRenderer>().color = temp;
        }

        yield return new WaitForSeconds(25);

        soundReality.Stop();

        changeButton.interactable = true;
        checkButton.interactable = true;
        txtReality.gameObject.SetActive(false);
        bg.color = Color.white;

        downButton.SetActive(false);

        if (PlayerHealth.instance)
        {
            PlayerHealth.instance.GetComponent<BoxCollider2D>().isTrigger = false;
            PlayerHealth.instance.GetComponent<Rigidbody2D>().gravityScale = 8;

            Color temp = PlayerHealth.instance.GetComponent<SpriteRenderer>().color;
            temp.a = 1;
            PlayerHealth.instance.GetComponent<SpriteRenderer>().color = temp;
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
