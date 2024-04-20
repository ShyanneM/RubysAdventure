using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Bee Controller written by AG
public class BeeController : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    private Transform target; 

    void Update()
    {
        if (target != null)
        {
            //find ruby
            Vector2 direction = (target.position - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

            // update position of bee
            transform.position = newPosition;
        }
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController >();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }

        Destroy(gameObject);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}