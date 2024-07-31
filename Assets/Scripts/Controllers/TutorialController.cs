using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialDialogues;
    private int indexDialogues = 0;
    [SerializeField] private bool IsNormalTime = false;
    public void NextIndexDialogue(int Seconds)
    {
        indexDialogues++;
        StartCoroutine(TimeSkiped(Seconds));
        if(indexDialogues < tutorialDialogues.Length)
            tutorialDialogues[indexDialogues].SetActive(true);
    }
    IEnumerator TimeSkiped(int time)
    {
        yield return new WaitForSeconds(time);
    }
    //Time.timeScale no funciona correctamente, no tengo idea por que
    public void ToggleTimeScale()
    {
        if (IsNormalTime == false)
            Time.timeScale = 0.0f;
        else 
            Time.timeScale = 1.0f;
    }
}
