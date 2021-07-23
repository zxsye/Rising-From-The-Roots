using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void LoadPhaseOneScene()
    {
        SceneManager.LoadScene("PhaseOneScene");
    }

    public void LoadPhaseTwoScene()
    {
        SceneManager.LoadScene("PhaseTwoScene");
    }

    public void LoadPhaseThreeScene()
    {
        SceneManager.LoadScene("PhaseThreeScene");
    }

    public void LoadVNOneScene()
    {
        SceneManager.LoadScene("VN1");
    }

    public void LoadVNTwoScene()
    {
        SceneManager.LoadScene("VN2");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadCutScene(int i)
    {
        if (i == 1)
        {
            LoadVNOneScene();
        }
        else if (i == 2)
        {
            LoadVNTwoScene();
        }
        else if (i == 3)
        {
            LoadWinScene();
        }
    }

    public void LoadPhaseScene(int i)
    {
        if(i == 1)
        {
            LoadPhaseTwoScene();
        }
        else if (i == 2)
        {
            LoadPhaseThreeScene();
        }
        else if (i == 3)
        {
            LoadWinScene();
        }
    }

    

}
