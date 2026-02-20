using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    [Header("Camera")]
    public Camera mainCamera;

    private CharacterController cc;
    private float xRotation = 0f;
    private NPC currentNearbyNPC;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movement();
        CameraRotation();
        CheckNearbyNPC();
        NPCInteractionInput();
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.right * h + transform.forward * v;
        cc.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void CheckNearbyNPC()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        currentNearbyNPC = null;
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out NPC npc))
            {
                currentNearbyNPC = npc;
                break;
            }
        }
    }

    private void NPCInteractionInput()
    {
        if (currentNearbyNPC == null) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            currentNearbyNPC.InteractTalk();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyNPC.InteractAct();
        }
    }
}