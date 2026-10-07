using System;
using UnityEngine;

public class MaxHealthUpgrade : IUpgrade
{
   private UpgradeData data;
   public UpgradeData Data => data;
   private PlayerHealth playerHealth;
   private int level; 
   public int Level => level;
   
   
   public MaxHealthUpgrade(UpgradeData data,PlayerHealth playerHealth)
   {
      this.playerHealth = playerHealth;
      this.data = data;
   }
   
   public void Apply()
   {
    playerHealth.IncreaseMaxHealth(Mathf.RoundToInt(data.Value));
      level++;
   }
}
