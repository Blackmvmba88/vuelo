using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float speed = 10f;
    public float lookSpeed = 2f;
    public float sprintMultiplier = 3f;
    public float smoothness = 0.1f;

    float yaw, pitch;
    Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        yaw += Input.GetAxis("Mouse X") * lookSpeed;
        pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Movement
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = transform.TransformDirection(dir);

        float multiplier = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f;
        Vector3 targetVel = move * speed * multiplier;

        velocity = Vector3.Lerp(velocity, targetVel, Time.deltaTime / smoothness);
        transform.position += velocity * Time.deltaTime;

        // Unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
