using UnityEngine;
using UnityEngine.UI;

public class ShopDesc : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject txtDescription;
    private Color colorDecription;
    private Color colorImage;
    private bool isDescription;

    private void Start()
    {
        isDescription = false;

        colorDecription = Color.black;
        colorDecription.a = 0.4f;

        colorImage = Color.white;
        colorImage.a = 1f;

        HideDescription();
    }

    public void DescriptionButton()
    {
        isDescription = !isDescription;

        if (isDescription) ShowDescription();
        else HideDescription();
    }

    private void ShowDescription()
    {
        image.color = colorDecription;
        txtDescription.SetActive(true);
    }

    private void HideDescription()
    {
        txtDescription.SetActive(false);
        image.color = colorImage;
    }
}
