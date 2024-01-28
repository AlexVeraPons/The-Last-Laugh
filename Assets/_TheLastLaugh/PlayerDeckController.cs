using UnityEngine;

public class PlayerDeckController : DeckController
{
    protected override void OnEnable()
    {
           deck = Instantiate(deck);
        deck.CreateDeck();
        Card[] cards = Resources.LoadAll<Card>("StarterDeck");
        foreach (Card card in cards)
        {
            deck.AddCard(card);
        }

        // Debug.Log("Player deck created" + deck.name + " with " + deck.cards.Count + " cards");
        base.OnEnable();
    }
}
