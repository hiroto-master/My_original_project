using UnityEngine;

public class FPCameraLook : MonoBehaviour
{
    private FirstPersonController controller;
    private Transform cameraRoot;
    public Camera playerCamera;
    public FPMovement FPMovement;

    [Header("Look")]
    [SerializeField, Range(1, 10)] private float lookSpeed = 2.0f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 50.0f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 50.0f;

    public void Initialize(FirstPersonController controller)
    {
        this.controller = controller;
        cameraRoot = playerCamera.transform.parent.transform;
    }

    private void Update()
    {
        if (FPMovement.isOpenInventory) return;
        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 euler = transform.eulerAngles;

        euler.y += delta.x * lookSpeed;
        transform.eulerAngles = euler;

        float cameraEulerX = cameraRoot.localEulerAngles.x;
        cameraEulerX -= delta.y * lookSpeed;
        cameraEulerX = 180 < cameraEulerX ? cameraEulerX - 360 : cameraEulerX;
        cameraEulerX = Mathf.Clamp(cameraEulerX, -upperLookLimit, lowerLookLimit);
        cameraRoot.localRotation = Quaternion.Euler(cameraEulerX, 0, 0);
    }
}