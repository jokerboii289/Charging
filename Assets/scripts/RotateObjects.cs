using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour //rotate attached to the prism
{
    GameObject charger;
    float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        rotationAngle = transform.eulerAngles.y;
    }
 
    private void OnMouseDown()
    {
        if(!PauseMenu.stop)
        {
            rotationAngle += 90;

            transform.eulerAngles = new Vector3(0, rotationAngle, 0);

            AudioManager.instance.Play("PrsimRotate");  //Tap sound 
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
