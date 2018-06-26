using UnityEngine;
using System.Collections.Generic;

public class LogicPlayer : MonoBehaviour
{
	private float Seconds;
	private float SpawnX;
	private float SpawnY;
	private Rigidbody2D rb;
	void Start()
	{
		Seconds = 0.0f;
		rb = GetComponent<Rigidbody2D>();
		SpawnX = rb.transform.position.x;
		SpawnY = rb.transform.position.y;
		Instantiate(Logic.Instance.Point,
		new Vector3(Random.Range(Logic.Instance.RangeMin.position.x, Logic.Instance.RangeMax.position.x),
					Random.Range(Logic.Instance.RangeMin.position.y, Logic.Instance.RangeMax.position.y), 0),
					Quaternion.identity);
	}

	void Update()
	{
		if (!Logic.Instance.IsGameOver && !Logic.Instance.IsPause)
		{
			Seconds += Time.deltaTime;
			if (Seconds >= Logic.Instance.Timer)
			{
				PlayerControl.Player.MoveTail();
				PlayerControl.Player.MoveSnake();
				Seconds = 0.0f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Point")
		{
			Destroy(collision.gameObject);
			Logic.Instance.PrintScore.text = (++Logic.Instance.Score).ToString();
			Instantiate(Logic.Instance.Point,
			new Vector3(Random.Range(Logic.Instance.RangeMin.position.x, Logic.Instance.RangeMax.position.x),
					Random.Range(Logic.Instance.RangeMin.position.y, Logic.Instance.RangeMax.position.y), 0),
					Quaternion.identity);
			if (PlayerControl.Player.nTails == 0)
				PlayerControl.Player.Tails.Add(Instantiate(PlayerControl.Player.Tail, rb.transform.position, Quaternion.identity));
			else
				PlayerControl.Player.Tails.Add(Instantiate(PlayerControl.Player.Tail, 
				PlayerControl.Player.Tails[PlayerControl.Player.nTails - 1].transform.position, Quaternion.identity));
			++PlayerControl.Player.nTails;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Tail" && Logic.Instance.Score > 2
			|| collision.gameObject.tag == "Border")
		{
			RecordingScore();
			AnnulationGame();
		}
	}

	void RecordingScore()
	{
		if (Logic.Instance.Score > Logic.Instance.Record)
		{
			PlayerPrefs.SetInt("Record", Logic.Instance.Score);
			PlayerPrefs.Save();
			Logic.Instance.Record = Logic.Instance.Score;
		}
		Logic.Instance.PrintGameOver.text = "Game Over\r\nScore: " + Logic.Instance.Score
											+ "\r\nRecord: " + Logic.Instance.Record;
	}

	void AnnulationGame()
	{
		Logic.Instance.IsGameOver = true;
		foreach (GameObject it in PlayerControl.Player.Tails)
			Destroy(it.gameObject);
		PlayerControl.Player.Tails = new List<GameObject>();
		rb.transform.position = new Vector3(SpawnX, SpawnY, rb.transform.position.z);
		PlayerControl.Player.nTails = 0;
		PlayerControl.Player.Dir = PlayerControl.direction.STOP;
		Logic.Instance.Score = 0;
		Logic.Instance.PrintScore.text = "0";
	}
}
