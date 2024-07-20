using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Threading.Tasks;
using DG.Tweening;
public class Test : MonoBehaviour
{

    [Header("InputsTaps")]
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private float swipeTimer = 0f;
    [SerializeField] private bool allowIncrease = false;
    InputAction.CallbackContext currentContext;
    private float maxTimeDT = 0.2f;
    private float minTimePress = 0.15f;
    private float swipeLoopTime = 0.12f;
    private float swipeMinDistance = 0.3f;
    Vector3 currentPosition;
    Vector2 startPosition;
    bool isDoubleTap = false;
    bool isTapped = true;
    bool isPress = false;

    [Header("Strategy")]
    [SerializeField] private VisionSensorPrimitive currentObject;
    [SerializeField] private StrategySO warriorSO;
    [SerializeField] LayerMask touchableLayers;
    [SerializeField] bool canCreated=false;
    [Header("Camera")]
    [SerializeField] Camera mainCamera;
    Vector2 positionCamera;
    Vector2 currentpositionCamera;
    [SerializeField] Ease easeDOT;
    [SerializeField] float delay;
    [SerializeField] Vector3 posfinal;
    [SerializeField] Transform target;
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
            isTapped = true;
            isPress = false;
        }
  

        if (swipeTimer >= swipeLoopTime)
        {
            swipeTimer = 0f;
            float distance = Vector2.Distance(positionCamera, currentpositionCamera);
           
            if (distance >= swipeMinDistance)
            {
                //MySwipe();
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
                Debug.Log("Waiting");

                break;
            case InputActionPhase.Disabled:
                Debug.Log("Disabled");
                break;
            case InputActionPhase.Started:
                if (isDoubleTap)
                {
                    isTapped = false;
                    MyDoubleTap();
                    totalTime = maxTimeDT;
                }
                else
                {
                    Debug.Log("Deberia hacer tap");
                    isTapped = true;
                    MyTap();
                }
                positionCamera = currentpositionCamera;
                startPosition = currentPosition;
                //Debug.Log("startPosition: " + startPosition);
                break;
            case InputActionPhase.Performed:
                allowIncrease = true;
                isDoubleTap = true;
                if (totalTime >= minTimePress)
                {
                    isPress = true;
                    totalTime = minTimePress;
                    MyPress();
                }
                posfinal = mainCamera.transform.position;
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

        Vector2 currentP = currentPosition;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 centeredPosition = currentP - screenSize / 2;

        currentpositionCamera = mainCamera.ViewportToScreenPoint( centeredPosition/1000000);
    }


    private void MyTap()
    {
        Debug.Log("Se presiono");
        Ray ray = mainCamera.ScreenPointToRay(currentPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayers))
        {
            int layerInt = hit.transform.gameObject.layer;
         
            switch (layerInt)
            {
                case 5: 
                    
                    break;
                case 6: 
                    currentObject = hit.transform.gameObject.GetComponent<VisionSensorPrimitive>();
                    Debug.Log("NPCRojo");
                    break;
                case 12: // Floor
                    Debug.Log("Toco el suelo");
                    if (!canCreated)
                    {
                        if (currentObject != null)
                        {
                            Debug.Log("Podemos Mover Army");
                            warriorSO.GetCharacter().MoveArmy(hit.point, currentObject.id);
                        }
                    }
                    else
                    {
                        Debug.Log("Podemos Crear Army");
                        warriorSO.Instantiate(new Vector3(hit.point.x, hit.point.y + 2, hit.point.z));
                        canCreated = false;
                    }
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
        float horizontal = positionCamera.x - currentpositionCamera.x;
        float vertical = positionCamera.y - currentpositionCamera.y;
        if (horizontal >= 0.5f)
        {
            Debug.Log("Right");
            Debug.Log(horizontal);
            posfinal = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -27);
            MoveCamera(delay, posfinal);
        }
        else if (horizontal <= -0.5f)
        {
            Debug.Log("Left");
            Debug.Log(horizontal);
            posfinal = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y,27);
            MoveCamera(delay, posfinal);
        }
        else if(vertical >= 0)
        {
            Debug.Log("Up");
            Debug.Log(vertical);
            posfinal = new Vector3(28, mainCamera.transform.position.y, mainCamera.transform.position.z);
            MoveCamera(delay, posfinal);
            RotateCamera(delay,-180);
        }
        else if (vertical <= 0)
        {
            Debug.Log("Down");
            Debug.Log(vertical);
            posfinal = new Vector3(-28, mainCamera.transform.position.y, mainCamera.transform.position.z);
            MoveCamera(delay, posfinal);
            RotateCamera(delay , 180);
        }
        Debug.Log("Swipe");
    }
    public async void MoveCamera(float time,Vector3 posfinal)
    {        
        await mainCamera.transform.DOMove(posfinal, time).SetEase(easeDOT).AsyncWaitForCompletion();
    }
    public async void RotateCamera(float time,  float rotate)
    {
        float rotateTarget = Mathf.Clamp(mainCamera.transform.rotation.y + rotate,-90,90);
        Vector3 tmp = new Vector3(mainCamera.transform.rotation.x, rotateTarget, mainCamera.transform.rotation.z);
        await mainCamera.transform.DORotate(tmp, time).SetEase(easeDOT).AsyncWaitForCompletion();
    }
    //Buttons
    public void SetWarriorPrefab(StrategySO current)
    {
        warriorSO = current;
        canCreated = true;
    }
}
