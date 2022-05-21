using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour  //this script is attached to gameManager
{
    [SerializeField] GameObject winPanel;
    public static bool stop;
    public static PauseMenu instance;
    [SerializeField]int countNoOfPhones;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        instance = this;
        Vibration.Init();
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        winPanel.SetActive(false);
    }

    void Win()
    {
        if (countNoOfPhones < 1)
        {
            stop = true;
            print("win panel active");
            StartCoroutine(DelayWinPanel());
        }
    }
    public void UpdateNoOfChargedPhones()
    {
        countNoOfPhones -= 1;
        Win();
    }
    public void NegateCharges()
    {
        countNoOfPhones += 1;
    }

    IEnumerator DelayWinPanel()
    {
        print(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(2f);
        //function For sound win sound
        winPanel.SetActive(true); //winpanel
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        NextLvl();
    }

    public void NextLvl() //for next level 
    {
       // print("welcom");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
