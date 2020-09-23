using DG.Tweening;
using UnityEngine;

public class ShapeContainer : MonoBehaviour
{
    private SpriteRenderer _sr;

    private const float AnimTimer = .3f;
    private const float PosY = 2.5f;
	private const float PosX = 4;

    private Tween _tween;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(Sprite shapeIndex)
    {
        _sr.sprite = shapeIndex;
    }
    
    public void RightToCenter()
    {
        transform.DORotate(Vector3.left, AnimTimer);
        
        transform.DOMoveX(0, AnimTimer);
    }

    public void CenterToLeft()
    {
        transform.DORotate(new Vector2(0, -90), AnimTimer);

        transform.DOMoveX(-PosX, AnimTimer).OnComplete(() =>
        {
            transform.position = new Vector2(PosX, PosY);
            
            transform.rotation = Quaternion.Euler(0, 90, 0); 
        });
    }
}