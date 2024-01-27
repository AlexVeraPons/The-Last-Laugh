using System;
using UnityEngine;

public class DropZoneAndCombiner : DropZone
{
    public static Action<CardsCombined> OnCardsCombined;
    [SerializeField] private GameObject _confirmButton;

    private void OnEnable() {
        _confirmButton.GetComponent<ConfirmButton>().OnConfirmButtonClicked += CombineCards;
    }
    
    private void OnDisable() {
        _confirmButton.GetComponent<ConfirmButton>().OnConfirmButtonClicked -= CombineCards;
    }


    private void Start() {
        _confirmButton.SetActive(false);   
    }

    private void CombineCards()
    {
        if (_cards.Count == 2)
        {
            CardsCombined cardsCombined = new CardsCombined();
            cardsCombined.cardStat1 = _cards[0].stats;
            cardsCombined.cardStat2 = _cards[1].stats;
            OnCardsCombined?.Invoke(cardsCombined);
        }
    }

    private void Update()
    {
        if (_cards.Count == 2 && _cards[0].startOrrEnd != _cards[1].startOrrEnd)
        {
            ShowConfirmButton();
        }
        else
        {
            HideConfirmButton();
        }
    }

    private void ShowConfirmButton()
    {
        _confirmButton.SetActive(true);
    }

    private void HideConfirmButton()
    {
        _confirmButton.SetActive(false);
    }
}

public class CardsCombined
{
    public CardStat cardStat1;
    public CardStat cardStat2;
}
