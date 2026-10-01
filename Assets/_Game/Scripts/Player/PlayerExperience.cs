
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int currentExperience;
    
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int experienceToNextLevel = 10;


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

        Debug.Log("Level Up! Level: " + currentLevel);
    }
    
}