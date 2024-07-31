using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Threading.Tasks;
using DG.Tweening;
public class Test : MonoBehaviour
{

    [Header("InputsTaps")]
    //Tap
    private float totalTime = 0f;
    private float swipeTimer = 0f;
    private bool allowIncrease = false;
    private float maxTimeDT = 0.2f;
    private float minTimePress = 0.15f;
    //swipe
    private float swipeLoopTime = 2f;
    private float swipeMinDistance = 0.45f;
    bool isTapped = true;
    bool isPress = false;
    bool isHolding = false;
    private int countHolding = 0;
    public Button[] btns;
    public int btnNumber;
    public bool movingCamera=false;
    //double tap
    bool isDoubleTap = false;
    private float doubleTapTime = 0.3f; 
    private float lastTapTime = 0f;
    [Header("Strategy")]
    [SerializeField] private VisionSensorPrimitive currentObject;
    [SerializeField] private StrategySO warriorSO;
    [SerializeField] LayerMask touchableLayers;
    [SerializeField] bool couldCreated=false;
    [SerializeField] bool canCreated = false;
    [SerializeField] bool isMovingArmy = false;
    [SerializeField] bool isTouchingUnit = false;
    [Header("FeedBack")]
    [SerializeField] GameObject flag;
    public int id;
    [Header("Camera******")]
    public float velocityCamera=12;
    public bool ISCameraBlocked = false;
    [SerializeField] private Vector2 limitX;
    [SerializeField] private Vector2 limitZ;
    Vector3 currentPosition;
    [SerializeField] Camera mainCamera;
    Vector2 positionCamera;
    Vector2 currentpositionCamera;
    [SerializeField] Ease easeDOT;
    [SerializeField] Vector3 posfinal;
    private void Start()
    {
        flag.SetActive(false);
        for (int i = 0; i < btns.Length; i++)
        {
            int id = i;
            btns[i].onClick.AddListener(() => DetectedBtn(id));
        }
    }
    private void Update()
    {
        if (allowIncrease)
        {
            totalTime += Time.deltaTime;
            isHolding = false;
        }

        if (totalTime >= maxTimeDT)
        {
            allowIncrease = false;
            totalTime = 0f;            
            isDoubleTap = false;            
            isTapped = true;
            isHolding = true;
        }
        if (isPress)
        {
            swipeTimer += Time.deltaTime;            
        }
        if (swipeTimer >= swipeLoopTime)
        {
            isPress = false;
            swipeTimer = 0f;
            countHolding = 0;
        }
        if (mainCamera != null)
        {            
            if (isPress == true)
            {
                    Vector2 direction = new Vector2(currentpositionCamera.x - positionCamera.x, currentpositionCamera.y - positionCamera.y);
                    MySwipe(direction);
            }
        }

     

    }
    private void FixedUpdate()
    {
        if (isTapped)
            MyTap();
    }
    public void OnTouch(InputAction.CallbackContext context)
    {        
        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                Debug.Log("Waiting");

                break;
            case InputActionPhase.Disabled:
                Debug.Log("Disabled");
                break;
            case InputActionPhase.Started:
                flag.SetActive(false);
                //Debug.Log("Started");              
                isPress = true;
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    MyDoubleTap();
                    isTapped = false;
                    totalTime = maxTimeDT;
                }
                else
                {                    
                    if (totalTime >= minTimePress)
                    {
                        //isPress = true;
                        totalTime = minTimePress;
                        MyPress();
                    }
                }
                lastTapTime = Time.time;                
                break;
            case InputActionPhase.Performed:
                allowIncrease = true;
                isDoubleTap = true;
                break;
            case InputActionPhase.Canceled:
                //Debug.Log("Canceled");
                if (isPress)
                {
                    isPress = false;
                    isTapped = false;
                }
                if (couldCreated)
                {
                    canCreated = true;
                    couldCreated = false;
                }    
                movingCamera = false;
                for (int i = 0; i < btns.Length; i++)
                {
                    btns[i].interactable = true;
                }
                break;
        }
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        currentPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector2 currentP = currentPosition;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 centeredPosition = currentP - screenSize / 2;
        if(mainCamera!=null)
            currentpositionCamera = mainCamera.ViewportToScreenPoint( centeredPosition/1000000);
    }

    private void MyTap()
    {
        Ray ray = mainCamera.ScreenPointToRay(currentPosition);
        RaycastHit hit;     
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayers))
        {
           
            int layerInt = hit.transform.gameObject.layer;
            flag.SetActive(false);
            if (canCreated)
            {
                if (currentObject != null)
                {
                    warriorSO.GetCharacter().MoveArmy(hit.point, currentObject.id);
                }
            }
            switch (layerInt)
            {
                case 5:
                  
                    break;       
                case 6:
                    isTouchingUnit = true;
                    if (isTouchingUnit)
                    {
                        currentObject = hit.transform.gameObject.GetComponent<VisionSensorPrimitive>();
                        warriorSO = hit.transform.gameObject.GetComponent<Health>()._warriorData._strategy;
                        warriorSO.GetCharacter().ActiveSelectionArmy(currentObject._selection, currentObject.id);
                        //if (currentObject._selection)
                            isMovingArmy = true;

                    }
                    else {
                        flag.SetActive(false);
                         isMovingArmy = false; 
                    }


                        break;
                case 11:
                    isTouchingUnit = false;
                    if (currentObject != null)
                        {
                            if (isMovingArmy)
                            {
                                warriorSO.GetCharacter().MoveArmy(hit.point, currentObject.id);
                                flag.transform.position = hit.point;
                                flag.SetActive(true);
                            }
                        isMovingArmy = false;
                        }
                    
                    break;
                case 12: // Floor
                    isTouchingUnit = false;

                    if (!canCreated)
                    {
                        if (currentObject != null)
                        {
                            if (isMovingArmy)
                            {
                                warriorSO.GetCharacter().MoveArmy(hit.point, currentObject.id);
                                flag.transform.position = hit.point;
                                flag.SetActive(true);
                            }
                            isMovingArmy = false;
                        }
                    }
                    else
                    {
                        if (warriorSO.maxSpawn > 0)
                        {
                            warriorSO.Instantiate(new Vector3(hit.point.x, hit.point.y + 2, hit.point.z));
                            warriorSO.maxSpawn--;
                        }
                        flag.SetActive(false);
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
        // Debug.Log("DoubleTap");
        resetAll(warriorSO);
        
    }

    private void MySwipe(Vector2 direction)
    {
        if(ISCameraBlocked) return;
        float horizontal = direction.x;
        float vertical = direction.y;
        if(Mathf.Abs(horizontal)> Mathf.Abs(vertical))
        {
            if (horizontal >= 0.2f)
            {
                Debug.Log("Right");           
                posfinal = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, Mathf.Clamp(mainCamera.transform.position.z + (velocityCamera * Time.deltaTime), limitX.x, limitX.y));
                MoveCamera(0.01f, posfinal);
            }
            else if (horizontal <= -0.2f)
            {
                Debug.Log("Left");
                posfinal = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, Mathf.Clamp(mainCamera.transform.position.z - (velocityCamera * Time.deltaTime), limitX.x, limitX.y));
                MoveCamera(0.01f, posfinal);
            }
        }
        else if(Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            if (vertical >= 0.2f)
            {
                Debug.Log("Up");
                posfinal = new Vector3(Mathf.Clamp(mainCamera.transform.position.x - (velocityCamera * Time.deltaTime), limitZ.x, limitZ.y), mainCamera.transform.position.y, mainCamera.transform.position.z);
                MoveCamera(0.01f, posfinal);
            }
            else if (vertical <= -0.2f)
            {
                Debug.Log("Down");
                posfinal = new Vector3(Mathf.Clamp(mainCamera.transform.position.x + (velocityCamera * Time.deltaTime), limitZ.x, limitZ.y), mainCamera.transform.position.y, mainCamera.transform.position.z);
                MoveCamera(0.01f, posfinal);
            }
        }
    }
    private void DetectedBtn(int id)
    {
        btnNumber = id;
        SetCurrentBtn(id);
        movingCamera = true;
    }
    void SetCurrentBtn(int id)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            if (i != id)
            {
                btns[i].interactable=false;
            }
        }        
    }    
    public void MoveCamera(float time, Vector3 posfinal)
    {
        mainCamera.transform.DOMove(posfinal, time).SetEase(easeDOT);
    }
    public void SetWarriorPrefab(StrategySO current)
    {
        warriorSO = current;
        couldCreated = true;
    }
    public void resetAll(StrategySO current)
    {
        warriorSO = current;
        MoveCamera(0, mainCamera.transform.position);
        canCreated = false;
    }

    public void SetCameraLocked()
    {
        ISCameraBlocked = !ISCameraBlocked;
    }
}
