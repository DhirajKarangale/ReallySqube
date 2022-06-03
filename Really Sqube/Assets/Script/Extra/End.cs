using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] string[] dialogues;

    private void OnEnable()
    {
        StartDialogue();
    }

    public void MenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void StartDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogues, 0);
    }
}
