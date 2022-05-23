using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinerendererTracer : MonoBehaviour   // attached to linetracer (empty gameObject which is set to the linerendere last position for line rendere animation)
{
    [SerializeField] GameObject phoneChargingScreen;  //charging screen of the phone(sub gameobject of mobile
    [SerializeField] Image chargePercentage; //percentage wheel of the phone(sub gameobject of mobile
    float startAmount ;
    bool start;
    bool stop;
   
    // Start is called before the first frame update
    void Start()
    {
        phoneChargingScreen.SetActive(false);
        startAmount = 0;
        start = false;
        stop = false;   // this bool mainly resposible for  stoping further tapping of screen when game is finished
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
                Stoper();
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
            PauseMenu.instance.UpdateNoOfChargedPhones();
            other.GetComponent<Phone>().charged = true;
            Vibration.Vibrate(30);         //for vibration

            AudioManager.instance.Play("PhoneCharge");
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
            PauseMenu.instance.NegateCharges();
            other.GetComponent<Phone>().charged = false;
            AudioManager.instance.Play("PhoneDischarge");
            
        }
    }


    void Stoper() //stop percentage wheel
    {
        stop = true;
    }

   
}
