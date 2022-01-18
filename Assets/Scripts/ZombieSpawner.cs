using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab; // Should be a list of zombies
    bool spawn = true;
    [SerializeField] float minSpawnDelay = 1.0f;
    [SerializeField] float maxSpawnDelay = 5.0f;

    private float counter = 0f;
    private float spawnAccelerationSpeed = 19.0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnZombie();
        }
    }

    void Update()
    {
        if (counter < spawnAccelerationSpeed)
        {
            counter += Time.deltaTime;
        }
        else if (counter >= spawnAccelerationSpeed)
        {
            counter = 0;
            if (maxSpawnDelay > 1.0f)
            {
                maxSpawnDelay -= 0.16f;
            }
        }

        Debug.Log("counter: " + counter);
    }

    public void SetSpawnBool(bool value)
    {
        spawn = value;
    }

    private void SpawnZombie()
    {
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
    }
}
