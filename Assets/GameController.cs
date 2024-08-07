using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject prefab;
    Vector2 initPos;
    public Vector3 worldInitPos;
    public Vector3 worldTouchPos;
    public Vector2 touchPos;
    float directionX;
    float directionY;

    public float timer;
    void Update()
    {
        WithTouch();
    }
    void WithTouch()
    {       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initPos = touch.position;
            }
            else if ( touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.position;
                worldInitPos = Camera.main.ScreenToWorldPoint(new Vector3(initPos.x, initPos.y, Camera.main.nearClipPlane));
                worldTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));
                timer += Time.deltaTime;
                float directionX = worldTouchPos.x - worldInitPos.x;
                float directionY = worldTouchPos.y - worldInitPos.y;
                if (directionY > 0.15f && timer>0.35f)
                {
                    Vector3 spawnPos = new Vector3(worldTouchPos.x, worldTouchPos.y, prefab.transform.position.z);
                    GameObject tmp = Instantiate(prefab, spawnPos, Quaternion.identity);
                    timer = 0;
                }
            }
        }
    }

}
