using System.Collections;
using UnityEngine;

public class RealityStone : MonoBehaviour
{
    public static RealityStone instance;

    [SerializeField] AudioSource soundReality;
    private SpriteRenderer bg;
    private UIManager uiManager;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        instance = this;
        uiManager = UIManager.instance;
        playerHealth = PlayerHealth.instance;
        bg = playerHealth.reverse.bg;
    }

    private IEnumerator IECheckReality()
    {
        uiManager.UpdateRealityStone(-1);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
        uiManager.buttonChangeReality.interactable = false;
        uiManager.buttonCheckReality.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        // bg.color = Color.Lerp(Color.white, Color.red, Time.deltaTime * smooth);
        bg.color = Color.red;
        uiManager.txtRealityStatus.text = "Reality";
        uiManager.txtRealityStatus.color = Color.yellow;
        uiManager.txtRealityStatus.gameObject.SetActive(true);

        GameObject[] fakeObjs = GameObject.FindGameObjectsWithTag("Fake");
        foreach (GameObject fakeObj in fakeObjs)
        {
            Color temp = fakeObj.GetComponent<SpriteRenderer>().color;
            temp.a = 0.2f;
            fakeObj.GetComponent<SpriteRenderer>().color = temp;
        }

        yield return new WaitForSeconds(20);

        soundReality.Stop();

        uiManager.buttonChangeReality.interactable = true;
        uiManager.buttonCheckReality.interactable = true;
        uiManager.txtRealityStatus.gameObject.SetActive(false);
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
        uiManager.UpdateRealityStone(-2);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
        uiManager.buttonChangeReality.interactable = false;
        uiManager.buttonCheckReality.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        bg.color = Color.yellow;
        uiManager.txtRealityStatus.text = "Reality Changed";
        uiManager.txtRealityStatus.color = Color.red;
        uiManager.txtRealityStatus.gameObject.SetActive(true);

        uiManager.buttonDown.gameObject.SetActive(true);
        if (playerHealth)
        {
            playerHealth.GetComponent<BoxCollider2D>().isTrigger = true;
            playerHealth.GetComponent<Rigidbody2D>().gravityScale = 0;

            Color temp = playerHealth.gui.color;
            temp.a = 0.2f;
            playerHealth.gui.color = temp;
        }

        yield return new WaitForSeconds(25);

        soundReality.Stop();

        uiManager.buttonChangeReality.interactable = true;
        uiManager.buttonCheckReality.interactable = true;
        uiManager.txtRealityStatus.gameObject.SetActive(false);
        bg.color = Color.white;

        uiManager.buttonDown.gameObject.SetActive(false);

        if (playerHealth)
        {
            playerHealth.GetComponent<BoxCollider2D>().isTrigger = false;
            playerHealth.GetComponent<Rigidbody2D>().gravityScale = 8;

            Color temp = playerHealth.gui.color;
            temp.a = 1;
            playerHealth.gui.color = temp;
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
