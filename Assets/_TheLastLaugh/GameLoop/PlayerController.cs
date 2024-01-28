using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<Deck> OnDrawnCards;
    [SerializeField] private int _numberOfCardsToDraw = 6;
    [SerializeField] private DeckController _hand;
    [SerializeField] private DeckController _deck;
    private void OnEnable() {
        GameLoop.OnPlayerTurn += StartTurn;
    }

    private void OnDisable() {
        GameLoop.OnPlayerTurn -= StartTurn;
    }
    public void StartTurn()
    {
        Debug.Log("Player turn started");
        PutHandInDeck();
        ShuffleDeck();
        DrawCards(_numberOfCardsToDraw);
    }

    private void PutHandInDeck()
    {
        _deck.AddCards(_hand.GetCards());
        _hand.Clear();
    }

    private void ShuffleDeck()
    {
        _deck.Shuffle();
    }

    private void DrawCards(int numberOfCards)
    {

        for (int i = 0; i < numberOfCards; i++)
        {
            Card card = _deck.Draw();
            if (card == null)
            {
                break;
            }
            _hand.AddCard(card);
            Debug.Log("Drawn card: " + card.name);
        }

        OnDrawnCards?.Invoke(_hand.GetDeck());
    }
}
