using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScenesManager;
using System;
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private SpamArmy[] listaSpamArmy;
    [SerializeField] private List<PrefabMovement> listaEnemysInScene;
    [SerializeField] private Health King;
    [SerializeField] private Transform[] PosSpamArmyEnemy;
    [SerializeField] private Transform[] PosMovementArmyEnemy;
    [SerializeField] private int currentcomboArmy;

    [SerializeField] private SceneConfiguration End;
    public int cantArmy;
    public int comboArmy= 4;
    public bool isPause = false;
    public bool isWorking = false;
    public bool canCreated = true;
    private bool isOver = false;
    private bool orderMove = false;
    public HanldeEvent GameOver;
    private void Start()
    {
        currentcomboArmy = UnityEngine.Random.Range(0, comboArmy);
        ChooseCombo(currentcomboArmy);

    }
 
    private void Update()
    {
        if (!isPause)
        {
            if (!isWorking)
            {
                if (listaEnemysInScene.Count <12)
                {
                    if (canCreated)
                    {
                        currentcomboArmy = UnityEngine.Random.Range(0, comboArmy);
                        isWorking = true;
                        ChooseCombo(currentcomboArmy);
                        canCreated = false;
                    }
                }
            }          
        }
        if (King.isDeath&& isOver)
        {
            GameOver.CallEventGeneral();
            isOver = false;
        }
        if(listaEnemysInScene.Count == 12)
        {
            Debug.Log("Completo ");
            if (orderMove)
            {
                Debug.Log("Moviendose ");
                for (int i = 0; i < listaEnemysInScene.Count; i++)
                {
                    listaEnemysInScene[i].VisionSensor.SelectDestination(PosMovementArmyEnemy[0].position);
                    Debug.Log("Moviendose a..."+ PosMovementArmyEnemy[0].position);
                }
                //listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id-1);
                orderMove = false;
            }
        }
    
    }
    IEnumerator SpamEnemyCombo(int i)
    {
        if (!isPause)
        {
            int posId = UnityEngine.Random.Range(0, PosSpamArmyEnemy.Length-1);
            listaSpamArmy[i].Instatiate(PosSpamArmyEnemy[posId].position);
            AgregarLista(listaSpamArmy[i]);
            
            //listaEnemysInScene = listaSpamArmy[i].listaSpamArmy;
            cantArmy++;          
        }
        yield return new WaitForSecondsRealtime(1);
        if (listaEnemysInScene.Count == 12)
            orderMove = true;
        isWorking = false;
        if (cantArmy < 3)
            {
                canCreated = true;
            }
    }
    void AgregarLista(SpamArmy army)
    {
        int i = 0;
        switch (army.listArmy.Count%4)
        {
            case 0:
                i = 0;
                break;
            case 1:
                i = 0;
                break;
            case 2:
                i = 4;
                break;
            case 3:
                i = 8;
                break;
        }
        for(i = 0; i < army.listArmy.Count; i++)
        {
            listaEnemysInScene.Add(army.listArmy[i]);
        }
    }
    void ChooseCombo(int numberCombo)
    {
        switch (numberCombo)
        {
            case 0:
                if (isWorking)
                {
                    if (canCreated)
                    {
                        StartCoroutine(SpamEnemyCombo(numberCombo));
                        Debug.Log("Entramos al 0");
                    }
                }
                break;
            case 1:
                if (isWorking)
                {
                    if (canCreated)
                    {
                        StartCoroutine(SpamEnemyCombo(numberCombo));
                        Debug.Log("Entramos al 1");
                    }
                }
                break;
            case 2:
                if (canCreated)
                {
                    StartCoroutine(SpamEnemyCombo(numberCombo));
                    Debug.Log("Entramos al 2");
                }
                break;
            case 3:
                if (canCreated)
                {
                    StartCoroutine(SpamEnemyCombo(numberCombo));
                    Debug.Log("Entramos al 3");
                }
                break;
            case 4:
                if (canCreated)
                {
                    StartCoroutine(SpamEnemyCombo(numberCombo));
                    Debug.Log("Entramos al 4");
                }
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
        }
    }
    public void StopGame()
    {
        if (isPause)
        {
            isPause = false;
        }
        else
        {
            isPause = true;
        }
    }
    public void StopGameBrain()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        GameOver.ActionGeneral += GameOverS;
    }
    private void OnDisable()
    {
        GameOver.ActionGeneral -= GameOverS;
    }
    void GameOverS()
    {
        SceneGlobalManager.Instance.LoadScene(End);
    }
}
