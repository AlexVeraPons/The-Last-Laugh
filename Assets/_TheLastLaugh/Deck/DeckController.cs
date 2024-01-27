using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] private Deck deck;
    Card card1;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Card card = Draw();
            Debug.Log(card.cardText);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            int n = Random.Range(0, 1000);
            card1 = ScriptableObject.CreateInstance<Card>();
            card1.cardText = "New Card" + n.ToString();
            AddCard(card1);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            Shuffle();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            RemoveCard(card1);
        }
    }

    public void Shuffle()
    {
        deck.Shuffle();
    }

    public Card Draw()
    {
        return deck.Draw();
    }

    public Card Draw(out bool empty)
    {
        return deck.Draw(out empty);
    }

    public void AddCard(Card card)
    {
        deck.AddCard(card);
    }

    public void RemoveCard(Card card)
    {
        deck.RemoveCard(card);
    }
}
