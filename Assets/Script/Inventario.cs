using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;

    public GameObject Selector;
    public int ID;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                Image bagImage = Bag[i].GetComponent<Image>();
                if (bagImage != null && !bagImage.enabled)
                {
                    bagImage.enabled = true;
                    bagImage.sprite = coll.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }
        }
    }

    public void Navegar()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && ID < Bag.Count - 1)
        {
            ID++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && ID > 0)
        {
            ID--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && ID > 3) 
        {
            ID -= 4;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && ID < Bag.Count - 4) 
        {
            ID += 4;
        }

        if (ID >= 0 && ID < Bag.Count)
        {
            Selector.transform.position = Bag[ID].transform.position;
        }

    }

    void Start()
    {
    }

    void Update()
    {
        Navegar();

        inv.SetActive(Activar_inv);

        if (Input.GetKeyDown(KeyCode.I))
        {
            Activar_inv = !Activar_inv;
        }
    }

    public void AgregarAlInventario(GameObject item)
    {
        for (int i = 0; i < Bag.Count; i++)
        {
            Image bagImage = Bag[i].GetComponent<Image>();
            if (bagImage != null && !bagImage.enabled)
            {
                bagImage.enabled = true;
                bagImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
    }
}
