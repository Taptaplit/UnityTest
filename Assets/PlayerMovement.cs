using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementCollectDisappear : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public float pickupRange = 2f;

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (jumpAction != null)
        {
            jumpAction.action.Enable();
            jumpAction.action.performed += OnSpacePressed;
        }
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (jumpAction != null)
        {
            jumpAction.action.performed -= OnSpacePressed;
            jumpAction.action.Disable();
        }
    }

    private void Update()
    {
        if (moveAction == null) return;
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }

    private void OnSpacePressed(InputAction.CallbackContext context)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        bool collected = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("PickupCube"))
            {
                Destroy(hit.gameObject);
                collected = true;
            }
        }

        if (collected) Debug.Log("Collected cube");
        else Debug.Log("No cubes nearby");
    }
}
