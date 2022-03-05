using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale = Vector3.one;
    private Vector3 changeScale = Vector3.one * 1.15f;

    private void Start()
    {
        Invoke("GetScale", 2);
    }

    public void OnPointerDown(PointerEventData data)
    {
        transform.localScale = changeScale;
    }
    public void OnPointerUp(PointerEventData data)
    {
        transform.localScale = originalScale;
    }

    private void GetScale()
    {
        originalScale = transform.localScale;
        changeScale = 1.3f * originalScale;
        transform.localScale = originalScale;
    }
}