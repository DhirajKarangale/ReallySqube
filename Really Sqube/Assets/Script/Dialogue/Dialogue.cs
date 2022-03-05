using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] bool isPlayerStop;
    [SerializeField] string[] dialogues;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.instance.StartDialogue(dialogues, isPlayerStop);
            Destroy(gameObject);
        }
    }
}
