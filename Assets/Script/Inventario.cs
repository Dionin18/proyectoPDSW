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
    public Animator animator;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject WinSprite;
    [SerializeField] private GameObject firework1;
    [SerializeField] private GameObject firework2;
    [SerializeField] private int count;
    [SerializeField] private int CantidadParaGanar;


    public int ID;

    void Start()
    {
        count = 0;
        Singleton();

        if (Bag == null)
        {
            Bag = new List<GameObject>();
            Debug.LogWarning("Bag estaba vacío, inicializado nuevamente.");
        }

        animator = player != null ? player.GetComponent<Animator>() : null;

        if (firework1 == null)
        {
            firework1 = GameObject.Find("SF_Rainbow");
        }

        if (firework2 == null)
        {
            firework2 = GameObject.Find("SF_Rainbow_(1)");
        }
    }


    public void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    void OnTriggerEnter(Collider coll)
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

        if (count == CantidadParaGanar)
        {
            activar();
        }
    }

    public void AgregarAlInventario(GameObject item)
    {
        if (Bag == null)
        {
            Debug.LogError("La variable 'Bag' no está asignada en el script Inventario.");
            return;
        }

        Bag.Add(item);
        count++;
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
    public void activar()
    {
        if (firework1 != null)
        {
            firework1.SetActive(true);
        }
        else
        {
            Debug.LogWarning("firework1 ha sido destruido o no está asignado.");
        }

        if (firework2 != null)
        {
            firework2.SetActive(true);
        }
        else
        {
            Debug.LogWarning("firework2 ha sido destruido o no está asignado.");
        }

        if (WinSprite != null)
        {
            WinSprite.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WinSprite no está asignado.");
        }

        if (animator != null)
        {
            animator.Play("GrenadeThrow");
        }
        else
        {
            Debug.LogWarning("Animator no está asignado.");
        }

        if (animator != null)
        {
            animator.Play("GrenadeThrow");
        }
        else
        {
            Debug.LogWarning("Animator no está asignado o ha sido destruido.");
        }
    }
}

