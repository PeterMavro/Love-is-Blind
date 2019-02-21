using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RendererComponent : MonoBehaviour
{
    public Material[] materials;

    private Renderer[] _rens;

    private void Awake()
    {
        _rens = GetComponentsInChildren<Renderer>();
    }

    public void ChangeMaterial(int matIndex)
    {
        if (!materials.ValidIndex(matIndex)) return;

        for (int i = 0; i < _rens.Length; i++)
        {
            _rens[i].material = materials[matIndex];
        }
    }

    public void SetActive(bool active)
    {
        for (int i = 0; i < _rens.Length; i++)
        {
            _rens[i].enabled = active;
        }
    }
}
