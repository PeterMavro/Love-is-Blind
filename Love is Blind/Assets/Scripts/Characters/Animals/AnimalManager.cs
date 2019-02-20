using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public Animal[] animals;

    private void Update()
    {
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].OnUpdated(Time.deltaTime);
        }
    }
}