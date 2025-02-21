using UnityEngine;

public class BorderScript : MonoBehaviour
{
    public LogicScript logic;
    private bool triggered;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && !triggered)
        {
            triggered = true;
            logic.GameOver();
        }
    }
}
