
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    private Vector2 moveInput;

    [SerializeField] public float speed = 5f;

    void Start()
    {
        MoveAction.Enable();
    }

    void Update()
    {
        moveInput = MoveAction.ReadValue<Vector2>();

        transform.Translate(
            Vector3.forward * Time.deltaTime * speed * moveInput.y,
            Space.World
        );

        transform.Translate(
            Vector3.right * Time.deltaTime * speed * moveInput.x,
            Space.World
        );
    }
}
