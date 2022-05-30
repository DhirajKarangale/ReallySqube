using UnityEngine;

public class EyeOpen : MonoBehaviour
{
    [SerializeField] AudioSource bgMusic;
    [SerializeField] GameObject objDesable;

    private void Start()
    {
        bgMusic.Stop();
        objDesable.SetActive(false);
        Invoke("DesableEye", 9.5f);
    }

    private void DesableEye()
    {
        bgMusic.Play();
        objDesable.SetActive(true);
        Destroy(gameObject);
    }
}
