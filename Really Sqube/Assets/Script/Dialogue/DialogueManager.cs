using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] PlayerMove player;
    [SerializeField] GameObject controlPanel;
    [SerializeField] Animator animator;
    [SerializeField] UnityEngine.UI.Text dialogueText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip writeSound;
    [SerializeField] AudioClip soundButton;
    private Queue<string> sentencesQue;
    private string sentence;

    private void Awake()
    {
        instance = this;

        sentencesQue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences, bool isPlayerStop)
    {
        if (isPlayerStop)
        {
            player.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            player.moveInputVal = 0;
            controlPanel.SetActive(false);
        }

        audioSource.PlayOneShot(soundButton);
        animator.Play("DialogueOpen");
        sentencesQue.Clear();
        foreach (string sentence in sentences)
        {
            sentencesQue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(GameManager.instance.isGameOver)
        {
            sentencesQue.Clear();
        }
        if (sentencesQue.Count == 0)
        {
            EndDialogue();
            return;
        }

        sentence = sentencesQue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(writeSound);
            dialogueText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        animator.Play("DialogueClose");
        PlayerHealth.instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        PlayerHealth.instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        controlPanel.SetActive(true);
    }
}