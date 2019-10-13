using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// レーザーでパネル選択したらどうなるか
/// PhysicsLaserにアタッチ
/// </summary>
public class SelectLaser : MonoBehaviour
{
    Laser m_laser;

    void Start()
    {
        m_laser = this.gameObject.GetComponent<Laser>();
    }

    void Update()
    {
        //オブジェクトに当たった状態でトリガーを引いたらイベント発火
        if(m_laser.GetIsRay() && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            PanelSelectedEvent panelSelectedEvent = m_laser.hitObj.GetComponent<PanelSelectedEvent>();

            if (panelSelectedEvent != null)
            {
                panelSelectedEvent.IncrementSelectedCount();
            }        
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(SelectLaser))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("レーザーでパネル選択したらどうなるか", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}
