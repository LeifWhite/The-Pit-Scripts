using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject winLoseMenu;

	public void Start()
	{
		winLoseMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        Target[] health = new Target[players.Length];

        for(int i = 0; i < health.Length; i++)
		{
			if(health[i] != null)
            if(health[i].health <= 0)
			{
                gameOver();
			}
		}
    }

    public void gameOver()
	{
        winLoseMenu.SetActive(true);
        StartCoroutine(restartGame());
	}


    IEnumerator restartGame()
	{
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Main Scene");
	}
}
