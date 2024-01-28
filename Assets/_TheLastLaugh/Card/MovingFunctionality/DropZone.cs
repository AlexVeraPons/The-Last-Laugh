using System;
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

    internal bool CanFit(CardType startOrrEnd)
    {
        if (_cards.Count >= _maxCards)
        {
            return false;
        }

        if (_cards.Count == 1 && _maxCards == 2)
        {
            if (_cards[0].startOrrEnd == startOrrEnd)
            {
                return false;
            }
        }

        return true;
    }
}
