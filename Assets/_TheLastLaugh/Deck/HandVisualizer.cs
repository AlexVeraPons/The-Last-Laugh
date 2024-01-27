using System;
using UnityEngine;

public class HandVisualizer : MonoBehaviour
{
    [SerializeField] private DeckController _hand;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private float _padding = 2f;
    [SerializeField] private DropZone _handDropZone;

    private void OnEnable()
    {
        SelectionVisualizer.OnDrawnCards += UpdateHand;
        PlayerController.OnDrawnCards += UpdateHand;
        GameLoop.GameLoopEnded += OnSessionOver;
    }

    private void OnDisable()
    {
        SelectionVisualizer.OnDrawnCards -= UpdateHand;
        PlayerController.OnDrawnCards -= UpdateHand;
        GameLoop.GameLoopEnded -= OnSessionOver;
    }

    private void UpdateHand(Deck deck)
    {
        Debug.Log("UpdateHand");
        Debug.Log(deck.cards.Count);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach (Card card in deck.cards)
        {
            GameObject cardObject = Instantiate(_cardPrefab);
            cardObject.GetComponent<CardVisualizer>().SetCard(card);
            cardObject.GetComponent<DragAndDropFromZones>().SetDropZone(_handDropZone);

            cardObject.transform.position = new Vector3(0, -50, 0);
            cardObject.GetComponent<GoToCardPosition>().GoToPosition(i);
            i++;
        }
    }

    private void OnSessionOver()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
