using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public Action<Deck> OnDeckChanged;
    public Action<Deck> OnDeckShuffled;
    public Action<Deck> OnDrawn;
    [SerializeField] private bool ResetDeckAfterSession = false;
    [SerializeField] private bool YesNoShuffle = true;
    [SerializeField] private bool ClearOnStart = false;
    [SerializeField] private bool UseNewDeck = false;
    [SerializeField] protected Deck deck;
    protected Deck _deckInitialState;

    protected virtual void OnEnable()
    {
        CoreLoop.OnExitCombat += ResetDeck;
        CoreLoop.OnEnterCombat += ResetDeck;
    }

    private void OnDisable()
    {
        CoreLoop.OnExitCombat -= ResetDeck;
        CoreLoop.OnEnterCombat -= ResetDeck;
        ResetDeck();
    }

    protected virtual void Start()
    {
        if (UseNewDeck)
        {
            deck = Instantiate(deck);
            deck.CreateDeck();
        }
        if (ClearOnStart)
        {
            Clear();
        }
        
        CopyDeck();
    }

    public void CopyDeck()
    {
        _deckInitialState = Instantiate(deck);
        _deckInitialState.name = deck.name;
        _deckInitialState.cards = new List<Card>(deck.cards);
    }

    public void ResetDeck()
    {
        if (ResetDeckAfterSession == false) return;
        deck.cards = new List<Card>(_deckInitialState.cards);
        OnDeckChanged?.Invoke(deck);
    }

    public void Shuffle()
    {
        if (YesNoShuffle)
        {
            deck.ShuffleIntoPairs();
        }
        else
        {
            deck.Shuffle();
        }

        OnDeckShuffled?.Invoke(deck);
    }

    public Card Draw()
    {
        OnDrawn?.Invoke(deck);
        OnDeckChanged?.Invoke(deck);
        return deck.Draw();
    }

    public Card Draw(out bool empty)
    {
        OnDeckChanged?.Invoke(deck);
        return deck.Draw(out empty);
    }

    public void AddCard(Card card)
    {
        deck.AddCard(card);
        OnDeckChanged?.Invoke(deck);
    }

    public void AddCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            deck.AddCard(card);
        }

        OnDeckChanged?.Invoke(deck);
    }

    public void RemoveCard(Card card)
    {
        deck.RemoveCard(card);
        OnDeckChanged?.Invoke(deck);
    }

    public List<Card> GetCards()
    {
        return deck.cards;
    }

    public Deck GetDeck()
    {
        return deck;
    }

    public void Clear()
    {
        deck.cards.Clear();
        OnDeckChanged?.Invoke(deck);
    }
}
