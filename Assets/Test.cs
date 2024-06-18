using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Test : MonoBehaviour
{
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private float swipeTimer = 0f;
    [SerializeField] private bool allowIncrease = false;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask touchableLayers;
    InputAction.CallbackContext currentContext;
    [SerializeField] private VisionSensorPrimitive currentObject;
    [SerializeField] private GameObject prefabAllied;
    private float maxTimeDT = 0.2f;
    private float minTimePress = 0.15f;
    private float swipeLoopTime = 0.2f;
    private float swipeMinDistance = 3.0f;
    Vector3 currentPosition;
    Vector2 startPosition;
    bool isDoubleTap = false;
    bool isTapped = true;
    bool isPress = false;
    private void Update()
    {
        if (allowIncrease)
            totalTime += Time.deltaTime;

        if (totalTime >= maxTimeDT)
        {
            allowIncrease = false;
            totalTime = 0f;
            swipeTimer = 0f;
            isDoubleTap = false;
            if (isTapped)
                MyTap();
            isTapped = true;
            isPress = false;
        }
        else if (currentContext.phase == InputActionPhase.Performed && totalTime >= minTimePress)
        {
            swipeTimer += Time.deltaTime;
            isPress = true;
            totalTime = minTimePress;
            MyPress();
        }

        if (swipeTimer >= swipeLoopTime)
        {
            swipeTimer = 0f;
            float distance = Vector2.Distance(startPosition, currentPosition);
            if (distance >= swipeMinDistance)
            {
                MySwipe();
            }
            startPosition = currentPosition;
        }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        currentContext = context;
        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Started:
                if (isDoubleTap)
                {
                    isTapped = false;
                    MyDoubleTap();
                    totalTime = maxTimeDT;
                }
                startPosition = currentPosition;
                break;
            case InputActionPhase.Performed:
                allowIncrease = true;
                isDoubleTap = true;
                break;
            case InputActionPhase.Canceled:
                if (isPress)
                    isTapped = false;
                break;
        }
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        currentPosition = Touchscreen.current.primaryTouch.position.ReadValue();

    }

    //Inputs
    private void MyTap()
    {
        Ray ray = mainCamera.ScreenPointToRay(currentPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayers))
        {
            int layerInt = hit.transform.gameObject.layer;
            switch (layerInt)
            {
                case 3: //Floor
                    if (currentObject != null)
                        currentObject.SelectDestination(hit.point);
                    else
                        Instantiate(prefabAllied, new Vector3(hit.point.x,hit.point.y + 2, hit.point.z), Quaternion.identity);
                    break;
                case 6: //Player
                    currentObject = hit.transform.gameObject.GetComponent<VisionSensorPrimitive>();
                    break;
                default:
                    break;
            }
        }
    }

    private void MyPress()
    {
        currentObject = null;
    }

    private void MyDoubleTap()
    {
        Debug.Log("DoubleTap");
    }
    private void MySwipe()
    {
        Debug.Log("Swipe");
    }

    //Buttons
}
