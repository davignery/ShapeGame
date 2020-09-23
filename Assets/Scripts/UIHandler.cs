using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	[SerializeField] private GameManager _manager;

	public Button _startBtn;
	
	public List<GameObject> _btns = new List<GameObject>();

	private void Start()
	{
		_manager = _manager.GetComponent<GameManager>();

		_startBtn = _startBtn.GetComponent<Button>();
		
		_startBtn.onClick.AddListener(ChangeUi);
	}

	private void Update()
	{
		// START GAME INPUT
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ChangeUi();
		}
	}

	public void ChangeUi()
	{
		foreach (var button in _btns)
		{
			button.gameObject.SetActive(true);
		}
		
		_startBtn.gameObject.SetActive(false);
		
		_manager.InitGame();
	}
	
}