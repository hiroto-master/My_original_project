using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        FPCameraLook cameraLook = GetComponent<FPCameraLook>();
        cameraLook?.Initialize(this);//カメラルックがあれば実行される

        FPMovement movement = GetComponent<FPMovement>();
        movement?.Initialize(this);
    }
}