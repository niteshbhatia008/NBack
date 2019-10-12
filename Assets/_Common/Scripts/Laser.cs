using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField]
    GameObject handAnchor;

    LineRenderer lineRenderer;
    Vector3 hitPos;
    Vector3 tmpPos;

    float lazerDistance = 10f;
    float lazerStartPointDistance = 0.15f;
    float lineWidth = 0.01f;

    void Reset()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }

    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }


    void Update()
    {
        OnRay();
    }

    void OnRay()
    {
        Vector3 direction = handAnchor.transform.forward * lazerDistance;
        Vector3 rayStartPosition = handAnchor.transform.forward * lazerStartPointDistance;
        Vector3 pos = handAnchor.transform.position;
        RaycastHit hit;
        Ray ray = new Ray(pos + rayStartPosition, handAnchor.transform.forward);

        lineRenderer.SetPosition(0, pos + rayStartPosition);

        if (Physics.Raycast(ray, out hit, lazerDistance))
        {
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);
        }
        else
        {
            lineRenderer.SetPosition(1, pos + direction);
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);

    }
}