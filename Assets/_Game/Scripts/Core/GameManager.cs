using UnityEngine;

public class GameManager : MonoBehaviour
{
   public float ElapsedTime { get; private set; }

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        
    }
}