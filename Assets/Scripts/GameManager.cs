using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public UIHandler ui;
	[HideInInspector] public int MyPoints;
	
	public List<ShapeContainer> Container = new List<ShapeContainer>();
	public List<Sprite> Shapes = new List<Sprite>();
	
	public enum ShapeType { Circle, Square, Triangle };
	public ShapeType MyType;

	[SerializeField] private ShapeType _prevShape;

	[SerializeField] private TextMeshProUGUI _txtbox;

	private bool _canClick = true;

	[HideInInspector] public bool GameStarted, GameEnded;
	
	[SerializeField] private GameObject _endGamePanel;
	[SerializeField] private GameObject _startGameBtn;

	[SerializeField] private SliderControl _slider;

	private void Awake()
	{
		_slider = _slider.GetComponent<SliderControl>();
		ui = ui.GetComponent<UIHandler>();
	}
	
	private void Start()
	{
		DefineShape(0);
	}

	private void Update()
	{
		// CHOICES INPUT
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Verifier(true);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Verifier(false);
		}
	}

	public void InitGame()
	{
		GameStarted = true;
		AnimShapes();
	}
	
	public void DefineShape(int index)
	{
		var num = Random.Range(1, 4);

		switch (num)
		{
			case 1: MyType = ShapeType.Circle;
				break;
			case 2: MyType = ShapeType.Square;
				break;
			case 3: MyType = ShapeType.Triangle;
				break;
		}
		
		Container[index].UpdateSprite(Shapes[num - 1]);

		if (GameStarted) return;
		
		_prevShape = MyType;
	}

	public void Verifier(bool verifier)
	{
		if (!_canClick) return;
		
		if (verifier)
		{
			if (_prevShape == MyType)
			{
				_slider.ResetValue();
				MyPoints++;
			}
			else
			{
				EndGame();
			}
		} else
		{
			if (_prevShape != MyType)
			{
				_slider.ResetValue();
				MyPoints++;
			}
			else
			{
				EndGame();
			}
		}

		if (GameEnded) return;

		_prevShape = MyType;
		
		AnimShapes();

		_txtbox.text = MyPoints.ToString();

		_canClick = false;

		StartCoroutine(DelayBetweenClicks());
	}

	private void AnimShapes()
	{
		Container[0].CenterToLeft();
		
		DefineShape(1);
		
		Container[1].RightToCenter();
		
		Container.Reverse();
	}

	private IEnumerator DelayBetweenClicks()
	{
		yield return new WaitForSeconds(.35f);
		
		_canClick = true;
	}

	public void EndGame()
	{
		GameEnded = !GameEnded;
		_canClick = !_canClick;
		_endGamePanel.SetActive(true);
	}

	public void PlayAgain()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
	
}