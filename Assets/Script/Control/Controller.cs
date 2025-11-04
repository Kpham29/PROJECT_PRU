using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;
    private Rigidbody2D myBody;
    private Animator animator;
    private bool grounded;
    private bool faceInRight;
    private bool isRunning;
    private MainCharacterStat characterStat;
    private Vector3 initialSpawnPoint;
    private Quaternion initialSpawnRotation;
    private Vector3 respawnPosition;
    private Quaternion respawnRotation;
    private bool hasCheckpoint = false;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        characterStat = GetComponent<MainCharacterStat>();
        faceInRight = true;
        grounded = true;
        isRunning = false;
        initialSpawnPoint = transform.position;
        initialSpawnRotation = transform.rotation;
        respawnPosition = initialSpawnPoint;
        respawnRotation = initialSpawnRotation;
        FollowObject.SetTarget(gameObject); 
        Debug.Log("Controller: Set target to " + gameObject.name);
    }

    void FixedUpdate()
    {
        if (characterStat.isDead) return;
        float move = Input.GetAxis("Horizontal");
        float speed = isRunning ? runSpeed : moveSpeed;
        myBody.velocity = new Vector2(move * speed, myBody.velocity.y);

        if (!faceInRight && move > 0)
        {
            Flip();
        }
        else if (faceInRight && move < 0)
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("IsRunning", isRunning);
    }

    void Update()
    {
        if (characterStat.isDead)
        {
            StartCoroutine(characterStat.WaitForDeathAnimationAndRespawn());
            return;
        }
        animator.SetBool("IsGrounded", grounded);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerJump();
        }
        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }
        if (Input.GetButtonDown("Fight"))
        {
            animator.SetTrigger("Fight");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerAttack();
        }
        else if (Input.GetButtonDown("Specialskill"))
        {
            animator.SetTrigger("Specialskill");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerAttack();
        }
        else if (Input.GetButtonDown("Shield"))
        {
            animator.SetTrigger("Shield");
        }
    }

    private void Flip()
    {
        faceInRight = !faceInRight;
        Vector3 vector = transform.localScale;
        vector.x *= -1;
        transform.localScale = vector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    grounded = true;
                    if (AudioManager.Instance != null && myBody.velocity.y < -2f)
                        AudioManager.Instance.PlayPlayerLand();
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            characterStat.TakeDamage(20);
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayTrapActivate();
        }
    }

    public void SetCheckpoint(Vector3 position, Quaternion rotation)
    {
        respawnPosition = position;
        respawnRotation = rotation;
        hasCheckpoint = true;
        Debug.Log($"Checkpoint saved at {position}");
    }

    public void SetInitialSpawnPoint(Vector3 position, Quaternion rotation)
    {
        initialSpawnPoint = position;
        initialSpawnRotation = rotation;
        respawnPosition = initialSpawnPoint;
        respawnRotation = initialSpawnRotation;
        hasCheckpoint = false;
        Debug.Log($"Initial spawn point set at {position}");
    }

    public IEnumerator Respawn()
    {
        float length = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);
        transform.position = hasCheckpoint ? respawnPosition : initialSpawnPoint;
        transform.rotation = hasCheckpoint ? respawnRotation : initialSpawnRotation;
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        characterStat.RestoreHealth();
        grounded = true;
        isRunning = false;
        animator.SetBool("IsGrounded", grounded);
        animator.SetFloat("Speed", 0);
        animator.SetBool("IsRunning", false);
        if (!faceInRight)
        {
            Flip();
        }
        FollowObject.SetTarget(gameObject); // Cập nhật target sau respawn
        Debug.Log($"Player respawned at {(hasCheckpoint ? "checkpoint" : "initial spawn point")}. Target updated to " + gameObject.name);
    }

    public void StopImmediately()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetFloat("Speed", 0f);
        }
    }
}