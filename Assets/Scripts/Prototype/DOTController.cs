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

}
