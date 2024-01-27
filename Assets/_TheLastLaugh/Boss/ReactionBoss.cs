using System;
using UnityEngine;

public class ReactionBoss : MonoBehaviour
{
    public static Action<float> OnBossInteraction;
    public static Action OnBossDied;
    [SerializeField] private BossStats bossStats;
    private float _health, _currentHealth;

    private void OnEnable() {
        DropZoneAndCombiner.OnCardsCombined += OnCardsCombined;
    }

    private void OnCardsCombined(CardsCombined combined)
    {
        float result = 0;
        foreach (BossStat bossStat in bossStats.bossStats)
        {
            if (bossStat.stat == combined.cardStat1)
            {
                result += bossStat.value * 1;
            }
            else if (bossStat.stat == combined.cardStat2)
            {
                result += bossStat.value * 1;
            }
        }

        if (result < 0.5f)
        {
            result = 0;
            OnBossInteraction?.Invoke(result);
        }
        else
        {
            _currentHealth -= result;
            OnBossInteraction?.Invoke(result);

            if (_currentHealth <= 0)
            {
                OnBossDied?.Invoke();
            }
        }
    }

    private void Start() {
        _health = bossStats._health;
        _currentHealth = _health;
    }
}
