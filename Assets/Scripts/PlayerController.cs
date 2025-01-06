using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1f, 10f)]
    public float playerSpeed = 5.0f;
    [Range(100f, 1000f)]
    public float mouseSensitivity = 500f;

    private CharacterController controller;
    private float xRotation = 0f;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        // Lock the cursor to the game window and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Convert movement to world space relative to player direction
        Vector3 worldMove = transform.TransformDirection(move) * playerSpeed * Time.deltaTime;

        // Ensure Y position remains constant
        worldMove.y = 0;

        // Move the player
        controller.Move(worldMove);

        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player object horizontally (Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the player object vertically (X-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit the up and down view
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0f);
    }
}
