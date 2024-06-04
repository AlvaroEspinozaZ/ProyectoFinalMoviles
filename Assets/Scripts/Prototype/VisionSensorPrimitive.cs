using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensorPrimitive : MonoBehaviour
{
    [Header("Opciones")]
    public SphereCollider visionCollider;
    public LayerMask detectableLayers;
    public float visionRange = 4.0f;
    public float speed = 2.0f;
    public float stopDistance = 2.0f;
    public float rotationSpeed = 5.0f;

    [Header("Personaje Detectado")]
    public GameObject objectCollision;

    public bool isObjectDetected = false;

    private void Start()
    {
        visionCollider.radius = visionRange;
    }

    private void Update()
    {
        if (!isObjectDetected)
        {
            objectCollision = null;
        }
        else if (objectCollision != null)
        {
            MoveTowardsObject();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isObjectDetected && ((1 << other.gameObject.layer) & detectableLayers) != 0)
        {
            objectCollision = other.gameObject;
            isObjectDetected = true;
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
        Vector3 direction = (objectCollision.transform.position - transform.position).normalized;
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
}
