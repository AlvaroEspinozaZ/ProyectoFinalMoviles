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
    public bool isOver = false;
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
        if (King != null){
            if (King.isDeath && !isOver)
            {
                GameOver.CallEventGeneral();
                isOver = true;
            }
        }
    
        //if(listaEnemysInScene.Count == 12)
        //{
        //    if (orderMove)
        //    {
        //        Debug.Log("Moviendose ");
        //        for (int i = 0; i < listaEnemysInScene.Count; i++)
        //        {

        //            Debug.Log("Moviendose a..." + PosMovementArmyEnemy[0].position);
        //            Debug.Log("isCurrentMove" + listaEnemysInScene[i].VisionSensor.isCurrentMove);
        //            listaEnemysInScene[i].VisionSensor.isCurrentMove = true;
        //            Debug.Log("isCurrentMove" + listaEnemysInScene[i].VisionSensor.isCurrentMove);
                    
        //        }
        //        //listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id-1);
        //        orderMove = false;
        //    }
        //}
    
    }
    IEnumerator SpamEnemyCombo(int i)
    {
        if (!isPause)
        {
            int posId = UnityEngine.Random.Range(0, PosSpamArmyEnemy.Length);
            //Debug.Log("posId: " + posId);
            listaSpamArmy[i].Instatiate(PosSpamArmyEnemy[posId].position);           
            //listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id - 1);
            //listaEnemysInScene = listaSpamArmy[i].listaSpamArmy;
            cantArmy++;          
        }

        yield return new WaitForSecondsRealtime(1.5f);
        if (!isPause)
        {
            AgregarLista(listaSpamArmy[i]);
            listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id - 1);
        }
        //for(int id = 0; id< listaSpamArmy[i].listArmy.Count; id++)
        //{
        //    Debug.Log("Comprobandooooo"+listaSpamArmy[i].listArmy[id].VisionSensor.isCurrentMove);
        //    listaSpamArmy[i].listArmy[id].VisionSensor.isCurrentMove = true;
        //    Debug.Log("Comprobandooooo2222222222" + listaSpamArmy[i].listArmy[id].VisionSensor.isCurrentMove);
        //}
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
            army.listArmy[i].MyHealth.clearList += CLearSoldier;

        }
    }
    private void CLearSoldier(PrefabMovement soldierDied)
    {
        listaEnemysInScene.Remove(soldierDied);
    }
    void ChooseCombo(int numberCombo)
    {
        if (!isPause)
        {
            switch (numberCombo)
            {
                case 0:
                    if (isWorking)
                    {
                        if (canCreated)
                        {
                            StartCoroutine(SpamEnemyCombo(numberCombo));
                        }
                    }
                    break;
                case 1:
                    if (isWorking)
                    {
                        if (canCreated)
                        {
                            StartCoroutine(SpamEnemyCombo(numberCombo));
                        }
                    }
                    break;
                case 2:
                    if (canCreated)
                    {
                        StartCoroutine(SpamEnemyCombo(numberCombo));
                    }
                    break;
                case 3:
                    if (canCreated)
                    {
                        StartCoroutine(SpamEnemyCombo(numberCombo));
                    }
                    break;
                case 4:
                    if (canCreated)
                    {
                        StartCoroutine(SpamEnemyCombo(numberCombo));
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
        isOver = true;
        SceneGlobalManager.Instance.LoadScene(End);
    }
}
