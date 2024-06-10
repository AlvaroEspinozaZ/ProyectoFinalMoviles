using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using DG.Tweening;
using System;
public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject score;
    [SerializeField] Vector2 posfinal;
    private RectTransform movingButtonRect;
    private void Start()
    {
        movingButtonRect = score.GetComponent<RectTransform>();        
        movingButtonRect.DOMove(posfinal, 0.6f).SetEase(Ease.OutBounce);
    }
    public async void Muerte(float time)
    {
        Vector3 tmp = new Vector3(transform.position.x, -10,transform.position.z);
        List<Task> currentTask = new List<Task>();
        currentTask.Add( transform.DOScale(Vector3.zero, time/3).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        currentTask.Add(transform.DOMove(tmp, time).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        await Task.WhenAll(currentTask);
    }
}
