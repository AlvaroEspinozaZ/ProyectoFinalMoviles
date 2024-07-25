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

        for (int i = 0; i< cant/2; i++)
        {
            right = tmpinit.x;
            for (int j = 0; j < cant/2; j++)
            {
                Vector3 tmp = new Vector3(right, tmpinit.y+2, left);
                CreateSoldier(tmp);
                right += 4;
            }            
            left += 4;
        }
        id++;
    }
 
    public void InstantiateTwoTriangles(Vector3 tmpinit)
    {
        listHide = new GameObject[cant];
        int halfCount = cant / 2;

        
        float right = tmpinit.x - 20; 
        float left = tmpinit.z;
        int count = 0;
        int rowCount = Mathf.CeilToInt(Mathf.Sqrt(halfCount));

        for (int i = 0; i < rowCount && count < halfCount; i++)
        {
            for (int j = 0; j <= i && count < halfCount; j++)
            {
                Vector3 tmp = new Vector3(right + j * 4 - i * 2, tmpinit.y + 2, left + i * 4);
                CreateSoldier(tmp);
                count++;
            }
        }

       
        right = tmpinit.x + 20; 
        left = tmpinit.z;
        count = 0;

        for (int i = 0; i < rowCount && count < halfCount; i++)
        {
            for (int j = 0; j <= i && count < halfCount; j++)
            {
                Vector3 tmp = new Vector3(right + j * 4 - i * 2, tmpinit.y + 2, left + i * 4);
                CreateSoldier(tmp);
                count++;
            }
        }

        id++;
    }
    private void CreateSoldier(Vector3 position)
    {
        GameObject soldier = Instantiate(prefab, position, Quaternion.identity);
        soldier.GetComponent<VisionSensorPrimitive>().id = id;
        listArmy.Add(soldier.GetComponent<VisionSensorPrimitive>());
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
    public void ActiveSelectionArmy(bool selection, int id)
    {
        if (selection)
        {
            for (int i = 0; i < listArmy.Count; i++)
            {
                if (listArmy[i].id == id)
                {
                    listArmy[i]._isSelection = false; ;

                }
            }
        }
        else
        {
            for (int i = 0; i < listArmy.Count; i++)
            {
                if (listArmy[i].id == id)
                {
                    listArmy[i]._isSelection = true;

                }
            }
        }
   
    }
    private void OnDisable()
    {
        id = 0;
        listArmy.Clear();
        if (listHide != null)
        {
            listHide = null;
        }
    }
}
