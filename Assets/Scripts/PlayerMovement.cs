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

        // Réinitialiser la rotation de la caméra pour éviter une rotation involontaire au lancement
        cameraTransform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        // Déplacement avec les touches WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Direction de déplacement basée sur la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Empêcher le mouvement vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveVertical + right * moveHorizontal;

        rb.MovePosition(transform.position + desiredMoveDirection * moveSpeed * Time.deltaTime);

        // Rotation horizontale avec la souris
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            float turnHorizontal = Input.GetAxis("Mouse X");
            Vector3 rotationHorizontal = new Vector3(0.0f, turnHorizontal, 0.0f) * turnSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationHorizontal));

            // Rotation verticale avec la souris
            float turnVertical = Input.GetAxis("Mouse Y");
            verticalLookRotation -= turnVertical * verticalLookSpeed;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle); // Limiter l'angle de rotation

            cameraTransform.localEulerAngles = new Vector3(verticalLookRotation, 0.0f, 0.0f);
        }

        // Rotation de la caméra autour du joueur avec SHIFT enfoncé
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            float orbitHorizontal = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
            float orbitVertical = -Input.GetAxis("Mouse Y") * verticalLookSpeed * Time.deltaTime;

            cameraTransform.RotateAround(transform.position, Vector3.up, orbitHorizontal);
            cameraTransform.RotateAround(transform.position, cameraTransform.right, orbitVertical);

            verticalLookRotation -= orbitVertical;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle);
        }
    }
}
