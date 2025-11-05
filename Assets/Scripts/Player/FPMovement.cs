using System;
//using UnityEditor.ShaderGraph.Internal;
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
    private float currentStamina = 100f;
    private float staminaRegenRate = 20f;    // スタミナの回復速度 (秒間)
    private float staminaRegenDelay = 1.5f;  // 回復が始まるまでの待機時間
    private float itemEffectDuration = 10f; // アイテムの効果時間
    public enum PlayerState
    {
        Normal,
        Dashing,
        CoolDown,
        UsedItem
    }
    public PlayerState currentState = PlayerState.Normal;
    
    //item関連
    public InventoryProd InventoryProd;
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
        //インベントリを開く
        if (Input.GetKeyDown(KeyCode.Tab) && !isOpenInventory)
        {
            Time.timeScale = 0; //時間を止める
            isOpenInventory = true;
            inventoryPanel.SetActive(true);
            InventoryProd.Reset();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpenInventory)
        {
            CloseInventory();
        }

        var currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 horizontalMovementVelocity =
            transform.TransformDirection(new Vector3(currentInput.x, 0, currentInput.y));
        horizontalMovementVelocity = horizontalMovementVelocity * movementSpeed * sprintSpeed;


        Vector3 verticalMovementVelocity = new Vector3(0, velocity.y, 0);
        if (!controller.CharacterController.isGrounded || 0f < verticalMovementVelocity.y)
        {
            verticalMovementVelocity.y += gravity * Time.deltaTime;
        }
        else
        {
            verticalMovementVelocity.y = gravity * 0.5f; //もとは0.1
        }

        velocity = horizontalMovementVelocity + verticalMovementVelocity;
        if (isOpenInventory) return; //インベントリを開いている時に処理を行わない
        controller.CharacterController.Move(velocity * Time.deltaTime);

        switch (currentState)
        {
            case PlayerState.Normal:
                if (currentStamina > 0)
                {
                    sprintGaugeImage.gameObject.SetActive(false);
                    sprintSpeed = 1;
                    if (currentStamina > 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                    {
                        currentState = PlayerState.Dashing;
                    }
                }
                break;
            case PlayerState.Dashing:
                staminaRegenDelay = 1.5f;
                sprintGaugeImage.gameObject.SetActive(true);
                sprintGaugeImage.fillAmount = currentStamina / 100;
                sprintSpeed = 2;
                currentStamina -= staminaRegenRate * Time.deltaTime;
                if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W) || currentStamina <= 0)
                {
                    currentState = PlayerState.CoolDown;
                }
                break;
            case PlayerState.CoolDown:
                sprintSpeed = 1;
                sprintGaugeImage.fillAmount = currentStamina / 100;
                if (staminaRegenDelay > 0)
                {
                    staminaRegenDelay -= Time.deltaTime;
                }
                else if (staminaRegenDelay <= 0)
                {
                    currentStamina += staminaRegenRate * Time.deltaTime; 
                    if (currentStamina >= 100)
                    {
                        currentState = PlayerState.Normal;
                    }
                }
                if (currentStamina > 0 && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                {
                    currentState = PlayerState.Dashing;
                }
                break;
            case PlayerState.UsedItem:
                sprintGaugeImage.gameObject.SetActive(true);
                sprintGaugeImage.color = Color.yellow;
                sprintSpeed = 2;
                if (currentStamina <= 100)
                {
                    currentStamina += staminaRegenRate * Time.deltaTime;
                    sprintGaugeImage.fillAmount = currentStamina / 100;
                }
                itemEffectDuration -= Time.deltaTime;
                if (itemEffectDuration <= 0)
                {
                    itemEffectDuration = 10f;
                    currentState = PlayerState.Normal;
                    sprintGaugeImage.color = Color.white;
                }
                break;
        }
    }

    public void CloseInventory()
    {   
        Time.timeScale = 1;
        isOpenInventory = false;
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("enemy"))
        {
            Invoke("Damage",0.1f);
        }
        if (other.gameObject.CompareTag("hokora"))
        {
            InventoryProd.isGool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hokora"))
        {
            InventoryProd.isGool = false;
        }
    }
    void Damage()//敵に接触したときの処理
    {
        transform.position = new Vector3(-2.3f,1.35f,4.2f);
    }
}