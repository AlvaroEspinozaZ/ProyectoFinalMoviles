using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
public class Health : MonoBehaviour
{

    public float characterHealth = 100f;
    public bool isDeath => characterHealth <= 0;
    void Update()
    {
        
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
}
