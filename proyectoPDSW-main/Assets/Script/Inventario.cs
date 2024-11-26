using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public List<GameObject> BagImage = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;
    public static Inventario instance;
    public GameObject Selector;

    public int ID;

    void Start()
    {
        Singleton();
    }

    public void Singleton()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item"))
        {
            AgregarAlInventario(coll.gameObject);
        }
    }

    public void ShowImagesInInventory()
    {
        for (int i = 0; i < BagImage.Count; i++)
        {
            if (i < Bag.Count && Bag[i] != null)
            {
                Sprite itemSprite = Bag[i].GetComponent<SpriteRenderer>().sprite;
                BagImage[i].GetComponent<Image>().sprite = itemSprite;
                BagImage[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                BagImage[i].GetComponent<Image>().sprite = null;
                BagImage[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    public void Navegar()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && ID < BagImage.Count - 1)
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
        if (Input.GetKeyDown(KeyCode.DownArrow) && ID < BagImage.Count - 4)
        {
            ID += 4;
        }

        if (ID >= 0 && ID < BagImage.Count)
        {
            Selector.transform.position = BagImage[ID].transform.position;
        }
    }

    void Update()
    {
        Navegar();

        inv.SetActive(Activar_inv);

        if (Input.GetKeyDown(KeyCode.I))
        {
            Activar_inv = !Activar_inv;
        }
        ShowImagesInInventory();
    }

    public void AgregarAlInventario(GameObject item)
    {
        Bag.Add(item);

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

    // Función para buscar un sprite por nombre y mostrarlo en el inventario
    public void MostrarItemPorNombre(string nombreItem)
    {
        foreach (var item in Bag)
        {
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite.name == nombreItem)
            {
                for (int i = 0; i < BagImage.Count; i++)
                {
                    if (BagImage[i].GetComponent<Image>().sprite == null)
                    {
                        BagImage[i].GetComponent<Image>().sprite = spriteRenderer.sprite;
                        BagImage[i].GetComponent<Image>().enabled = true;
                        Debug.Log($"Mostrando el objeto: {nombreItem}");
                        return;
                    }
                }
            }
        }

        Debug.LogWarning($"El objeto con el nombre '{nombreItem}' no se encuentra en el inventario.");
    }
}

