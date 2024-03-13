using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sWinPanelUI : MonoBehaviour
{
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
