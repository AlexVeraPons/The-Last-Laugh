using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionVisualizer : MonoBehaviour
{
    public static Action OnSelectedCards;
    public static Action<Deck> OnDrawnCards;

    [SerializeField] private DeckController _playerDeck;

    [SerializeField] private DeckController _availableCards;
    [SerializeField] private DeckController _shownCards;
    [SerializeField] private GameObject _selectionZonePrefab;
    [SerializeField] private ConfirmButton _confirmButton;

    [SerializeField] private int _numberOfCardsToShow = 3;
    [SerializeField] private int _numberOfCardsToSelect = 1;



    private List<SelectionController> _selectionZones = new List<SelectionController>();
    [SerializeField] private List<Transform> _selectionZonesPositions = new List<Transform>();

    private List<Card> _selectedCards = new List<Card>();


    private void OnEnable() {
        CoreLoop.OnSelectCards += ShowCard;

        _confirmButton.OnConfirmButtonClicked+= ConfirmSelection;
        _confirmButton.gameObject.SetActive(false);

    }

    private void ConfirmSelection()
    {
        foreach (Card card in _selectedCards)
        {
            _shownCards.RemoveCard(card);
        }

        _availableCards.AddCards(_shownCards.GetCards());
        _playerDeck.AddCards(_selectedCards);
        _selectedCards.Clear();
        _shownCards.Clear();

        foreach (SelectionController selectionZone in _selectionZones)
        {
            Destroy(selectionZone.gameObject);
        }

        _selectionZones.Clear();
        _selectionZonesPositions.Clear();

        OnSelectedCards?.Invoke();
    }

    private void OnDisable() {
        CoreLoop.OnSelectCards -= ShowCard;
        _confirmButton.OnConfirmButtonClicked-= ConfirmSelection;
    }
    private void ShowCard()
    {
        for (int i = 0; i < _numberOfCardsToShow; i++)
        {
            Card card = _availableCards.Draw();
            if (card == null)
            {
                break;
            }
            _shownCards.AddCard(card);
            Debug.Log("Drawn card: " + card.name);
        }

        //foreach card in shown cards 
        //create a selection zone
        //add card to selection zone
        int index = 0;
        foreach (Card card in _shownCards.GetCards())
        {
            GameObject selectionZoneObject = Instantiate(_selectionZonePrefab);
            SelectionController selectionZone = selectionZoneObject.GetComponent<SelectionController>();

            selectionZone.SetCard(card);
            _selectionZones.Add(selectionZone);
            _selectionZonesPositions.Add(selectionZoneObject.transform);
            selectionZoneObject.transform.position = _selectionZonesPositions[index].position;
            index++;
        }
    }

    private void Update()
    {
        if (GetCount() == _numberOfCardsToSelect)
        {
            _confirmButton.gameObject.SetActive(true);
        }
        else
        {
            _confirmButton.gameObject.SetActive(false);
        }

        int GetCount()
        {
            int count = 0;
            foreach (SelectionController selectionZone in _selectionZones)
            {
                if (selectionZone.IsSelected())
                {
                    count++;
                }
            }
            Debug.Log("Count: " + count);
            return count;
        }
    }
}
