using UnityEngine;

public class AnimalManager : Singleton<AnimalManager>
{
    public Animal[] animals;

    private void Update()
    {
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].OnUpdated(Time.deltaTime);
        }
    }

    public void ShowAnimals(bool show)
    {
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].RendererComponent.SetActive(show);
        }
    }
}