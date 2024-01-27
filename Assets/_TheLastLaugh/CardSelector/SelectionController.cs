using System;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
public class SelectionController : MonoBehaviour
{
    [SerializeField]private CardVisualizer _cardVisualizer;
    [SerializeField] private GameObject _selectionIndicator;
    
    private bool _isSelected = false;

    private void Start() {

        _cardVisualizer.gameObject.GetComponent<Collider2D>().enabled = false;
    }
    public void SetCard(Card card)
    {
        _isSelected = false;
        _cardVisualizer.SetCard(card);
    }

    public void MouseDown(Vector2 mousePosition)
    {
        if (IsMouseOver(mousePosition))
        {
            if (_isSelected)
            {
                _isSelected = false;
            }
            else
            {
                _isSelected = true;
            }
        }
    }

    public bool IsSelected()
    {
        return _isSelected;
    }

    private bool IsMouseOver(Vector2 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider.OverlapPoint(mousePosition);
    }
    private void Update() {
        if (_isSelected)
        {
            _selectionIndicator.SetActive(true);
        }
        else
        {
            _selectionIndicator.SetActive(false);
        }
    }

    public Card GetCard()
    {
        return _cardVisualizer.GetCard();
    }
}
