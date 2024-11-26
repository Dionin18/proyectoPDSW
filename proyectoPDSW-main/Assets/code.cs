using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class code : MonoBehaviour
{
    [Header("Parte 1")]
    [SerializeField] private code Code;

    private void OnClick()
    {
        this.gameObject.SetActive(false);
    }
}
