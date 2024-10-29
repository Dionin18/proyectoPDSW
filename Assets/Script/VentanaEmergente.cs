using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaEmergente : MonoBehaviour
{
    public GameObject VentanaFlotante;
    public void Start()
    {
    }

    public void OnclickAcepttButton()
    {
        VentanaFlotante.active = false;

    }
}
