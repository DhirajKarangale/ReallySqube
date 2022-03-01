using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] Transform player;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] bool isPlayerStop;
    [SerializeField] string[] dialogues;

    private void FixedUpdate()
    {
        if (player && dist >= Vector2.Distance(transform.position, player.position))
        {
            dialogueManager.StartDialogue(dialogues, isPlayerStop);
            this.enabled = false;
        }
    }
}
