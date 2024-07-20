using UnityEngine;
using System;
public class VisionSensorPrimitive : MonoBehaviour
{
    [Header("Opciones")]
    public SphereCollider visionCollider;
    public LayerMask detectableLayers;
    public float visionRange = 4.0f;
    public float speed = 2.0f;
    public float stopDistance = 2.0f;
    public float rotationSpeed = 5.0f;
    public bool isTower = false;
    public int id;
    [Header("Personaje Seleccionado")]
    public GameObject _selection;
    public bool _isSelection=false;
    [Header("Personaje Detectado")]
    public GameObject objectCollision;
    public Health currentEnemy;
    public Health[] currentAlied;
    public bool isObjectDetected = false;
    public bool isCurrentMove = false;
    Vector3 destination;
    public Action<float, VisionSensorPrimitive,Health> counterAttack;
    private void Start()
    {
        if(visionCollider!=null)
            visionCollider.radius = visionRange/transform.localScale.x;
    }

    private void Update()
    {
        if (isCurrentMove && !isObjectDetected)
        {
            MoveToDestination();
        }

        if (!isObjectDetected)
        {
            objectCollision = null;
        }
        else if (objectCollision != null)
        {
            MoveTowardsObject();
        }
        FeedBackSelectionCharacter();
        if (objectCollision == gameObject)
        {
            objectCollision = null;
            currentEnemy = null;
            isObjectDetected = false;
        }
    }
    public void FeedBackSelectionCharacter()
    {
        if (_selection!=null){
            if (_isSelection == true)
            {
                _selection.SetActive(false);
                //_isSelection = false;
            }
            else
            {
                _selection.SetActive(true);
                //_isSelection = true;
            }
        }
   
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isObjectDetected && ((1 << other.gameObject.layer) & detectableLayers) != 0)
        {
            objectCollision = other.gameObject;
            if (objectCollision.layer!= gameObject.layer)
            {
                currentEnemy = objectCollision.GetComponent<Health>();
            }
            else
            {
                //currentAlied[0] = objectCollision.GetComponent<Health>();
            }
            
            isObjectDetected = true;
            if (currentEnemy.isDeath)
            {
                isObjectDetected = false;
                visionCollider.enabled = false;
            }
            visionCollider.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (objectCollision == other.gameObject)
        {
            isObjectDetected = false;
            objectCollision = null;
        }
    }

    void MoveTowardsObject()
    {
        isCurrentMove = false;
        Vector3 direction = (objectCollision.transform.position - transform.position).normalized;
        direction.y = 0;

        float distance = Vector3.Distance(transform.position, objectCollision.transform.position);

        if (distance > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveToDestination()
    {
        Vector3 direction = (destination - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            float distance = Vector3.Distance(transform.position, destination);
            if (distance > stopDistance)
            {
                Vector3 movement = direction * speed * Time.deltaTime;
                transform.position += movement;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, distance, detectableLayers))
                {
                    isCurrentMove = false;
                }
            }
            else
            {
                isCurrentMove = false;
            }
        }
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SelectDestination(Vector3 position)
    {
        
        destination = position;
        isCurrentMove = true;
    }

    void CounterAttack(float distance, VisionSensorPrimitive tmp,Health myHealth)
    {
        if(tmp.currentEnemy!= myHealth)
        {
            float distance1 = Vector3.Distance(transform.position, tmp.currentEnemy.gameObject.transform.position);
            float distance2 = Vector3.Distance(transform.position, tmp.gameObject.transform.position);
            if (distance1 < distance2)
            {                
                Debug.Log("Se mantiene");
            }
            else
            {
                tmp.objectCollision = gameObject;
                tmp.currentEnemy = myHealth;
                tmp.isObjectDetected = true;
                Debug.Log("Cambiamos");
            }
        }
      
    }
    private void OnEnable()
    {
        counterAttack += CounterAttack;
    }
    private void OnDisable()
    {
        counterAttack -= CounterAttack;
    }
}

