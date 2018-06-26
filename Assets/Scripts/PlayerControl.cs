using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	[Header("Move Gameplay")]
	public GameObject Head;
	public GameObject Tail;
	public float Speed;
	public enum direction { STOP = 0, LEFT, RIGHT, UP, DOWN };
	public direction Dir;
	[HideInInspector]
	public int nTails;
	[HideInInspector]
	public List<GameObject> Tails;

	static public PlayerControl Player { get; private set; } 
	void Awake()
	{
		Player = this;
	}

	private Rigidbody2D rb;
	void Start()
	{
		Tails = new List<GameObject>();
		rb = Head.GetComponent<Rigidbody2D>();
	}

	public void MoveSnake()
	{
		switch (Dir)
		{
			case direction.UP:
				rb.transform.position = new Vector3(rb.transform.position.x,
											rb.transform.position.y + Speed, rb.transform.position.z);
				break;
			case direction.DOWN:
				rb.transform.position = new Vector3(rb.transform.position.x,
											rb.transform.position.y - Speed, rb.transform.position.z);
				break;
			case direction.LEFT:
				rb.transform.position = new Vector3(rb.transform.position.x - Speed,
											rb.transform.position.y, rb.transform.position.z);
				break;
			case direction.RIGHT:
				rb.transform.position = new Vector3(rb.transform.position.x + Speed,
											rb.transform.position.y, rb.transform.position.z);
				break;
		}
	}

	public void MoveTail()
	{
		if (nTails != 0)
		{
			Vector3 pref = Tails[0].transform.position;
			Tails[0].transform.position = Head.transform.position;
			for (int i = 1; i < nTails; ++i)
			{
				Vector3 pref2 = Tails[i].transform.position;
				Tails[i].transform.position = pref;
				pref = pref2;
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Logic.Instance.IsGameOver == true)
			Logic.Instance.IsGameOver = false;
		if (Logic.Instance.IsPause == true)
		{
			Logic.Instance.PrintPause.enabled = false;
			Logic.Instance.IsPause = false;
		}
		if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
		{
			if (eventData.delta.x > 0 && Dir != direction.LEFT)
				Dir = direction.RIGHT;
			else
				Dir = direction.LEFT;
		}
		else
		{
			if (eventData.delta.y > 0 && Dir != direction.DOWN)
				Dir = direction.UP;
			else
				Dir = direction.DOWN;
		}
	}

	public void OnDrag(PointerEventData eventData) {}
}
