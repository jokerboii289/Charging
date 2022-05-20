using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinerendererTracer : MonoBehaviour
{
    [SerializeField] GameObject phoneChargingScreen;
    [SerializeField] Image chargePercentage;
    float startAmount ;
    bool start;
    bool stop;
    GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        phoneChargingScreen.SetActive(false);
        startAmount = 0;
        start = false;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {            
        if (start)
        {
            if (startAmount < 1.1)
            {
                startAmount += .6f * Time.deltaTime;
                chargePercentage.fillAmount = startAmount;
            }
            if(startAmount>=1 && !stop)
            {
                hit.GetComponent<Phone>().charged = true;
                InitiateWin();
            }
        }
        else if(!start && startAmount>0)
        {
            startAmount -= .6f * Time.deltaTime;
            chargePercentage.fillAmount = startAmount;
            if (startAmount < 0.1)
            {
                phoneChargingScreen.SetActive(false);
                startAmount = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Phone"))
        {
            //Vibration.Vibrate(30);
            AudioManager.instance.Play("PhoneCharge");
            hit = other.gameObject;
            phoneChargingScreen.SetActive(true);
            start = true;
        }
        else
        {
            start = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Phone"))
        {
            AudioManager.instance.Play("PhoneDischarge");
          
        }
    }


    void InitiateWin()
    {
        //function to call win
        PauseMenu.instance.Win();
        stop = true;
    }

   
}
