using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PurchaseableEff : MonoBehaviour
{
    [SerializeField] Pop popPause;
    [SerializeField] Pop popRealityCheck;
    [SerializeField] Pop popRealityChange;
    [SerializeField] Pop popStopTime;
    [SerializeField] Pop popReverseTime;

    [Header("Shop")]
    [SerializeField] Image shopKnob;
    [SerializeField] Image pauseKnob;
    [SerializeField] Color colorRealityStone;
    [SerializeField] Color colorTimeStone;
    [SerializeField] Color colorGold;

    private CollectableData collectableData;

    private void Start()
    {
        collectableData = CollectableData.instance;
        StartCoroutine(IECheckPop());
    }

    private IEnumerator IECheckPop()
    {
        popPause.isPopAllow = (collectableData.realityStone >= 1 || collectableData.timeStone >= 3);
        popRealityCheck.isPopAllow = collectableData.realityStone >= 1;
        popRealityChange.isPopAllow = collectableData.realityStone >= 2;
        popStopTime.isPopAllow = collectableData.timeStone >= 3;
        popReverseTime.isPopAllow = collectableData.timeStone >= 5;

        ShopPurchasableEffect();

        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(IECheckPop());
    }

    private void ShopPurchasableEffect()
    {
        if(collectableData.coin >= 700)
        {
            shopKnob.color = colorGold;
            pauseKnob.color = colorGold;
            shopKnob.gameObject.SetActive(true);
            pauseKnob.gameObject.SetActive(true);
        }
        else if(collectableData.coin >= 300)
        {
            shopKnob.color = colorTimeStone;
            pauseKnob.color = colorTimeStone;
            shopKnob.gameObject.SetActive(true);
            pauseKnob.gameObject.SetActive(true);
        }
        else if(collectableData.coin >= 100)
        {
            shopKnob.color = colorRealityStone;
            pauseKnob.color = colorRealityStone;
            shopKnob.gameObject.SetActive(true);
            pauseKnob.gameObject.SetActive(true);
        }
        else
        {
            shopKnob.gameObject.SetActive(false);
            pauseKnob.gameObject.SetActive(false);
        }
    }
}
