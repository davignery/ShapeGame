using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    [SerializeField] private GameManager _manager;
    
    private Slider _slider;

    private float _decrement = .5f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if(!_manager.GameStarted) return;
        
        _slider.value -= Time.deltaTime * (_decrement + (_manager.MyPoints / 50));

        if (Mathf.Approximately(_slider.value, 0))
        {
            _manager.EndGame();
        }
    }

    public void ResetValue()
    {
        _slider.value = _slider.maxValue;
    }
    
    //TODO increase difficult
}