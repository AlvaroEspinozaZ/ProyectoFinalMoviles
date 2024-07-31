using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public GameObject Dialogue;
    public TMP_Text dialogueText;
    public UnityEvent OnEndEvent;
    public string[] dialogues;
    private int dialogueIndex = 0;
    private bool isTyping = false;
    private bool skipToFull = false;

    private void Start()
    {
        ShowDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                skipToFull = true;
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeSentence(dialogues[dialogueIndex]));
        }
        else
        {
            OnEndEvent?.Invoke();
            Dialogue.SetActive(false);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;
        skipToFull = false;

        foreach (char letter in sentence.ToCharArray())
        {
            if (skipToFull)
            {
                dialogueText.text = sentence;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    void NextDialogue()
    {
        dialogueIndex++;
        ShowDialogue();
    }
}
