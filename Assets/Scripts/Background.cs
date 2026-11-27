using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = 0.5f;
    private float width;
    private GameManager gm;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;

        width = GetComponent<SpriteRenderer>().bounds.size.x - 0.02f;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gm.gameOver) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -width)
        {
            transform.position += Vector3.right * width * 2;
        }
    }
}
