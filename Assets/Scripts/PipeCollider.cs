using UnityEngine;

public class PipeCollider : MonoBehaviour
{
    private GameObject gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.GetComponent<GameManager>().StopGame();
        }
    }
}
