using UnityEngine;

public class Border : MonoBehaviour
{
    public GameManager gm;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.StopGame();
        }
    }
}
