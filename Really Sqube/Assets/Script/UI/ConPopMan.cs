using UnityEngine;
using System.Collections;

public class ConPopMan : MonoBehaviour
{
    [SerializeField] ContinuePop popPause;
    [SerializeField] ContinuePop popShop;
    [SerializeField] ContinuePop popRealityCheck;
    [SerializeField] ContinuePop popRealityChange;
    [SerializeField] ContinuePop popStopTime;
    [SerializeField] ContinuePop popReverseTime;
    private CollectableData collectableData;

    private void Start()
    {
        collectableData = CollectableData.instance;
        StartCoroutine(IECheckPop());
    }

    private IEnumerator IECheckPop()
    {
        popPause.isPopAllow = (collectableData.realityStone >= 1 || collectableData.timeStone >= 3);
        popShop.isPopAllow = collectableData.coin >= 100;
        popRealityCheck.isPopAllow = collectableData.realityStone >= 1;
        popRealityChange.isPopAllow = collectableData.realityStone >= 2;
        popStopTime.isPopAllow = collectableData.timeStone >= 3;
        popReverseTime.isPopAllow = collectableData.timeStone >= 5;
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(IECheckPop());
    }
}
