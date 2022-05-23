using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour  //this script is attached to gameManager
{
    public static PauseMenu instance;

    [SerializeField] GameObject winPanel;
    public static bool stop;   
    [SerializeField]int countNoOfPhones;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        stop = false;
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
        yield return new WaitForSeconds(2f);

        // write function For win sound

        winPanel.SetActive(true); //winpanel
    }


    public void NextLvl() //for next level  assign to next button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void ReloadScene()// assign to reload button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
