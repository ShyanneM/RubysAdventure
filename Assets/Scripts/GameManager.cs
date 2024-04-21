using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image youWinText;
    public Image youLostText;
    public GameObject playerObject; 
    bool gameEnded = false;

    void Start()
    {
        
        youWinText.gameObject.SetActive(false);
        youLostText.gameObject.SetActive(false);
    }

    public void CheckWinCondition()
    {
        
        if (!GameObject.FindGameObjectWithTag("Enemy") && !gameEnded)
        {
            EndGame(true);
        }
    }

    public void PlayerLost()
    {
        EndGame(false);
    }

    public void EndGame(bool win) 
    {
        gameEnded = true;

        if (win)
        {
            youWinText.gameObject.SetActive(true);

        }
        else
        {
            youLostText.gameObject.SetActive(true);

            playerObject.GetComponent<RubyController>().enabled = false; 
        }
    }
	
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
			FlowerScore.scoreCount = 0;
        }
		
    }
}