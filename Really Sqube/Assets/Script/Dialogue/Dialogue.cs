using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public float playerDir;
    [SerializeField] string[] dialogues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.instance.StartDialogue(dialogues, playerDir);
            Destroy(gameObject);
        }
    }
}
