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

    [SerializeField] private float _bossReactionTime = 1f;
    [SerializeField] private ConfirmButton _confirmButton;
    private enum Turn { Player, Boss }
    private Turn _currentTurn = Turn.Player;
    private bool _sessionOver = false;

    private void OnEnable()
    {
        _confirmButton.OnConfirmButtonClicked += PlayerTurnEnded;
        ReactionBoss.OnBossInteraction += BossTurnEnded;
        ReactionBoss.OnBossDied += SessionOver;
        HangedBarVisualizer.OnHangedBarReachedEnd += GameOver;
    }

    private void OnDisable()
    {
        _confirmButton.OnConfirmButtonClicked -= PlayerTurnEnded;
        ReactionBoss.OnBossInteraction -= BossTurnEnded;
        ReactionBoss.OnBossDied -= SessionOver;
        HangedBarVisualizer.OnHangedBarReachedEnd -= GameOver;
    }

    private void Start() {
        StartPlayerTurn();
    }

    private void PlayerTurnEnded()
    {
        _currentTurn = Turn.Boss;
    }
    private void BossTurnEnded(float obj)
    {
        if (_sessionOver) return;
        StartCoroutine(NewTurn());
    }

    private IEnumerator NewTurn()
    {
        if (_sessionOver) yield break;
        yield return new WaitForSeconds(_bossReactionTime);
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
        GameLoopEnded?.Invoke();
    }

    private void SessionOver()
    {
        if (_sessionOver) return;
        Debug.Log("Session Over");
        _sessionOver = true;
        GameLoopEnded?.Invoke();
    }
}
