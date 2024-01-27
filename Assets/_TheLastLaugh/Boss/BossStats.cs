using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "BossStats", order = 0)]
public class BossStats : ScriptableObject
{
    public List<BossStat> bossStats = new List<BossStat>();
    public float _health;

    // Initialize the list of boss stats
    private void Awake()
    {
        if (bossStats.Count == 4)
        {
            return;
        }

        bossStats.Add(new BossStat(CardStat.Dark, 0));
        bossStats.Add(new BossStat(CardStat.Parody, 0));
        bossStats.Add(new BossStat(CardStat.Surreal,   0));
        bossStats.Add(new BossStat(CardStat.Surreal, 0));
    }
}

[System.Serializable]
public class BossStat 
{
    public BossStat(CardStat stat, float value)
    {
        this.stat = stat;
        this.value = value;
    }

    public CardStat stat;

    [Range(0, 1)]
    public float value;
} 
