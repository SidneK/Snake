using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
	[Header("Text")]
	public Text PrintScore;
	public Text PrintGameOver;
	public Text PrintPause;
	[Header("Gameplay")]
	public int Score;
	public int Record;
	public bool IsGameOver;
	public bool IsPause;
	public float Timer;
	[Header("Spawn Points")]
	public RectTransform RangeMin;
	public RectTransform RangeMax;
	public GameObject Point;

	static public Logic Instance { get; private set; }
	void Awake()
	{
		switch (ActionButton.level)
		{
			case ActionButton.Level.EASY:
				Timer = 0.35f;
				break;
			case ActionButton.Level.MEDIUM:
				Timer = 0.25f;
				break;
			case ActionButton.Level.HARD:
				Timer = 0.10f;
				break;
		}
		PrintPause.enabled = false;
		Instance = this;
		Record = PlayerPrefs.GetInt("Record");
	}
}
