using UnityEngine;

public class InfoGizmo : MonoBehaviour
{
    enum Shape
    {
        None,
        WireSphere,
        WireCube,
        Sphere,
        Cube,
    }

    [SerializeField] private Shape _shape = Shape.None;
    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _radius = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        switch (_shape)
        {
            case Shape.None:
                break;
            case Shape.WireSphere:
                Gizmos.DrawWireSphere(transform.position, _radius);
                break;
            case Shape.WireCube:
                Gizmos.DrawWireCube(transform.position, new Vector3(_radius, _radius,_radius));
                break;
            case Shape.Sphere:
                Gizmos.DrawSphere(transform.position, _radius);
                break;
            case Shape.Cube:
                Gizmos.DrawCube(transform.position, new Vector3(_radius, _radius, _radius));
                break;
            default:
                break;
        }
    }
}
