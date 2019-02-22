using UnityEngine;

public class Pitfall : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider[] relatedFloorColliders;

    private RendererComponent _renderer;

    public RendererComponent RendererComponent => _renderer;

    private void Awake()
    {
        _renderer = GetComponent<RendererComponent>();
    }

    private void OnEnable()
    {
        PitfallManager.Instance.Add(this);
    }

    private void OnDisable()
    {
        PitfallManager.Instance.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
        {
            PlayerManager.Instance.localPlayer.SendGameOver(GameResult.Lose);

            _renderer.SetActive(true);

            for (int i = 0; i < relatedFloorColliders.Length; i++)
            {
                relatedFloorColliders[i].enabled = false;
            }
        }
    }
}