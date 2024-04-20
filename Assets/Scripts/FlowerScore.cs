using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script made by Shyanne Murdock
public class FlowerScore : MonoBehaviour
{
	public Text scoreText;
	public static int scoreCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + Mathf.Round(scoreCount);
    }
}
