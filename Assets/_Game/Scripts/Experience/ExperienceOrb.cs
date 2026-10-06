using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] private int experienceAmount = 1;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerExperience playerExperience =
            other.GetComponent<PlayerExperience>();

        if (playerExperience != null)
        {
            playerExperience.AddExperience(experienceAmount);
            Destroy(gameObject);
        }
    }
    public void SetExperienceAmount(int amount)
    {
        experienceAmount = amount;
    }
}