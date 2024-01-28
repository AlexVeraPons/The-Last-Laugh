using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardText;
    public CardType startOrrEnd;

    [Header("Card Stats")]
    public CardStat stats;

    [HideInInspector]public DeckController deck;
}
public enum CardType
{
    Start,
    End,
}
public enum CardStat
{
    Dark,
    Surreal,
    Parody,
    Self
}
