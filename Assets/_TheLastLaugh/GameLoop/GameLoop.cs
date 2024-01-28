using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //playerturn -> boss reaction -> new turn (delay)
    public static Action OnPlayerTurn;
    public static Action GameLoopEnded;
    public static Action GameLoopStarted;
    public static Action GameEnded;

    public static Action OnBossReaction;
    public static Action OnBossReactionEnded;

    [SerializeField] private float _bossReactionTime = 1f;
    [SerializeField] private ConfirmButton _confirmButton;
    [SerializeField] private int _availableTurns = 4 ;


    private enum Turn { Player, Boss }
    private Turn _currentTurn = Turn.Player;
    private bool _sessionOver = false;
    private int _currentTurrnIndex = 0;

    private void OnEnable()
    {
        _confirmButton.OnConfirmButtonClicked += PlayerTurnEnded;
        ReactionBoss.OnBossInteraction += BossTurnEnded;
        ReactionBoss.OnBossDied += SessionOver;
        HangedBarVisualizer.OnHangedBarReachedEnd += GameOver;
        CoreLoop.OnEnterCombat += StartLoop;
    }

    private void OnDisable()
    {
        _confirmButton.OnConfirmButtonClicked -= PlayerTurnEnded;
        ReactionBoss.OnBossInteraction -= BossTurnEnded;
        ReactionBoss.OnBossDied -= SessionOver;
        HangedBarVisualizer.OnHangedBarReachedEnd -= GameOver;
        CoreLoop.OnEnterCombat -= StartLoop;
    }

    private void Start()
    {
        StartLoop();
    }
    private void StartLoop()
    {
        StartPlayerTurn();
        GameLoopStarted?.Invoke();
        _sessionOver = false;
        _currentTurrnIndex = 1;
    }

    private void PlayerTurnEnded()
    {
        _currentTurn = Turn.Boss;
    }
    private void BossTurnEnded(float obj)
    {
        if (_sessionOver) return;
        if (_currentTurrnIndex >= _availableTurns)
        {
            SessionOver();
            return;
        }

        StartCoroutine(NewTurn());
    }

    private IEnumerator NewTurn()
    {
        _currentTurrnIndex++;
        OnBossReaction?.Invoke();
        if (_sessionOver) yield break;
        yield return new WaitForSeconds(_bossReactionTime);
        OnBossReactionEnded?.Invoke();
        if (_sessionOver) yield break;
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        _currentTurn = Turn.Player;
        OnPlayerTurn?.Invoke();
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
        if (_sessionOver) return;
        _sessionOver = true;
        GameEnded?.Invoke();
    }

    private void SessionOver()
    {
        if (_sessionOver) return;
        Debug.Log("Session Over");
        _sessionOver = true;
        GameLoopEnded?.Invoke();
    }
}
