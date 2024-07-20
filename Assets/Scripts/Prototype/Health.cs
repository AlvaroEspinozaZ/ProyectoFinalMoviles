using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;
using System;
public class Health : MonoBehaviour
{

    public int characterHealth = 100;
    private int maxHealth;
    public bool isDeath => characterHealth <= 0;
    private CancellationTokenSource cancellationTokenSource;
    [Header("UI Character")]
    public Image HealthBar;
    public Action<int> eventTakeDamage;
    public Action eventTakeDamageUI;
    public Action<float> eventDead;
    private void Start()
    {
        maxHealth = characterHealth;
    }
    public async void Died(float time)
    {
        Vector3 tmp = new Vector3(transform.position.x, -10, transform.position.z);
        List<Task> currentTask = new();
        currentTask.Add(transform.DOScale(Vector3.zero, time / 3).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        currentTask.Add(transform.DOMove(tmp, time).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        await Task.WhenAll(currentTask);
        Destroy(gameObject, time);
    }
    public async void Desapear(float time)
    {
        //await 5 segundos
        await Task.Delay(5000);
        gameObject.SetActive(false);
    }
    public void UpdateCharacterHealthUI()
    {
        if (HealthBar == null) return;
        HealthBar.fillAmount = (float)characterHealth / (float)maxHealth;
        //Debug.Log("Actulizamos a " + gameObject + " y su vida es " + HealthBar.fillAmount);
    }
    public void UpdateCharacterHealth(int damage)
    {
        characterHealth -= damage;
        //Debug.Log("Actulizamos a "+ gameObject+" y le quitamos : "+ damage);
    }
    public void CancelDied()
    {
        cancellationTokenSource?.Cancel();
    }
    private void OnEnable()
    {
        //Recibir daño
        eventTakeDamageUI += UpdateCharacterHealthUI;
        eventTakeDamage += UpdateCharacterHealth;
        //Al morir
        eventDead += Died;
    }
    private void OnDisable()
    {
        //Recibir daño
        eventTakeDamageUI -= UpdateCharacterHealthUI;
        eventTakeDamage -= UpdateCharacterHealth;
        //Al morir
        eventDead -= Died;
    }
    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }
}
