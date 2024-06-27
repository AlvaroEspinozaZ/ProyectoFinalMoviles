using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfiguration", menuName = "Scriptable Objects/Army")]

public class SpamArmy : ScriptableObject
{
    public GameObject prefab;
    public int cant;
    public int id;
    public GameObject[] listHide;
    public List<VisionSensorPrimitive> listArmy;
    float right = 0;
    float left = 0;
    public void Instatiate(Vector3 tmpinit)
    {
        listHide = new GameObject[cant];
        
            left = tmpinit.z;

        for (int i = 0; i< cant/10; i++)
        {
            right = tmpinit.x;
            for (int j = 0; j < cant/10; j++)
            {
                Vector3 tmp = new Vector3(right, tmpinit.y+2, left);
                GameObject soldier=  Instantiate(prefab, tmp, Quaternion.identity);
                soldier.GetComponent<VisionSensorPrimitive>().id= id;
                listArmy.Add(soldier.GetComponent<VisionSensorPrimitive>());
                right += 4;
            }            
            left += 4;
        }
        id++;
    }

    public void MoveArmy(Vector3 posToMove,int id)
    {
            for (int i = 0; i < listArmy.Count; i++)
            {
                if(listArmy[i].id == id)
                {
                    listArmy[i].SelectDestination(posToMove);

                }
        }        
    }
}
