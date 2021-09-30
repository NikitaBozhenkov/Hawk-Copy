using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Character[] enemies;

    public void Setup()
    {
        foreach (Character enemy in enemies)
        {
            enemy.Setup();
        }
    }
}