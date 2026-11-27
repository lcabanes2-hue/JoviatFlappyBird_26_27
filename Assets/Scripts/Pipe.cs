using UnityEngine;

public class Pipe : MonoBehaviour
{
    private GameObject gm;
    public float speed = 2.5f;
    public float destroyX = -6f;
    private bool scored = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        if (gm.GetComponent<GameManager>().gameOver) return;

        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !scored)
        {
            scored = true;
            gm.GetComponent<GameManager>().AddScore(1);
        }
    }
}
