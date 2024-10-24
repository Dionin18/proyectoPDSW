using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
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
