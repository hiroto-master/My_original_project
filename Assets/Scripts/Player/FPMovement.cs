using UnityEngine;

public class FPMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float gravity = -9.81f;
    public float Gravity => gravity;

    private Vector3 velocity = Vector3.zero;
    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }
    private FirstPersonController controller;

    public GameObject inventoryPanel;
    public bool isOpenInventory = false;
    private void Start()
    {
        inventoryPanel.SetActive(false);
        isOpenInventory = false;
    }

    public void Initialize(FirstPersonController controller)
    {
        this.controller = controller;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isOpenInventory)
        {
            isOpenInventory = true;
            inventoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && isOpenInventory)
        {
            isOpenInventory = false;
            inventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        var currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 horizontalMovementVelocity = transform.TransformDirection(new Vector3(currentInput.x, 0, currentInput.y));
        horizontalMovementVelocity *= movementSpeed;

        Vector3 verticalMovementVelocity = new Vector3(0, velocity.y, 0);
        if (!controller.CharacterController.isGrounded || 0f < verticalMovementVelocity.y)
        {
            verticalMovementVelocity.y += gravity * Time.deltaTime;
        }
        else
        {
            verticalMovementVelocity.y = gravity * 0.1f;
        }
        velocity = horizontalMovementVelocity + verticalMovementVelocity;
        if(isOpenInventory)return;//ƒvƒŒƒCƒ„[‚ð“®‚©‚È‚¢‚æ‚¤‚É‚·‚é
        controller.CharacterController.Move(velocity * Time.deltaTime);

    }
}