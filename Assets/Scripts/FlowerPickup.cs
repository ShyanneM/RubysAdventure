
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script made by Shyanne Murdock
public class FlowerPickup : MonoBehaviour
{
    public AudioClip collectedClip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            
            
                
                Destroy(gameObject);
            	FlowerScore.scoreCount += 1;
                controller.PlaySound(collectedClip);
            
        }

    }
}