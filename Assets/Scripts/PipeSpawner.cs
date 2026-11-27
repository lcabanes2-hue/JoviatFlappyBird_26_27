using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float timeToSpawn = 1.8f;
    public GameObject pipePrefab;
    public Transform spawnTop;
    public Transform spawnBottom;

    public void StartSpawning()
    {
        InvokeRepeating("CreateNewPipe", 0, timeToSpawn);
    }

    public void StopSpawning()
    {
        CancelInvoke("CreateNewPipe");
    }

    void CreateNewPipe()
    {
        float randomY = Random.Range(spawnBottom.position.y, spawnTop.position.y);
        Vector3 newPos = new Vector3(spawnTop.position.x, randomY, spawnTop.position.z);
        GameObject o = Instantiate(pipePrefab, newPos, Quaternion.identity);
        o.transform.parent = transform;
    }
}
