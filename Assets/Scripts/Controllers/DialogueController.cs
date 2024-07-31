using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction touchAction;
    public GameObject Dialogue;
    public TMP_Text dialogueText;
    public UnityEvent OnEndEvent;
    public string[] dialogues;
    private int dialogueIndex = 0;
    private bool isTyping = false;
    private bool skipToFull = false;
    private void Awake()
    {
        touchAction = playerInput.actions["Touch"];
        // Suscríbete a los eventos
        touchAction.performed += OnTouchPerformed;
        touchAction.canceled += OnTouchCanceled;
        touchAction.Enable();
    }
    private void Start()
    {
        ShowDialogue();
    }

    public void OnTouchPerformed(InputAction.CallbackContext context)
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
    void OnTouchCanceled(InputAction.CallbackContext context)
    {
        if (dialogueIndex > dialogues.Length)
        {
            OnEndEvent?.Invoke();
            Dialogue.SetActive(false);
        }
    }
    private void OnDisable()
    {
        touchAction.performed -= OnTouchPerformed;
        touchAction.canceled -= OnTouchCanceled;
    }
    void ShowDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeSentence(dialogues[dialogueIndex]));
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
