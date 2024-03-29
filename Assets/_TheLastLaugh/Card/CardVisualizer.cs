using UnityEngine;

public class CardVisualizer : MonoBehaviour
{
    [Header("Card")]
    [SerializeField] private Card card;

    [Header("References")]
    [SerializeField] private TMPro.TextMeshPro cardText;
    [SerializeField] private SpriteRenderer FrontSpriteRenderer;
    [SerializeField] private SpriteRenderer BackSpriteRenderer;
    [SerializeField] private CardType cardTypeSprite;
    [SerializeField] private IconSelector _iconSelector;

    private void Start()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if (card == null)
        {
            return;
        }

        cardText.text = card.cardText;
        cardTypeSprite = card.startOrrEnd;
        if (cardTypeSprite == CardType.Start)
        {
            FrontSpriteRenderer.enabled = true;
            BackSpriteRenderer.enabled = false;
        }
        else if (cardTypeSprite == CardType.End)
        {
            FrontSpriteRenderer.enabled = false;
            BackSpriteRenderer.enabled = true;
        }

        _iconSelector.ShowIcon(card.stats);
    }

    public Card GetCard()
    {
        return card;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        OnValidate();
    }
}
