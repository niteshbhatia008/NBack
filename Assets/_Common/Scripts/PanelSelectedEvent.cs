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
    Material lightPanel;

    [SerializeField,Header("正解音")]
    AudioClip correctSE;

    [SerializeField,Header("不正解音")]
    AudioClip wrongSE;

    MeshRenderer m_thisObjMeshRenderer;
    Material m_thisObjMaterial;

    AudioSource panelAudio;

    float m_blinkInterval = 1;

    void Start()
    {
        //最初に全部取ってきとく
        m_thisObjMeshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        m_thisObjMaterial = m_thisObjMeshRenderer.material;

    }

    //正解音
    public void CorrectSoundPlay()
    {
        panelAudio.clip = correctSE;
        panelAudio.Play();
    }
    
    //不正解音
    public void WrongSoundPlay()
    {
        panelAudio.clip = wrongSE;
        panelAudio.Play();
    }

    //パネル点滅
    public void BlinkPanel()
    {
        StartCoroutine(BlinkPanelCoroutine());
    }

    IEnumerator BlinkPanelCoroutine()
    {
        //光が消えるまで操作できないようにするならここにその処理を書く

        m_thisObjMeshRenderer.material = lightPanel;
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
