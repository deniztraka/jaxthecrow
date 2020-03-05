using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AscendingStatusChanged : UnityEvent<bool>
{
}

[System.Serializable]
public class OnLandEvent : UnityEvent<bool>
{
}

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement    
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    private bool isAscending;
    public bool IsAscending
    {
        get { return isAscending; }
        set
        {
            if (value != isAscending && AscendingStatusChanged != null)
            {
                AscendingStatusChanged.Invoke(m_Rigidbody2D.velocity.y > 0);
            }
            isAscending = value;
        }
    }

    public Animator animator;
    private PlayerSoundEffectsManager soundEffectsManager;


    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public OnLandEvent OnLandEvent;

    public AscendingStatusChanged AscendingStatusChanged;

    public UnityEvent OnJumpEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        soundEffectsManager = GetComponent<PlayerSoundEffectsManager>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new OnLandEvent();
        }

        if (OnJumpEvent == null)
        {
            OnJumpEvent = new UnityEvent();
        }

        if (OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }

        if (AscendingStatusChanged == null)
        {
            AscendingStatusChanged = new AscendingStatusChanged();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                // if (!wasGrounded)
                // {
                //     soundEffectsManager.Play("land", false);
                // }
                // OnLandEvent.Invoke(false);
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (jump)
        {
            OnJumpEvent.Invoke();

            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        //Debug.Log(m_Rigidbody2D.velocity);
        if (m_Rigidbody2D.velocity.y > 0)
        {
            IsAscending = true;
        }
        else
        {
            IsAscending = false;
        }

        var horizontalVelocity = m_Rigidbody2D.velocity.x;

        if (m_Grounded && (horizontalVelocity < -0.5 || horizontalVelocity > 0.5))
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsFlying", false);
            if (soundEffectsManager != null)
            {
                soundEffectsManager.Play("movement", true);
            }
            OnLandEvent.Invoke(true);
            //Debug.Log("onLand");
        }
        else

        if (!m_Grounded)
        {
            animator.SetBool("IsFlying", true);
            animator.SetBool("IsWalking", false);
            if (soundEffectsManager != null)
            {
                soundEffectsManager.Play("flying", true);
            }
            OnLandEvent.Invoke(false);
            //Debug.Log("flying");
        }
        else

        if (m_Grounded && (horizontalVelocity >= -0.5 && horizontalVelocity <= 0.5))
        {
            animator.SetBool("IsFlying", false);
            animator.SetBool("IsWalking", false);
            if (soundEffectsManager != null)
            {
                soundEffectsManager.Play("idle", false);
            }
            var rnd = Random.Range(0, 1000);
            if (rnd > 995)
            {
                //Debug.Log(rnd);
                animator.SetTrigger("Eyes");
            }

            var rnd2 = Random.Range(0, 1000);
            if (rnd > 995)
            {
                //Debug.Log(rnd);
                if (soundEffectsManager != null)
                {
                    soundEffectsManager.Play("shout", false);
                    animator.SetTrigger("Shout");
                }
            }

            OnLandEvent.Invoke(true);
        }

        //Debug.Log(horizontalVelocity);
    }

    public void SetJumpPower(float jumpForce)
    {
        this.m_JumpForce = jumpForce;
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool IsFacingRight()
    {
        return m_FacingRight;
    }
}