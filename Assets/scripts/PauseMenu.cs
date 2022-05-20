using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool stop;
    public static PauseMenu instance;
    [SerializeField]int countNoOfPhones;
    // Start is called before the first frame update
    void Start()
    {
        Vibration.Init();
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
        instance = this;
        stop = false;
    }

    public void Win()
    {
        UpdateNoOfChargedPhones();
        if (countNoOfPhones < 1)
        {
            stop = true;
            print("win panel active");
            StartCoroutine(Delay());
        }
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void UpdateNoOfChargedPhones()
    {
        countNoOfPhones -= 1;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0f);
        //win panel
    }
}
