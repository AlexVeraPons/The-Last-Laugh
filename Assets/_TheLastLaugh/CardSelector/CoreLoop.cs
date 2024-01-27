using System;
using UnityEngine;

public class CoreLoop : MonoBehaviour
{
    public static Action OnSelectCards;
    public static Action OnSelectCardsDone;

    public static Action OnEnterCombat;
    public static Action OnExitCombat;

    public static Action OnEnd;
    private enum State
    {
        SelectCards,
        Combat,
        End
    }

    private State _currentState = State.Combat;

    private int _day = 0;
    private int _selectedDone = 0;
    private const int _selectedDoneNeeded = 2;

    private void OnEnable()
    {
        GameLoop.GameLoopEnded += GameLoopEnded;
        SelectionVisualizer.OnSelectedCards += SelectCardsDone;
    }

    private void Start()
    {
    }

    private void SelectCardsDone()
    {
        _selectedDone++;
        if (_selectedDone >= _selectedDoneNeeded)
        {
            OnSelectCardsDone?.Invoke();
            _currentState = State.Combat;
            OnEnterCombat?.Invoke();
            _selectedDone = 0;
        }
    }

    private void GameLoopEnded()
    {
        _day++;
        if (_day < 5)
        {
            Debug.Log("Day " + _day);
            _currentState = State.SelectCards;
            OnExitCombat?.Invoke();
            OnSelectCards?.Invoke();
        }
        else
        {
            OnEnd?.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameLoopEnded();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SelectCardsDone();
        }
    }
}
