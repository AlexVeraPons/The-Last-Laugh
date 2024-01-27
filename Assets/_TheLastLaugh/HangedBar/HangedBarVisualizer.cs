using System;
using System.Collections;
using UnityEngine;

public class HangedBarVisualizer : MonoBehaviour
{
    public static Action OnHangedBarReachedEnd;
    

    [Header("Hanged Bar Settings")]
    [SerializeField] private float _decreaseSpeed = 0.1f;
    [SerializeField] private float _decreaseWhenHit = 1f;
    [SerializeField] private float _increaseWhenBossHit = 1f;
    [SerializeField] private float _timeToReachValueWhenHit = 0.3f;
    [SerializeField] private float _health = 10f;

    [Header("References ----")]
    [SerializeField] private Transform _hangedIcon;
    [SerializeField] private SpriteRenderer _barSpriteRenderer;

    [SerializeField] private float _currentHealth;
    private bool _isDecreasing = true;


    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnEnable()
    {
        ReactionBoss.OnBossInteraction += OnBossInteraction;
        GameLoop.GameLoopEnded += Stop;
        GameLoop.GameLoopStarted += StartNewTurn;
    }

    private void OnBossInteraction(float value)
    {
        if (value < 0.5f)
        {
            if (_currentHealth - _decreaseWhenHit < 0)
            {
                _currentHealth = 0;
                StartCoroutine(DecreaseBarWhenHit());
                return;
            }

            _currentHealth -= _decreaseWhenHit;
            StartCoroutine(DecreaseBarWhenHit());
        }
        else
        {
            if (_currentHealth + _increaseWhenBossHit * value > _health)
            {
                value = (_health - _currentHealth);
                _currentHealth = _health;
                StartCoroutine(GoToStart());
                return;
            }

            _currentHealth += _increaseWhenBossHit * value;
            StartCoroutine(IncreaseBarWhenHit(value));
        }
    }

    private IEnumerator GoToStart()
    {
        _isDecreasing = false;

        float time = 0;
        float startValue = _currentHealth;
        float endValue = _health;

        while (time < _timeToReachValueWhenHit)
        {
            _currentHealth = Mathf.Lerp(startValue, endValue, time / _timeToReachValueWhenHit);
            time += Time.deltaTime;
            yield return null;
        }
        _currentHealth = endValue;

        _isDecreasing = true;
    }

    private IEnumerator IncreaseBarWhenHit(float value)
    {
        _isDecreasing = false;

        float time = 0;
        float startValue = _currentHealth - _increaseWhenBossHit * value;
        float endValue = _currentHealth;

        if(_currentHealth >+ _health)
        {
            endValue = _health;
        }


        while (time < _timeToReachValueWhenHit)
        {
            _currentHealth = Mathf.Lerp(startValue, endValue, time / _timeToReachValueWhenHit);
            time += Time.deltaTime;
            yield return null;
        }
        _currentHealth = endValue;

        _isDecreasing = true;
    }

    private void Update()
    {



        if (_currentHealth <= 0)
        {
            OnHangedBarReachedEnd?.Invoke();
            return;
        }


        if (_isDecreasing)
        {
            _currentHealth -= _decreaseSpeed * Time.deltaTime;
        }

        UpdateIconPosition();

    }

    private void UpdateIconPosition()
    {
        float x = _barSpriteRenderer.bounds.min.x + (_barSpriteRenderer.bounds.size.x * (1 - _currentHealth / _health));
        _hangedIcon.position = new Vector3(x, _hangedIcon.position.y, _hangedIcon.position.z);
    }

    private IEnumerator DecreaseBarWhenHit()
    {
        _isDecreasing = false;

        float time = 0;
        float startValue = _currentHealth + _decreaseWhenHit;
        float endValue = _currentHealth < 0 ? 0 : _currentHealth;
        while (time < _timeToReachValueWhenHit)
        {
            _currentHealth = Mathf.Lerp(startValue, endValue, time / _timeToReachValueWhenHit);
            time += Time.deltaTime;
            yield return null;
        }
        _currentHealth = endValue;

        _isDecreasing = true;
    }

    private void Stop()
    {
        _isDecreasing = false;
        _barSpriteRenderer.enabled = false;
        _hangedIcon.gameObject.SetActive(false);
    }

    private void StartNewTurn()
    {
        _isDecreasing = true;
        _barSpriteRenderer.enabled = true;
        _hangedIcon.gameObject.SetActive(true);
        _currentHealth += _increaseWhenBossHit*1.5f >= _health ? _health : _increaseWhenBossHit*1.5f;
    }
}
