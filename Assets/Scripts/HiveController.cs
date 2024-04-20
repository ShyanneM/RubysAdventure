using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hive Controller written by AG
public class HiveController : MonoBehaviour
{
    public int maxHealth = 2;
    public float shootInterval = 3f;
    public GameObject beePrefab;
    public Transform playerTransform; // Reference to the player's transform (basically location)

    private int currentHealth;
    private bool isBroken;

    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(ShootBees());
    }

    IEnumerator ShootBees()
    {
        while (!isBroken)
        {
            //shoot 1 or 2 bees
            int numBeesToShoot = Random.Range(1, 3);

            for (int i = 0; i < numBeesToShoot; i++)
             {
                Vector2 spawnOffset = Random.insideUnitCircle * 0.5f; //bees spawning on eachother, made random spawns

                GameObject bee = Instantiate(beePrefab, transform.position + (Vector3)spawnOffset, Quaternion.identity);
                BeeController beeController = bee.GetComponent<BeeController>();
                beeController.SetTarget(playerTransform); 
            }

            yield return new WaitForSeconds(shootInterval);
        }
    }

    public void TakeHit()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            isBroken = true;
            Destroy(gameObject);
            
        }
    }
}