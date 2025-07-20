using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class FPMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float gravity = -9.81f;
    public float Gravity => gravity;

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }
    private FirstPersonController controller;
    private Vector3 velocity = Vector3.zero;
    
    public GameObject inventoryPanel;
    public bool isOpenInventory = false;
    
    private float sprintSpeed = 1;
    
    public Image sprintGaugeImage;
    
    private float sprintTime = 5;//ダッシュすることのできる時間
    private bool isCountSprintTime = false;//スプリントタイムを減らすか減らさないか
    private float sprintInterval = 2;　//回復すまでの待機時間
    private float deactiveGaugeTime = 1;
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
        horizontalMovementVelocity = horizontalMovementVelocity * movementSpeed * sprintSpeed;
        

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
        if(isOpenInventory)return;//インベントリを開いている時に処理を行わない
        controller.CharacterController.Move(velocity * Time.deltaTime);

        //sprintを変更する処理
        if (Input.GetKeyDown(KeyCode.LeftShift) && sprintTime > 0)
        {
            sprintSpeed = 2;
            isCountSprintTime = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || sprintTime <= 0)
        {
            sprintSpeed = 1;
            isCountSprintTime = false;
        }
        //スプリントタイムを減らす処理
        if (isCountSprintTime == true)
        {
            sprintGaugeImage.gameObject.SetActive(true);
            sprintTime -= Time.deltaTime; 
            sprintGaugeImage.fillAmount = sprintTime/5;
            sprintInterval = 2;
            deactiveGaugeTime = 1;
        }
        else if (isCountSprintTime == false)//スプリントの回復
        {
            if (sprintInterval >= 0)//sprintIntervalが減りすぎないようにするため
            {
                sprintInterval -= Time.deltaTime;
            } 
            if (sprintInterval <= 0)
            {
                if (sprintTime <= 5)
                {
                    sprintTime += Time.deltaTime;
                    sprintGaugeImage.fillAmount = sprintTime/5;
                }

            }
        }
        //体力ゲージの表示、非表示
        if (sprintTime >= 5)
        {
            if (deactiveGaugeTime > 0)
            {
                deactiveGaugeTime -= Time.deltaTime;
            }

            if (deactiveGaugeTime <= 0)
            {
                sprintGaugeImage.gameObject.SetActive(false);
            }
        }      
        Debug.Log(deactiveGaugeTime);
    }
}