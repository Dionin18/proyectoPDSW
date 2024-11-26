using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialBoton : MonoBehaviour
{
    public void OnStartTutorialClicked()
    {
        SceneManager.LoadSceneAsync("Prototype2");
    }
}
