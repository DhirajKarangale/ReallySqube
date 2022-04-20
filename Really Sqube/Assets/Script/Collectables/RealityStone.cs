using System.Collections;
using UnityEngine;

public class RealityStone : MonoBehaviour
{
    public static RealityStone instance;

    [SerializeField] SpriteRenderer bg;
    [SerializeField] AudioSource soundReality;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator IECheckReality()
    {
        UIManager.instance.UpdateRealityStone(-1);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
        UIManager.instance.buttonChangeReality.interactable = false;
        UIManager.instance.buttonCheckReality.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        // bg.color = Color.Lerp(Color.white, Color.red, Time.deltaTime * smooth);
        bg.color = Color.red;
        UIManager.instance.txtRealityStatus.text = "Reality";
        UIManager.instance.txtRealityStatus.color = Color.yellow;
        UIManager.instance.txtRealityStatus.gameObject.SetActive(true);

        GameObject[] fakeObjs = GameObject.FindGameObjectsWithTag("Fake");
        foreach (GameObject fakeObj in fakeObjs)
        {
            Color temp = fakeObj.GetComponent<SpriteRenderer>().color;
            temp.a = 0.2f;
            fakeObj.GetComponent<SpriteRenderer>().color = temp;
        }

        yield return new WaitForSeconds(20);

        soundReality.Stop();

        UIManager.instance.buttonChangeReality.interactable = true;
        UIManager.instance.buttonCheckReality.interactable = true;
        UIManager.instance.txtRealityStatus.gameObject.SetActive(false);
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
        UIManager.instance.UpdateRealityStone(-2);
        PlayerPrefs.SetInt("TimeStone", CollectableData.instance.timeStone);
        UIManager.instance.buttonChangeReality.interactable = false;
        UIManager.instance.buttonCheckReality.interactable = false;

        if (!soundReality.isPlaying) soundReality.Play();

        bg.color = Color.yellow;
        UIManager.instance.txtRealityStatus.text = "Reality Changed";
        UIManager.instance.txtRealityStatus.color = Color.red;
        UIManager.instance.txtRealityStatus.gameObject.SetActive(true);

        UIManager.instance.buttonDown.gameObject.SetActive(true);
        if (PlayerHealth.instance)
        {
            PlayerHealth.instance.GetComponent<BoxCollider2D>().isTrigger = true;
            PlayerHealth.instance.GetComponent<Rigidbody2D>().gravityScale = 0;

            Color temp = PlayerHealth.instance.gui.color;
            temp.a = 0.2f;
            PlayerHealth.instance.gui.color = temp;
        }

        yield return new WaitForSeconds(25);

        soundReality.Stop();

        UIManager.instance.buttonChangeReality.interactable = true;
        UIManager.instance.buttonCheckReality.interactable = true;
        UIManager.instance.txtRealityStatus.gameObject.SetActive(false);
        bg.color = Color.white;

        UIManager.instance.buttonDown.gameObject.SetActive(false);

        if (PlayerHealth.instance)
        {
            PlayerHealth.instance.GetComponent<BoxCollider2D>().isTrigger = false;
            PlayerHealth.instance.GetComponent<Rigidbody2D>().gravityScale = 8;

            Color temp = PlayerHealth.instance.gui.color;
            temp.a = 1;
            PlayerHealth.instance.gui.color = temp;
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
