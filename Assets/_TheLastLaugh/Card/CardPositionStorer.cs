using System.Collections.Generic;
using UnityEngine;

public class CardPositionStorer : MonoBehaviour
{
    private static CardPositionStorer _instance;
    public static CardPositionStorer Instance => _instance;

    [SerializeField] private List<Vector3> _cardPositions = new List<Vector3>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start() {
        _cardPositions.Clear();
        foreach (Transform child in transform)
        {
            _cardPositions.Add(child.position);
        }
    }

    public void GetCardPositions(List<Vector3> cardPositions)
    {
        cardPositions.Clear();
        cardPositions.AddRange(_cardPositions);
    }

    public Vector3 GetCardPosition(int index)
    {
        Start();
        return _cardPositions[index];
    }
}
