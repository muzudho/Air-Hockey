using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    [SerializeField] private string horizontalAxisName = "Horizontal";
    [SerializeField] private string verticalAxisName = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis(horizontalAxisName);
        //transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        float verticalInput = Input.GetAxis(verticalAxisName);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }
}
