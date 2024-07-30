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
    private CancellationTokenSource cancellationTokenSource;
    [Header("UI Character")]
    [SerializeField] private WarriorData _general;
    [HideInInspector] public WarriorData _warriorData;
    public int characterHealth = 0;
    public int maxHealth = 0;
    public bool isDeath => characterHealth <= 0;
    public Image HealthBar;
    public Action<int> eventTakeDamage;
    public Action eventTakeDamageUI;
    public Action<float> eventDead;
    public Action<PrefabMovement> clearList;
    private void Awake()
    {
        _warriorData = _general;
        characterHealth = _warriorData.characterHealth;
        maxHealth = characterHealth;
    }
    private void Start()
    {
        
    }
    public void Died(float time)
    {
        StartCoroutine(DiedCorutine(time));
    }
    IEnumerator DiedCorutine(float time)
    {
        Vector3 tmp = new Vector3(transform.position.x, -10, transform.position.z);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.zero, time / 3).SetEase(Ease.OutBounce));
        sequence.Join(transform.DOMove(tmp, time).SetEase(Ease.OutBounce));
        yield return sequence.WaitForCompletion();        
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
        if (isDeath)
        {
            //Llamar evento de Muerte
            clearList?.Invoke(gameObject.GetComponent<PrefabMovement>());
            eventDead?.Invoke(_warriorData.timeToDeath);
            //enemy.Died(timeToDeath);
        }
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
