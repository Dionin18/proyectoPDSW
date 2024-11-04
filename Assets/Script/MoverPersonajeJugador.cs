using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    public int numCarrotSeed = 0;
    public float speed;
    public Rigidbody rb;
    public Vector3 playerInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // Captura la entrada en los ejes horizontal y vertical
        
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
        }


        // Aplica la fuerza al Rigidbody2D
        rb.AddForce(playerInput * speed);

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
