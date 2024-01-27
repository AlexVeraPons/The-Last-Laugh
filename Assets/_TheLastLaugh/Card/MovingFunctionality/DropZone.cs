using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    [SerializeField] protected List<Card> _cards = new List<Card>();
    [SerializeField] private int _maxCards;
    public bool IsFull => _cards.Count >= _maxCards;

    private void Start() {
        foreach (Transform child in transform)
        {
            Card card = child.GetComponent<CardVisualizer>()?.GetCard();
            if (card != null)
            {
                _cards.Add(card);
            }
        }
    }
    public void AddCard(Card card)
    {
        if (_cards.Count >= _maxCards)
        {
            return;
        }

        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (!_cards.Contains(card))
        {
            return;
        }
        _cards.Remove(card);
    }
}
