using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressPlay : MonoBehaviour
{
    [SerializeField] GameObject Texto;
    [SerializeField] GameObject BotonH;
    [SerializeField] GameObject BotonM;


    public void jugar()
    {
        Texto.SetActive(true);
        BotonH.SetActive(true);
        BotonM.SetActive(true);
    }

}
