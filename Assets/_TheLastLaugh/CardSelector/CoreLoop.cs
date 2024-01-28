using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLoop : MonoBehaviour
{
    public static Action OnSelectCards;
    public static Action OnSelectCardsDone;

    public static Action OnEnterCombat;
    public static Action OnExitCombat;

    public static Action OnEnd;

    public static Action<int> OnNewDay;
    private enum State
    {
        SelectCards,
        Combat,
        End
    }

    private State _currentState = State.Combat;

    private int _day = 1;
    private int _selectedDone = 0;
    private const int _selectedDoneNeeded = 2;

    private void OnEnable()
    {
        GameLoop.GameLoopEnded += GameLoopEnded;
        SelectionVisualizer.OnSelectedCards += SelectCardsDone;
        HangedBarVisualizer.OnHangedBarReachedEnd += YouDied;
    }

    private void YouDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
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
            OnNewDay?.Invoke(_day);
            _selectedDone = 0;
        }
    }

    private void GameLoopEnded()
    {
        _day++;
        if (_day != 6)
        {
            _currentState = State.SelectCards;
            OnExitCombat?.Invoke();
            OnSelectCards?.Invoke();
        }
        else
        {
            OnEnd?.Invoke();
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
