using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// オブジェクト遮蔽レーザーの実装
/// </summary>
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

    bool isOnRay;

    [HideInInspector]
    public GameObject hitObj;

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
            hitObj = hit.collider.gameObject;
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);
            isOnRay = true;
        }
        else
        {
            lineRenderer.SetPosition(1, pos + direction);
            isOnRay = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);
    }

    //外部からレイの状態取ってくる
    public  bool GetIsRay()
    {
        return isOnRay;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Laser))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("オブジェクト遮蔽レーザーの実装", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}