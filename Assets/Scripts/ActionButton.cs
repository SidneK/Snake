using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionButton : MonoBehaviour
{
	public Text PrintDifficult; 

	public enum Level { EASY = 0, MEDIUM, HARD };
	static public Level level = Level.EASY;
	public void UseButton(string NameButton)
	{
		if (NameButton == "Pause")
		{
			Logic.Instance.IsGameOver = false;
			Logic.Instance.PrintPause.enabled = true;
			Logic.Instance.IsPause = true;
		}
		if (NameButton == "Play")
			SceneManager.LoadScene("Main");
		if (NameButton == "Options")
			SceneManager.LoadScene("Options");
		if (NameButton == "Back")
			SceneManager.LoadScene("Menu");
		if (NameButton == "Exit")
			Application.Quit();
		if (NameButton == "Easy")
		{
			PrintDifficult.text = "Difficult: " + "Easy";
			level = Level.EASY;
		}
		if (NameButton == "Medium")
		{
			PrintDifficult.text = "Difficult: " + "Medium";
			level = Level.MEDIUM;
		}
		if (NameButton == "Hard")
		{
			PrintDifficult.text = "Difficult: " + "Hard";
			level = Level.HARD;
		}
	}
}
