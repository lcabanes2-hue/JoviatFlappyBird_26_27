using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private GameManager gm;

    public float force = 7;
    public float maxFallSpeed = 10;
    public float rotationSpeed = 10;
    private bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        rb.simulated = false;
    }

    void Update()
    {
        bool tap = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                tap = true;
            }
        }

        if (!gm.gameStarted)
        {
            if (tap) gm.StartGame();
            return;
        }

        if (tap && !dead)
        {
            Jump();
            gm.PlaySound(gm.flapSound, 0.5f);
        }

        float angle = Mathf.Clamp(rb.linearVelocity.y * 8, -75, 30);
        Quaternion target = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(0, -maxFallSpeed);
        }
    }

    public void StartFlying()
    {
        rb.simulated = true;
        Jump();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(0, force);
    }

    public void Die()
    {
        dead = true;
        animator.SetTrigger("Die");
    }
}
