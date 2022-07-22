using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    private int _expirienceToLevelUp = 100;

    public int Experience { get; private set; } = 0;
    public int Level { get; private set; } = 1;

    public event UnityAction<int, int> ExpirienceAdded;
    public event UnityAction GotLevelUp;

    private void Start()
    {
        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
    }

    public void AddReward(int expirience)
    {
        Experience += expirience;

        if (Experience >= _expirienceToLevelUp)
            LevelUp();

        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
    }

    private void LevelUp()
    {
        int experinceToLevelUpInsrease = 50;

        Experience = Experience - _expirienceToLevelUp;
        _expirienceToLevelUp += experinceToLevelUpInsrease;
        Level++;
        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
        GotLevelUp?.Invoke();
    }
}
