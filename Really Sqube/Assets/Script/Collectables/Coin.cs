using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin instance;
    [SerializeField] Text txtCoin;
    [HideInInspector] public int coins;
    private Vector2 originalTxtScale;

    private void Awake()
    {
        instance = this;
        originalTxtScale = txtCoin.transform.localScale;
        StartCoroutine(IEUpdateStoneTxt());
    }

    public void UpdateCoin(int coin)
    {
        coins += coin;
        StartCoroutine(IEUpdateStoneTxt());
    }

    IEnumerator IEUpdateStoneTxt()
    {
        txtCoin.transform.localScale = originalTxtScale * 0.2f;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 7);
        txtCoin.transform.localScale = originalTxtScale;

        txtCoin.text = coins.ToString();

        if (coins > 0)
        {
            txtCoin.gameObject.SetActive(true);
        }
        else
        {
            txtCoin.gameObject.SetActive(false);
        }
    }
}
