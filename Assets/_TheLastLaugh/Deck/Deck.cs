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

    public void ShuffleIntoPairs()
    {
        Deck tempDeck1 = ScriptableObject.CreateInstance<Deck>();
        Deck tempDeck2 = ScriptableObject.CreateInstance<Deck>();

        foreach (Card card in cards)
        {
            if (card.startOrrEnd == CardType.Start)
            {
                tempDeck1.AddCard(card);
            }
            else
            {
                tempDeck2.AddCard(card);
            }
        }

        tempDeck1.Shuffle();
        tempDeck2.Shuffle();

        cards.Clear();

        while (tempDeck1.cards.Count > 0 || tempDeck2.cards.Count > 0)
        {
            if (tempDeck1.cards.Count > 0)
            {
                cards.Add(tempDeck1.Draw());
            }

            if (tempDeck2.cards.Count > 0)
            {
                cards.Add(tempDeck2.Draw());
            }
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
