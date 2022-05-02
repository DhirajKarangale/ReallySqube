using UnityEngine;
using System.Collections;

public class Pop : MonoBehaviour
{
    [SerializeField] float mxAmount = 1.2f;
    [SerializeField] float minAmount = 0.8f;
    [SerializeField] float time = 10;
    public bool isPopAllow;
    private Vector3 originalScale;

    private void OnEnable()
    {
        // originalScale = transform.localScale;
        originalScale = Vector3.one;
        StopAllCoroutines();
        StartCoroutine(IEPop());
    }

    private IEnumerator IEPop()
    {
        if (isPopAllow)
        {
            transform.localScale = originalScale * mxAmount;
            yield return new WaitForSecondsRealtime(0.02f * time);
            transform.localScale = originalScale * minAmount;
        }
        yield return new WaitForSecondsRealtime(0.02f * time);

        StartCoroutine(IEPop());
    }
}
