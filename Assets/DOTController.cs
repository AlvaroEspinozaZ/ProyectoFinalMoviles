using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using DG.Tweening;
using System;
public class DOTController : MonoBehaviour
{
    [SerializeField] GameObject score;
    [SerializeField] [Tooltip("Posicion final a la que se movera el objeto pero como gameobject, no rect transform")] 
    Vector2 posfinal;
    [SerializeField] Ease easeDOT;
    [SerializeField] float delay;
    private RectTransform movingButtonRect;
    private void Start()
    {
        if (score != null)
        {
            movingButtonRect = score.GetComponent<RectTransform>();
            movingButtonRect.DOMove(posfinal, delay).SetEase(easeDOT);
        }
    }
    public async void Muerte(float time)
    {
        Vector3 tmp = new Vector3(transform.position.x, -10,transform.position.z);
        List<Task> currentTask = new();
        currentTask.Add( transform.DOScale(Vector3.zero, time/3).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        currentTask.Add(transform.DOMove(tmp, time).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        await Task.WhenAll(currentTask);
        Debug.Log("contandos" + Time.time);
        Destroy(gameObject, time);
    }
}
