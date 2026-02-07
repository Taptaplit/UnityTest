using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class PlayerMovementCollectDisappear : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;

    float startTime;
    string filePath;

    void Awake()
    {
        startTime = Time.time;

        filePath = Path.Combine(
            Application.persistentDataPath,
            "collision_log.txt"
        );

        File.WriteAllText(
            filePath,
            "Session Start: " + System.DateTime.Now + "\n"
        );
    }

    void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    void Update()
    {
        if (moveAction == null) return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, 0f, input.y);

        transform.Translate(
            move * speed * Time.deltaTime,
            Space.World
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PickupCube")) return;

        string id = collision.gameObject.name.Replace("Pickup_", "");

        float elapsed = Time.time - startTime;

        string line =
            "Time: " + elapsed.ToString("F2") +
            "s | ID: " + id + "\n";

        File.AppendAllText(filePath, line);
        Debug.Log(filePath);

        Destroy(collision.gameObject);
    }
}