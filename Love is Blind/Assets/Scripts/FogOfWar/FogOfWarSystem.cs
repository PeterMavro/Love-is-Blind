using UnityEngine;

public class FogOfWarSystem : MonoBehaviour
{
 	public Transform fogOfWarPlane;
	public Transform player;
	public LayerMask fogLayer;
    [Range(0f, 20f)]
	public float radius = 5f;
    [Range(0f, 3f)]
    public float blurMultiplier = 1f;
    public Color fogColor;

	private float RadiusSqr { get { return radius * radius; }}
	
	private Mesh _mesh;
	private Vector3[] _vertices;
	private Color[] _colors;
    private Transform _catchedTransform;
    private Material _fogOfWarPlaneMat;
    private int _fogColorShaderParamId;

    private void Awake()
    {
        _catchedTransform = transform;

        _fogOfWarPlaneMat = fogOfWarPlane.GetComponent<Renderer>().material;
    }

    private void Start ()
    {
		Initialize();
	}
	
	private void Update ()
    {
		Ray r = new Ray(_catchedTransform.position, player.position - _catchedTransform.position);
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
			for (int i = 0; i < _vertices.Length; i++)
            {
				Vector3 v = fogOfWarPlane.TransformPoint(_vertices[i]);
				float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < RadiusSqr)
                {
                    float alpha = Mathf.Min(_colors[i].a, dist * blurMultiplier / RadiusSqr);
                    _colors[i].a = alpha;
                }
                else
                {
                    _colors[i] = fogColor;
                }
			}
			UpdateColor();
		}
	}
	
	private void Initialize()
    {
		_mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		_vertices = _mesh.vertices;
		_colors = new Color[_vertices.Length];

		for (int i=0; i < _colors.Length; i++)
			_colors[i] = fogColor;

        CalculateShaderPropertyIds();

        UpdateColor();
	}

    private void CalculateShaderPropertyIds()
    {
        _fogColorShaderParamId = Shader.PropertyToID("_TintColor");
    }

    private void UpdateColor()
    {
		_mesh.colors = _colors;
	}

    public void SetActive(bool active)
    {
        fogOfWarPlane.gameObject.SetActive(active);

        enabled = active;
    }

    public void SetFogColor(Color color)
    {
        fogColor = color;

        SetFogOfWarMatColor(color);
    }

    private void SetFogOfWarMatColor(Color color)
    {
        _fogOfWarPlaneMat.SetColor(_fogColorShaderParamId, color);
    }
}
