using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Lumin;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    // fields
    [SerializeField] private float m_moveSpeed = 7f;
    [SerializeField] private GameInput m_gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform m_kitchenObjectHoldPoint;

    private bool m_isPlayerWalking = false;
    private Vector3 m_lastInteractionDirection;
    private BaseCounter m_selectedCounter;
    private KitchenObject m_kitchenObject;

    // event
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter m_selectedCounter;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There is more than one player!");
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // if event is fired, invoke this interaction handler
        m_gameInput.OnInteractAction += gameInput_OnInteractAction;
    }

    private void gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // if there exists a selected counter, call its interact function
        m_selectedCounter?.Interact(this);
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    void HandleMovement()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();

        // mapping our input unit vector into Vector3 unit vector (the 3d world space)
        // this keeps the input vector logic separate from our world space movement logic
        // will help implementing gamepad
        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        float moveDistance = m_moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        // check for collision
        // cast a ray from player toward the direction it is moving; ray length = playerSize
        // if ray hits something return false else true
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Attempt to move in the x direction
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
                moveDirection = moveDirectionX;
            else
            {
                // Attempt to move in the z direction
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                    moveDirection = moveDirectionZ;
                // else
                // can't move in any direction
            }
        }

        if (canMove)
            transform.position += moveDirection * moveDistance;

        // is player walking
        m_isPlayerWalking = moveDirection != Vector3.zero;

        // slerp (rotation: circular way) the player towards the direction of movement
        float rotationSpeed = 8.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

        // Debug.Log(inputVector);
    }

    void HandleInteractions()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        // save the last movement direction when player was not stationary
        // because when the player isn't moving, inputVector will be zero so will be the moveDirection.
        if (moveDirection != Vector3.zero)
            m_lastInteractionDirection = moveDirection;

        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, m_lastInteractionDirection, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (m_selectedCounter != baseCounter)
                    SetSelectedCounter(baseCounter);

            }
            else
            {
                // if something that doesn't has the clearCounter script is in front of the player
                SetSelectedCounter(null);
            }
        }
        else
        {
            // if nothing is in front of the player
            SetSelectedCounter(null);
        }

    }

    // helping funcitons
    public bool IsPlayerWalking() { return m_isPlayerWalking; }
    
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        m_selectedCounter = selectedCounter;

        // fire an event, each time when a counter is selected, for the change of visuals
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            m_selectedCounter = m_selectedCounter
        });
    }

    public void SetKitchenObject(KitchenObject p_kitchenObject)
    {
        m_kitchenObject = p_kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return m_kitchenObject;
    }

    public void ClearKitchenObject()
    {
        m_kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return m_kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_kitchenObjectHoldPoint;
    }
}
