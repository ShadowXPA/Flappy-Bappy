using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BappyScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public float rotation = 30;
    public float velocity = 10;
    public LogicScript logic;
    public float flapCooldown = .1f;
    private float flapTimer = 0;

    void Start()
    {
        transform.Rotate(0, 0, -rotation);
    }

    void Update()
    {
        if (flapTimer > 0)
        {
            flapTimer -= Time.deltaTime;

            if (flapTimer <= 0)
            {
                flapTimer = 0;
                transform.Rotate(0, 0, -rotation * 2);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
    }

    public void Flap(InputAction.CallbackContext context)
    {
        if (!logic.gameOver && flapTimer == 0 && context.performed)
        {
            flapTimer = flapCooldown;
            myRigidbody2D.linearVelocity += Vector2.up * velocity;
            transform.Rotate(0, 0, rotation * 2);
        }
    }
}
