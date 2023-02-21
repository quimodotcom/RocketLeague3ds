using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalManager : MonoBehaviour {

	public int redGoals, blueGoals = 0;

	public TMP_Text scoreText;

	private GameObject ball;
	private GameObject[] players;

    private List<Vector3> playerPos = new List<Vector3>();

	public Team team;

	private void Awake()
	{
        UpdateScore();

		players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
		{
			playerPos.Add(players[i].transform.position);
		}
    }

	public void UpdateScore()
	{
        scoreText.text = @"<color=""red"">" + redGoals + @" <color=""white"">: " + @"<color=""blue"">" + blueGoals;
    }

	public void ResetGame()
	{
		ball.transform.position = Vector3.zero;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playerPos[i];
        }

        GetComponent<BoxCollider>().enabled = true;
    }

	public enum Team
	{
		None,
		Red,
		Blue
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.CompareTag("Ball"))
		{
			ball = col.gameObject;

			switch(team)
			{
				case Team.None:
					break;
				case Team.Blue:
					redGoals++;
					ChangeAllScores();
					Invoke("UpdateScore", 1.5f);
                    Invoke("ResetGame", 5f);

					GetComponent<BoxCollider>().enabled = false;
                    break;
				case Team.Red:
					blueGoals++;
					ChangeAllScores();
                    Invoke("UpdateScore", 1.5f);
                    Invoke("ResetGame", 5f);

                    GetComponent<BoxCollider>().enabled = false;
                    break;
			}
		}
        PlayerPrefs.Save();
		return;
    }

	public void ChangeAllScores()
	{
		GoalManager[] gm = FindObjectsOfType<GoalManager>();

		foreach(GoalManager g in gm)
		{
			g.redGoals = redGoals;
			g.blueGoals = blueGoals;
		}
    }
}
