using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEngine.UI.Text dialogueText;
    [SerializeField] GameObject controlPanel;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip writeSound;
    [SerializeField] AudioClip soundButton;
    private Queue<string> sentencesQue;
    private string sentence;

    private void Start()
    {
        sentencesQue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences, bool isPlayerStop)
    {
        if (isPlayerStop) controlPanel.SetActive(false);

        audioSource.PlayOneShot(soundButton);
        // soundButton.Play();
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
        controlPanel.SetActive(true);
    }
}