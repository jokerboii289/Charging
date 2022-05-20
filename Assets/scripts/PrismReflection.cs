using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismReflection : MonoBehaviour
{
    GameObject temp;
    [SerializeField]LayerMask ignoreLayer;
    public bool castRay;
    public GameObject threeWay;
    public GameObject tempChargerHolder;
    [SerializeField] Transform rayShootPoint;
    public bool reflect;
    // Start is called before the first frame update
    void Start()
    {
        castRay = true;
        reflect = false;
    }

    public void ReflectRay(Vector3 directionTocastRay,GameObject charger)
    { 
        temp = charger;
        transform.root.gameObject.GetComponent<RotateObjects>().Charger(charger);
        //modified
        //transform.root.GetChild(3).gameObject.GetComponent<RotateObjects>().Charger(charger);

        if (Physics.Raycast(rayShootPoint.position, directionTocastRay, out RaycastHit hit, 1000/*, ~ignoreLayer*/) && castRay)
        {
            //reflect ray
            if (hit.collider.CompareTag("reflect"))
            {
                charger.GetComponent<ShootRay>().UpdateListOfpoints(hit.transform.root.transform.position);

                var reflectRay = Vector3.Reflect(directionTocastRay, hit.normal);
                var temp = hit.collider.gameObject.GetComponent<PrismReflection>();

                temp.ReflectRay(reflectRay,charger);             
            }
            else if (hit.collider.CompareTag("prismbody"))
            {
               
                charger.GetComponent<ShootRay>().UpdateListOfpoints(hit.transform.position);
            }

            else if (hit.collider.CompareTag("Phone"))
            {     
                charger.GetComponent<ShootRay>().UpdateListOfpoints(hit.transform.position);
            }
            else if (hit.collider.CompareTag("threeway"))
            {
                threeWay = hit.transform.root.transform.GetChild(0).gameObject;
                charger.GetComponent<ShootRay>().UpdateListOfpoints(hit.transform.position);
               
                threeWay.SetActive(true);
            }
        }
    }

    public void DelayRaycast()
    {
        castRay = false;
        StartCoroutine(DelayUpdate());
    }
    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(0.05f);
        castRay = true;
    }

}
