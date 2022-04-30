using UnityEngine;
using System.Collections;

public class ContinuePop : MonoBehaviour
{
    [SerializeField] float mxAmount = 1.2f;
    [SerializeField] float minAmount = 0.8f;
    [SerializeField] float time = 10;
    public bool isPopAllow;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        // StartCoroutine(IEPop());
    }

    // private void Update()
    // {
    //     if(isPopAllow)
    //     {
    //         StartCoroutine_Auto(IEPop());
    //     }
    // }

    private IEnumerator IEPop()
    {
        // Debug.Log(transform.name + " pop trying");
        if (isPopAllow)
        {
            Debug.Log(transform.name + " pop");
            transform.localScale = originalScale * mxAmount;
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * time);
            transform.localScale = originalScale * minAmount;
        }
        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * time);
        Debug.Log(transform.name + " return");

        StartCoroutine(IEPop());
    }
}
