
using UnityEngine;
using System;
public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int currentExperience;
    
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int experienceToNextLevel = 10;
    
    public event Action OnLevelUp;


    public void AddExperience(int amount)
    {
        
        currentExperience += amount;
        Debug.Log(currentExperience);
        while (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }
    
    private void LevelUp()
    {
        currentLevel++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel += 5;
        OnLevelUp?.Invoke();

        Debug.Log("Level Up! Level: " + currentLevel);
    }
    
}