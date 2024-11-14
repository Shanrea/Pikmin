using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Vitesse de déplacement
    public float turnSpeed = 100.0f; // Vitesse de rotation
    public float verticalLookSpeed = 2.0f; // Vitesse de rotation verticale
    public float minVerticalAngle = -45.0f; // Angle de rotation verticale minimal
    public float maxVerticalAngle = 45.0f; // Angle de rotation verticale maximal

    private Rigidbody rb;
    private Transform cameraTransform;
    private float verticalLookRotation = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // Déplacement avec les touches WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        // Rotation horizontale avec la souris
        float turnHorizontal = Input.GetAxis("Mouse X");
        Vector3 rotationHorizontal = new Vector3(0.0f, turnHorizontal, 0.0f) * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationHorizontal));

        // Rotation verticale avec la souris
        float turnVertical = Input.GetAxis("Mouse Y");
        verticalLookRotation -= turnVertical * verticalLookSpeed;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle); // Limiter l'angle de rotation

        cameraTransform.localEulerAngles = new Vector3(verticalLookRotation, 0.0f, 0.0f);
    }
}
