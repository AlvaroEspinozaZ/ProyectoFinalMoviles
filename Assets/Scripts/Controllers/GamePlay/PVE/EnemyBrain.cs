using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private SpamArmy[] listaSpamArmy;
    [SerializeField] private List<PrefabMovement> listaEnemysInScene;
    [SerializeField] private List<Health> towerSpawm;
    [SerializeField] private Transform[] PosSpamArmyEnemy;
    [SerializeField] private Transform[] PosMovementArmyEnemy;
    [SerializeField] private int currentcomboArmy;
    public int cantArmy;
    private int comboArmy= 4;
    public bool isPause = true;
    public bool isWorking = false;
    public bool canCreated = true;
    private bool orderMove = false;
    public HanldeEvent GameOver;
    private void Start()
    {
        currentcomboArmy = UnityEngine.Random.Range(0, comboArmy);
        ChooseCombo(currentcomboArmy);
        GameOver.ActionFloat+= ItsOver;
        towerSpawm[0].clearListHealth += (health)=> { towerSpawm.Remove(health); };
        towerSpawm[1].clearListHealth += (health) => { towerSpawm.Remove(health); };
        StartCoroutine(PresentMap());
    }
    IEnumerator PresentMap()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        isPause = false;
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
        if (listaEnemysInScene.Count == 0|| listaEnemysInScene.Count == 4 || listaEnemysInScene.Count == 8)
        {
            canCreated = true;
        }
        if (!isPause)
        {
            if (listaEnemysInScene.Count == 12)
            {
                if (orderMove)
                {
                    Debug.Log("Moviendose ");
                    for (int i = 0; i < listaEnemysInScene.Count; i++)
                    {

                        Debug.Log("Moviendose a..." + PosMovementArmyEnemy[0].position);
                        Debug.Log("isCurrentMove" + listaEnemysInScene[i].VisionSensor.isCurrentMove);
                        listaEnemysInScene[i].VisionSensor.isCurrentMove = true;
                        Debug.Log("isCurrentMove" + listaEnemysInScene[i].VisionSensor.isCurrentMove);

                    }
                    //listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id-1);
                    orderMove = false;
                }
            }
        }
       

    }
    IEnumerator SpamEnemyCombo(int i)
    {
        if (!isPause)
        {
            int posId = UnityEngine.Random.Range(0, towerSpawm.Count);
            listaSpamArmy[i].Instatiate(PosSpamArmyEnemy[posId].position);
            cantArmy++;            
        }

        yield return new WaitForSecondsRealtime(1.5f);
        if (!isPause)
        {
            AgregarLista(listaSpamArmy[i]);
            listaSpamArmy[i].MoveArmy(PosMovementArmyEnemy[0].position, listaSpamArmy[i].id - 1);
        }
        if (listaEnemysInScene.Count == 12)
            orderMove = true;

        isWorking = false;
        canCreated = true;
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
                default:
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
    void ItsOver(float time)
    {
        StopGame();
        StopGameBrain();
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

}
