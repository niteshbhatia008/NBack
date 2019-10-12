using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// 選ばれたパネルが行うイベントまとめ
/// パネルにアタッチ (要プレファブ化)
/// </summary>
public class PanelSelectedEvent : MonoBehaviour
{
    [SerializeField, Header("パネルを光らせる")]
    Material m_lightPanel;

    [SerializeField,Header("正解音")]
    AudioClip m_correctSE;

    [SerializeField,Header("不正解音")]
    AudioClip m_wrongSE;

    MeshRenderer m_thisObjMeshRenderer;
    Material m_thisObjMaterial;

    AudioSource m_panelAudio;

    float m_blinkInterval = 1;

    void Start()
    {
        //最初に全部取ってきとく
        m_thisObjMeshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        m_thisObjMaterial = m_thisObjMeshRenderer.material;
        m_panelAudio = this.gameObject.GetComponent<AudioSource>();
    }

    //正解音
    public void CorrectSoundPlay()
    {
        m_panelAudio.clip = m_correctSE;
        m_panelAudio.Play();
    }
    
    //不正解音
    public void WrongSoundPlay()
    {
        m_panelAudio.clip = m_wrongSE;
        m_panelAudio.Play();
    }

    //パネル点滅
    public void BlinkPanel()
    {
        StartCoroutine(BlinkPanelCoroutine());
    }

    IEnumerator BlinkPanelCoroutine()
    {
        //光が消えるまで操作できないようにするならここにその処理を書く

        m_thisObjMeshRenderer.material = m_lightPanel;
        yield return new WaitForSeconds(m_blinkInterval);
        m_thisObjMeshRenderer.material = m_thisObjMaterial;
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(PanelSelectedEvent))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("選ばれたパネルが行うイベントまとめ", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}
