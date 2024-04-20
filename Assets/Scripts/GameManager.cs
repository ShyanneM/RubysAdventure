using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image youWinText;
    public Image youLostText;
    public GameObject playerObject; // Reference to the player object to disable movement
    bool gameEnded = false;

    void Start()
    {
        // Disable win and lose text at start
        youWinText.gameObject.SetActive(false);
        youLostText.gameObject.SetActive(false);
    }

    public void CheckWinCondition()
    {
        // all robots are fixed when there are no GameObjects tagged "Enemy" in the scene
        if (!GameObject.FindGameObjectWithTag("Enemy") && !gameEnded)
        {
            EndGame(true);
        }
    }

    public void PlayerLost()
    {
        EndGame(false);
    }

    public void EndGame(bool win) //made public so Enemy Controller can access
    {
        gameEnded = true;

        if (win)
        {
            youWinText.gameObject.SetActive(true);

        }
        else
        {
            youLostText.gameObject.SetActive(true);

            playerObject.GetComponent<RubyController>().enabled = false; // Disable player movement
        }
    }
	
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the game
			FlowerScore.scoreCount = 0;
        }
		
    }
}