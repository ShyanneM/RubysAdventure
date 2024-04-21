using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script by Shyanne Murdock
public class SlowZone : MonoBehaviour
{
public AudioClip slowSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController >();

        if (controller != null)
        {
          
            controller.ApplySlowEffect();
			controller.PlaySound(slowSound);
        }
    }
}