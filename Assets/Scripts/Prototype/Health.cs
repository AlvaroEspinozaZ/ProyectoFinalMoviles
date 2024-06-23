using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;
public class Health : MonoBehaviour
{

    public int characterHealth = 100;
    private int maxHealth;
    public bool isDeath => characterHealth <= 0;

    [Header("UI Character")]
    public Image HealthBar;

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
        //Debug.Log("contandos" + Time.time);
        Destroy(gameObject, time);
    }
    public void UpdateCharacterUI()
    {
        if (HealthBar == null) return;
        HealthBar.fillAmount = (float)characterHealth/ (float)maxHealth;
    }
}
