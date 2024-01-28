using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class PopulateDeckFromFolder : MonoBehaviour
{

    [SerializeField] private Deck _deckToPopulate;

    private void Start() {
        PopulateDeck();
        StartCoroutine(PopulateDeckCoroutine());
    }

    private IEnumerator PopulateDeckCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        PopulateDeck();
    }

    private enum Folder
    {
        StarterDeck,
        Prompts,
        Responses
    }
    [SerializeField] private Folder _type;
    public void PopulateDeck()
    {
        _deckToPopulate.Clear();
        Card[] cards = new Card[0];

        switch (_type)
        {
            case Folder.StarterDeck:
                cards = Resources.LoadAll<Card>("StarterDeck");
                break;
            case Folder.Prompts:
                cards = Resources.LoadAll<Card>("Prompts");
                break;
            case Folder.Responses:
                cards = Resources.LoadAll<Card>("Responses");
                break;
        }

        foreach (Card card in cards)
        {
            _deckToPopulate.AddCard(card);
        }

    }
}

# if UNITY_EDITOR
[CustomEditor(typeof(PopulateDeckFromFolder))]
public class PopulateDeckFromFolderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Populate Deck"))
        {
            PopulateDeckFromFolder populateDeckFromFolder = (PopulateDeckFromFolder)target;
            populateDeckFromFolder.PopulateDeck();
        }
    }
}
#endif
