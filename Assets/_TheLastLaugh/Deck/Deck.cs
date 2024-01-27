using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public List<Card> cards;

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Card temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

    public Card Draw(out bool empty)
    {
        if (cards.Count == 0)
        {
            empty = true;
            return null;
        }

        Card card = cards[0];
        cards.RemoveAt(0);
        empty = false;
        return card;
    }

    public Card Draw()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        }
        else
        {
            Debug.LogError("Card not found in deck");
        }
    }
}
