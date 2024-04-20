using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        //rules for projectile written by AG
        BeeController bee = other.collider.GetComponent<BeeController>();
        if (bee != null)
        {
            Destroy(bee.gameObject); 
            Destroy(gameObject); 
            return;
        }


        HiveController hive = other.collider.GetComponent<HiveController>();
        if (hive != null)
        {
            hive.TakeHit(); 
            Destroy(gameObject); 
            return;
        }
		EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
    
        Destroy(gameObject);
    }
}
