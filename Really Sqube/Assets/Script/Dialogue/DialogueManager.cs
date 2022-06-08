using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] Animator animator;
    [SerializeField] UnityEngine.UI.Text dialogueText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip writeSound;
    [SerializeField] AudioClip soundButton;
    private Queue<string> sentencesQue;
    private string sentence;
    public bool isPlayerStop;

    private void Awake()
    {
        instance = this;
        isPlayerStop = false;
        sentencesQue = new Queue<string>();
        sentencesQue.Clear();
    }

    private void OnEnable()
    {
        if (sentencesQue.Count != 0)
        {
            animator.Play("IdelOpen");
            DisplayNextSentence();
        }
    }

    public void StartDialogue(string[] sentences, float playerDir)
    {
        if (playerDir != 0)
        {
            isPlayerStop = true;
            GameManager.instance.StopPlayer(playerDir);
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
        if (GameManager.instance && GameManager.instance.isGameOver)
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
        yield return new WaitForSecondsRealtime(0.8f);
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        animator.Play("DialogueClose");
        if (GameManager.instance) GameManager.instance.StartPlayer();
        isPlayerStop = false;
    }
}