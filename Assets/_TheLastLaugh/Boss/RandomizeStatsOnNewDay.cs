using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizeStatsOnNewDay : MonoBehaviour
{
    [SerializeField] private BossStats _bossStats;

    private void OnEnable()
    {
        CoreLoop.OnEnterCombat += RandomizeStats;
    }

    private void RandomizeStats()
    {
        List<float> possibleStats = new List<float> { 0, 0, 0.5f, 1f };
        possibleStats = possibleStats.OrderBy(x => UnityEngine.Random.value).ToList();

        _bossStats.SetStat(CardStat.Dark, possibleStats[0]);
        _bossStats.SetStat(CardStat.Parody, possibleStats[1]);
        _bossStats.SetStat(CardStat.Surreal, possibleStats[2]);
        _bossStats.SetStat(CardStat.Self, possibleStats[3]);

        foreach (float stat in possibleStats)
        {
            Debug.Log(stat);
        }


    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeStats();
        }
    }


}
