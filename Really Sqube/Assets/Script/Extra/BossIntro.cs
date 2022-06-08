using UnityEngine;

public class BossIntro : MonoBehaviour
{
    [SerializeField] GameObject eyeAnim;
    [SerializeField] GameObject bossDialogue;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("BossIntro", 0) == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            eyeAnim.SetActive(true);
            bossDialogue.SetActive(true);
            PlayerPrefs.SetInt("BossIntro", 1);
        }
    }
}
