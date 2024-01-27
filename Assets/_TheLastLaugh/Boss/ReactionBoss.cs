using System;
using UnityEngine;

public class ReactionBoss : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;
    private float _health, _currentHealth;

    private void OnEnable() {
        DropZoneAndCombiner.OnCardsCombined += OnCardsCombined;
    }

    private void OnCardsCombined(CardsCombined combined)
    {
        // if the result es >0.5 the boss is not hit and the player is hit, else the boss is hit and the player is not hit
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
            Debug.Log("Player is hit");
        }
        else
        {
            Debug.Log("Boss is hit");
            _currentHealth -= result;
            Debug.Log(_currentHealth);
        }
    }

    private void Start() {
        _health = bossStats._health;
        _currentHealth = _health;
    }
}
