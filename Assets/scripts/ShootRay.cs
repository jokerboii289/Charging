using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
    public static ShootRay instance;
    [SerializeField]LayerMask ignoreLayer;
    int start;
    int countIndex;
    [SerializeField] float speed;
    [SerializeField] GameObject EmptyObject;
    

    [SerializeField] float duration;//linerender speed;
    
    bool castRay;  // to dealay
    LineRenderer lineRenderer;
    public List<Vector3> points = new List<Vector3>();
    
    Vector3 second;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       
        start = 0;
        countIndex = 1;
        castRay = true;
        lineRenderer = GetComponent<LineRenderer>();
        points.Add(transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, points[0]);
    }

    // Update is called once per frame
    void Update()
    {       
        PointOnWallFirst(); // point on wall
                          
        StartCoroutine(LineRenderAnimator());     
        //LineRenderAnimator();
    }

    void LineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }

    IEnumerator LineRenderAnimator() // animate line
    {
        if(lineRenderer.positionCount < points.Count)//countIndex<points.Count
            lineRenderer.positionCount = countIndex+1;
        else if(lineRenderer.positionCount > points.Count)//countIndex > points.Count
        {
            countIndex = points.Count -1;
            start = countIndex - 1;
            EmptyObject.transform.position = points[countIndex];

            lineRenderer.positionCount = points.Count;
        }    

        if (points.Count > 1)
        {
            if (start != points.Count - 1)
            {
                int end;
                end = start + 1;
                Vector3 startPos = points[start];

                Vector3 endPos = points[end];

                var direction = (endPos - startPos);
                if (direction.magnitude > .01)
                {
                    EmptyObject.transform.position = Vector3.Lerp(EmptyObject.transform.position, endPos, 5f * Time.deltaTime);
                    lineRenderer.SetPosition(end, EmptyObject.transform.position);
                    yield return null;
                }
            }
        }
        
        if (start < points.Count - 1 && (EmptyObject.transform.position - points[start+1]).magnitude < .09f)
        {
            lineRenderer.SetPosition(start+1, points[start+1]);
            EmptyObject.transform.position = points[start+1];
            start += 1;

            if(countIndex<points.Count)
                countIndex++;
        }
    }


    public void PointOnWallFirst()  // point on wall
    {
        if (Physics.Raycast(transform.position, transform.forward,out RaycastHit hit, 1000/*, ~ignoreLayer*/) && castRay)
        {
            if (hit.collider.CompareTag("reflect"))
            {
                second = hit.transform.root.transform.position;

                if (!points.Contains(second))
                {
                    points.Add(second);
                }
                //reflect ray
                var reflectRay = Vector3.Reflect(transform.forward, hit.normal);
                var temp = hit.collider.gameObject.GetComponent<PrismReflection>();

                hit.transform.root.gameObject.GetComponent<RotateObjects>().Charger(gameObject);
                ////modified
                //hit.transform.root.GetChild(3).gameObject.GetComponent<RotateObjects>().Charger(gameObject);

                temp.ReflectRay(reflectRay,gameObject);           
            }
            
            else if (hit.collider.CompareTag("prismbody"))
            {
                second = hit.transform.position;

                if (!points.Contains(second))
                {
                    points.Add(second);
                }
                hit.transform.gameObject.GetComponent<RotateObjects>().Charger(gameObject);
                ////modified
                //hit.transform.GetChild(3).gameObject.GetComponent<RotateObjects>().Charger(gameObject);
            }
            else if (hit.collider.CompareTag("threeway"))
            {
                second = hit.transform.position;

                if (!points.Contains(second))
                {
                    points.Add(second);
                }
                hit.transform.root.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("Phone"))
            {
                print("destination");
                UpdateListOfpoints(hit.transform.position);
            }
        }
    }

    public void ResetLineRendere()  //when charger rotates
    {
        castRay = false;
        for(int i=0;i<points.Count;i++)
        {           
            points.RemoveAt(i);
        }
        StartCoroutine(Delay());     
    }

    IEnumerator Delay()
    {      
        yield return new WaitForSeconds(0.01f);
        castRay = true;
        points.Add(transform.position);//first index is shoot position
    }

    public void UpdateListOfpoints(Vector3 pos)//line renderer;
    {
        if (!points.Contains(pos))
            points.Add(pos);
    }

    public void TrimPointsIndex(Vector3 pos)
    {
        int index=0;
        int temp;
        if (points.Contains(pos) && pos != points[points.Count - 1])
        {
            castRay = false;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == pos)
                {
                    index = i;                  
                    break;
                }             
            }
            temp = index + 1;
            points.RemoveRange(temp, points.Count-temp);
            StartCoroutine(DelayUpdate());
        }
    }

    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(.05f);
        castRay = true;
    }   

    public void CheckPoints(Transform pos)
    {
        if(points.Contains(pos.position))
        {
            pos.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.SetFloat("_emmision", 40);
        }
        else
        {
            pos.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.SetFloat("_emmision", 40);
        }
    }
}
