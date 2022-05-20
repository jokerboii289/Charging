using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserr : MonoBehaviour
{

    LineRenderer lineRenderer;
    List<Vector3> points = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    { 
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        PointOnWallFirst(); // point on wall
        LineRenderer();
    }

    void LineRenderer()
    {
        //if(!CheckPoint(transform.position))
           // points.Add(transform.position);
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
    void PointOnWallFirst()  // point on wall
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1000) /*&& hit.collider.CompareTag("reflect")*/)
        {
            print(hit.transform.name);
            if(!CheckPoint(hit.transform.position))
                points.Add(hit.transform.position);  //second point                
        }
        else
        {
          //  points.Add(transform.position);
        }
    }

    bool CheckPoint(Vector3 point)
    {
        bool exist = false;
        for(int i=0;i<points.Count;i++)
        {
            if(point==points[i])
            {
                exist = true;
            }
        }
        return exist;
    }
}
