using UnityEngine;
using System;
using System.Collections.Generic;

public class VisionSensorPrimitive : MonoBehaviour
{
    [Header("Opciones")]
    public SphereCollider visionCollider;

    public LayerMask detectableLayers;
    public LayerMask layersAlied;
    private WarriorData _warriorData;
    public float visionRange = 6.0f;
    public float stopDistance = 1.3f;
    public float rotationSpeed = 5.0f;
    public int id;
    [Header("Personaje Estructura")]
    public bool isTower = false;
    public bool isTowerToAttack = false;
    [Header("Personaje Seleccionado")]
    public GameObject _selection;
    public bool _isSelection=false;
    [Header("Personaje Detectado")]
    public GameObject objectCollision;
    public Health currentEnemy;
    public Health currentAlied;
    public List<Health> _alieds;
    public bool isObjectDetected = false;
    public bool isCurrentMove = false;
    public bool areEnemysNear = false;
    Vector3 destination;
    public Action<float, VisionSensorPrimitive,Health> counterAttack;
    
    private void Start()
    {
        _warriorData = GetComponent<Health>()._warriorData;
        if (visionCollider!=null)
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
        //if (objectCollision == null)
        //{
        //    currentEnemy = null;
        //}
        FeedBackSelectionCharacter();
        if (objectCollision == gameObject)
        {
            objectCollision = null;
            currentEnemy = null;
            isObjectDetected = false;
        }
        if (objectCollision == null || currentEnemy == null)
        {
            areEnemysNear = true;
        }
        else if (objectCollision != null && currentEnemy != null)
        {
            areEnemysNear = false;
        }
        if (areEnemysNear && objectCollision==null&& currentEnemy == null)
        {
            visionCollider.enabled = false;
            visionCollider.enabled = true;
        }
    }
    public void FeedBackSelectionCharacter()
    {
        if (_selection!=null){
            if (_isSelection == true)
            {
                _selection.SetActive(false);
            }
            else
            {
                _selection.SetActive(true);
            }
        }
   
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (((1 << collision.gameObject.layer) & detectableLayers) != 0)
        {
            if (collision.gameObject.GetComponent<VisionSensorPrimitive>() != null)
            {
                if (collision.gameObject.GetComponent<VisionSensorPrimitive>().isTower)
                {
                    isTowerToAttack = true;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (((1 << collision.gameObject.layer) & detectableLayers) != 0)
        {
            
            isTowerToAttack = false;              
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
            isObjectDetected = true;
            if (currentEnemy != null)
            {
                if (currentEnemy.isDeath)
                {
                    isObjectDetected = false;
                    visionCollider.enabled = false;
                }
            }
            
        }
        visionCollider.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (objectCollision == other.gameObject)
        {
            isObjectDetected = false;
            objectCollision = null;
             currentEnemy = null;
            areEnemysNear = false;
        }
    }

    void MoveTowardsObject()
    {
        isCurrentMove = false;
        Vector3 direction = (objectCollision.transform.position - transform.position).normalized;
        direction.y = 0;
        Vector3 mine = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 target = new Vector3(objectCollision.transform.position.x, 0, objectCollision.transform.position.z);
        float distance = Vector3.Distance(mine, target);

        if (distance > _warriorData.distanceAttack)
        {
            transform.position += direction * _warriorData.speed * Time.deltaTime;
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
            Vector3 mine = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 target = new Vector3(destination.x, 0, destination.z);
            float distance = Vector3.Distance(mine, target);

            if (distance > stopDistance)
            {
                Vector3 movement = direction * _warriorData.speed * Time.deltaTime;
                transform.position += movement;

                //RaycastHit hit;
                //if (Physics.Raycast(transform.position, direction, out hit, distance, detectableLayers))
                //{
                //    isCurrentMove = false;
                //    Debug.Log("JIJIIII");
                //}
            }
            else
            {
                isCurrentMove = false;
                //Debug.Log("JOJOOOO");
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
        //Debug.Log("isCurrentMove-> " + isCurrentMove);
    }

    void CounterAttack(float distance, VisionSensorPrimitive tmp,Health myHealth)
    {  
        if(tmp.currentEnemy!= myHealth)
        {
            float distance1 = Vector3.Distance(transform.position, tmp.currentEnemy.gameObject.transform.position);
            if (distance1 < distance)
            {                
                //Debug.Log("Se mantiene");
                //Debug.Log("distance1 " + distance1);
                //Debug.Log("distance1 " + distance);
            }
            else
            {
                //Debug.Log("distance1 " + distance1);
                //Debug.Log("distance1 " + distance);
                tmp.objectCollision = gameObject;
                tmp.currentEnemy = myHealth;
                tmp.isObjectDetected = true;
                //Debug.Log("Cambiamos");
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

