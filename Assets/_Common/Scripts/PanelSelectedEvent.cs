﻿using System.Collections;
using UnityEngine.Events;
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

    [SerializeField, Header("正解音")]
    AudioClip m_correctSE;

    [SerializeField, Header("不正解音")]
    AudioClip m_wrongSE;

    PanelSelected m_panelSelected;

    LevelGetFromSlider m_levelGetFromSlider;

    Clear m_clear;

    MeshRenderer m_thisObjMeshRenderer;
    Material m_thisObjMaterial;

    AudioSource m_panelAudio;

    float m_blinkInterval = 1;
    bool isDone;

    //何回選ぶか　エンドレスモードがあればここをめちゃくちゃでかい数字に変えればOK
    int m_playTime = 10;
    static int m_selectedCount;

    void Start()
    {
        m_selectedCount = 0;

        //最初に全部取ってきとく
        m_thisObjMeshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        m_thisObjMaterial = m_thisObjMeshRenderer.material;
        m_panelAudio = this.gameObject.GetComponent<AudioSource>();
        m_panelSelected = this.gameObject.GetComponentInParent<PanelSelected>();
        m_levelGetFromSlider = GameObject.Find("Slider").GetComponent<LevelGetFromSlider>();
        m_clear = GameObject.Find("ClearCanvas").GetComponent<Clear>();
    }

    //選んだものが同じかどうか判定して選択カウント増加
    public void IncrementSelectedCount()
    {
        if (m_selectedCount < m_playTime)
        {
            if (m_panelSelected.g_listObj[m_selectedCount] == this.gameObject)
            {
                m_panelSelected.ToNextSelect();
                CorrectSoundPlay();
                m_selectedCount++;
            }
            else
            {
                WrongSoundPlay();
            }
        }
        else
        {
            if (m_panelSelected.g_listObj[m_selectedCount] == this.gameObject)
            {
                CorrectSoundPlay();
                m_selectedCount++;

                if (m_selectedCount == m_playTime + m_levelGetFromSlider.GetLevel())
                {
                    //ここにクリア時の処理
                    m_clear.ClearTextAppear();
                }
            }
            else
            {
                WrongSoundPlay();
            }
        }
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
