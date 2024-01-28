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

    public void Clear()
    {
        cards.Clear();
    }

    public void ShuffleIntoPairs()
    {
        Deck tempDeck1 = CreateInstance<Deck>();
        Deck tempDeck2 = CreateInstance<Deck>();

        tempDeck1.CreateDeck();
        tempDeck2.CreateDeck();

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

        for (int i = 0; i < tempDeck1.cards.Count; i++)
        {
            cards.Add(tempDeck1.cards[i]);
            cards.Add(tempDeck2.cards[i]);
        }

        DestroyImmediate(tempDeck1);
        DestroyImmediate(tempDeck2);
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
    
    public void CreateDeck()
    {
        cards = new List<Card>();
    }

    public void RemoveCard(Card card)
    {
        foreach (Card cardInDeck in cards)
        {
            if (cardInDeck.cardText == card.cardText)
            {
                cards.Remove(cardInDeck);
                return;
            }
        }
    }
}
