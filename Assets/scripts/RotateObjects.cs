using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    GameObject charger;
    float rotationAngle;
    [SerializeField] bool player;
    // Start is called before the first frame update
    void Start()
    {
        rotationAngle = 0;
    }
 
    private void OnMouseDown()
    {

        AudioManager.instance.Play("PrsimRotate");
        //if (player)
        //{           
        //    //ShootRay.instance.ResetLineRendere();

        //    //rotationAngle += 90;

        //    //transform.eulerAngles = new Vector3(0, rotationAngle, 0);
        //}
        if(!PauseMenu.stop)
        {
            rotationAngle += 90;

            transform.eulerAngles = new Vector3(0, rotationAngle, 0);
            ////modified
            //transform.root.eulerAngles = new Vector3(0, rotationAngle, 0);


            if (charger != null)
            {
                charger.GetComponent<ShootRay>().TrimPointsIndex(transform.position);
                transform.GetChild(0).GetComponent<PrismReflection>().DelayRaycast();

                //modified
                //charger.GetComponent<ShootRay>().TrimPointsIndex(transform.position);
                //transform.root.GetChild(0).GetComponent<PrismReflection>().DelayRaycast();
            }
        }
    }

    public void Charger(GameObject obj)
    {
        charger = obj;
    }
}
