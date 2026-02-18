using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runMultiplier = 1.5f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 100f;
    public float maxLookX = 80f;
    public float minLookX = -80f;

    public float interactDistance = 2f;
    public LayerMask interactLayer;

    public GameObject burgerHeldUI;
    public GameObject pizzaHeldUI;
    public GameObject fortuneCookieHeldUI;

    private CharacterController _cc;
    private Camera _playerCam;
    private float _xRot;
    private Vector3 _velocity;

    private bool _isHoldingBurger = false;
    private bool _isHoldingPizza = false;
    private bool _isHoldingCookie = false;

    private MonoBehaviour _currentInteractTarget;

    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _cc = GetComponent<CharacterController>();
        _playerCam = GetComponentInChildren<Camera>();
        if (_playerCam == null) Debug.LogError("No Camera attached to Player child object!");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InitHeldUI();
    }

    private void Update()
    {
        if (PauseMenu.IsPaused) return;

        PlayerMovement();
        PlayerLook();
        CheckInteractTarget();
        PlayerInteractInput();
        UpdateHeldUI();
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.right * x + transform.forward * z;
        moveDir.Normalize();

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * runMultiplier : moveSpeed;
        _cc.Move(moveDir * currentSpeed * Time.deltaTime);

        if (_cc.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _cc.Move(_velocity * Time.deltaTime);
    }

    private void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRot -= mouseY;
        _xRot = Mathf.Clamp(_xRot, minLookX, maxLookX);
        _playerCam.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void CheckInteractTarget()
    {
        _currentInteractTarget = null;

        Ray ray = _playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            _currentInteractTarget = hit.collider.GetComponent<MonoBehaviour>();
            
            if (_currentInteractTarget != null)
            {
                Debug.Log("Interactable target detected: " + hit.collider.name);
            }
        }
    }

    private void PlayerInteractInput()
    {
        if (_currentInteractTarget == null) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_currentInteractTarget is NPCBase)
            {
                NPCBase npc = (NPCBase)_currentInteractTarget;
                npc.TriggerDialogue();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentInteractTarget is FoodTable && !_isHoldingBurger && !_isHoldingPizza)
            {
                FoodTable foodTable = (FoodTable)_currentInteractTarget;
                if (foodTable.HasBurger)
                {
                    _isHoldingBurger = true;
                    foodTable.TakeBurger();
                }
                else if (foodTable.HasPizza)
                {
                    _isHoldingPizza = true;
                    foodTable.TakePizza();
                }
            }
            else if (_currentInteractTarget is FortuneCookieTable && !_isHoldingCookie)
            {
                FortuneCookieTable cookieTable = (FortuneCookieTable)_currentInteractTarget;
                if (cookieTable.HasCookie)
                {
                    _isHoldingCookie = true;
                    cookieTable.TakeCookie();
                }
            }
            else if (_currentInteractTarget is NPCBase)
            {
                NPCBase npc = (NPCBase)_currentInteractTarget;
                if (_isHoldingBurger && npc.WantsBurger)
                {
                    npc.ReceiveBurger();
                    _isHoldingBurger = false;
                }
                else if (_isHoldingPizza && npc.WantsPizza)
                {
                    npc.ReceivePizza();
                    _isHoldingPizza = false;
                }
                else if (_isHoldingCookie && npc.WantsCookie)
                {
                    npc.ReceiveCookie();
                    _isHoldingCookie = false;
                }
            }
        }
    }

    private void InitHeldUI()
    {
        if (burgerHeldUI != null) burgerHeldUI.SetActive(false);
        if (pizzaHeldUI != null) pizzaHeldUI.SetActive(false);
        if (fortuneCookieHeldUI != null) fortuneCookieHeldUI.SetActive(false);
    }

    private void UpdateHeldUI()
    {
        if (burgerHeldUI != null) burgerHeldUI.SetActive(_isHoldingBurger);
        if (pizzaHeldUI != null) pizzaHeldUI.SetActive(_isHoldingPizza);
        if (fortuneCookieHeldUI != null) fortuneCookieHeldUI.SetActive(_isHoldingCookie);
    }

    public bool IsHoldingAnyItem()
    {
        return _isHoldingBurger || _isHoldingPizza || _isHoldingCookie;
    }

    public void DropAllItems()
    {
        _isHoldingBurger = false;
        _isHoldingPizza = false;
        _isHoldingCookie = false;
        UpdateHeldUI();
    }
}