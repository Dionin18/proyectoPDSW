using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    public Rigidbody rb;
    public Animator animator;
    public int numCarrotSeed = 0;
    public float speed;
    public Vector3 playerInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        // Captura la entrada en los ejes horizontal y vertical
        if (!Inventario.instance.Activar_inv)
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
        }
        /*
        if (Input.GetKey(KeyCode.W))
        {
            playerInput.z = Input.GetAxis("Vertical");
            
        }
        else
        {
            playerInput.z = 0;
        }    
        if (Input.GetKey(KeyCode.S))
        {
            playerInput.z = Input.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerInput.x = Input.GetAxis("Horizontal");
        }
        else
        {
            playerInput.x = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerInput.x = Input.GetAxis("Horizontal");
        }*/


        // Aplica la fuerza al Rigidbody2D
        rb.AddForce(playerInput * speed);
        Quaternion lookDirection = Quaternion.LookRotation(playerInput.normalized);

        if (playerInput != Vector3.zero )
        {
            animator.SetFloat("Speed_f",0.3f);
            rb.rotation = lookDirection;
        }
        else if(playerInput==Vector3.zero)
        {
            animator.SetFloat("Speed_f", 0f);
            
        }
        Salir();
    }

    public void Salir(){
        if (Input.GetKey("p") == false)
        {
            try
            {
                Application.Quit();
            }
            catch { }
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    void IDataPersistence.LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    void IDataPersistence.SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }

}
