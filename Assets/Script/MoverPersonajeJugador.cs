using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    public CanvasManager _cManager;
    string text1 = "Tocaste al bistec.";

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
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");

        // Aplica la fuerza al Rigidbody2D
        rb.AddForce(playerInput * speed);

        Salir();
    }

    public void Salir(){
        if (Input.GetKey("p") == false)
        {
            Debug.Log("p");
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Food_Steak_01")
        {
            _cManager.pnlTextE.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                if (_cManager.pnlTextBase.activeSelf == true)
                {
                    activarYPasarTxt(text1, false);
                }
                else if (_cManager.pnlTextBase.activeSelf == false)
                {
                    activarYPasarTxt(text1, true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Food_Steak_01")
        {
            _cManager.pnlTextBase.SetActive(false);
        }
    }

    public void activarYPasarTxt(string text, bool b)
    {
        _cManager.pnlTextBase.SetActive(b);
        _cManager.textPanel.text = text;
    }
}
