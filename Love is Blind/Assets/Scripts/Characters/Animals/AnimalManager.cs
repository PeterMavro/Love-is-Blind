using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : Singleton<AnimalManager>
{
    [SerializeField]
    private List<Animal> _animals = new List<Animal>();

    private void Update()
    {
        for (int i = 0; i < _animals.Count; i++)
        {
            _animals[i].OnUpdated(Time.deltaTime);
        }
    }

    public void Add(Animal entity)
    {
        if (!_animals.Contains(entity))
            _animals.Add(entity);
    }

    public void Remove(Animal entity)
    {
        _animals.Remove(entity);
    }

    public void ShowAnimals(bool show)
    {
        for (int i = 0; i < _animals.Count; i++)
        {
            _animals[i].RendererComponent.SetActive(show);
        }
    }
}