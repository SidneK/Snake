using UnityEngine;
using UnityEngine.UI;

public class FlashLabel : MonoBehaviour
{
	public string NameButton;
	private float Seconds = 0.0f;

	void Update()
	{
		if (NameButton == "GameOver")
			RelayText(Logic.Instance.PrintGameOver, Logic.Instance.IsGameOver);
		if (NameButton == "Pause")
			RelayText(Logic.Instance.PrintPause, Logic.Instance.IsPause);
	}

	void RelayText(Text txt, bool Is)
	{
		if (Is)
		{
			Seconds += Time.deltaTime;
			if (Seconds >= 0.5f)
				txt.enabled = true;
			if (Seconds >= 1.0f)
			{
				txt.enabled = false;
				Seconds = 0.0f;
			}
		}
		else
			txt.enabled = false;
	}
}
