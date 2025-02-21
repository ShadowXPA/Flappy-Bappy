using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float speed = 2;
    public float deadZone = -15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
