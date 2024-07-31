using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenesManager;
public class GameController : MonoBehaviour
{
    public float time = 2;
    [SerializeField] private Health KingRed;
    [SerializeField] private Health KingBlue;
    public bool isOver = false;
    public HanldeEvent GameOver;
    [SerializeField] private SceneConfiguration End;
    void Update()
    {
        if (KingRed != null)
        {
            if (KingRed.isDeath && !isOver)
            {
                GameOver.CallEventFloat(time);
                isOver = true;
            }
        }
        else if (KingBlue != null)
        {
            if (KingBlue.isDeath && !isOver)
            {
                GameOver.CallEventFloat(time);
                isOver = true;
            }
        }
    }
    private void OnEnable()
    {
        GameOver.ActionFloat += GameOverS;
    }
    private void OnDisable()
    {
        GameOver.ActionFloat -= GameOverS;
    }
    void GameOverS(float time)
    {
        isOver = true;
        StartCoroutine(GameOverTransition(time));
    }
    IEnumerator GameOverTransition(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneGlobalManager.Instance.LoadScene(End);
    }
}
