using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// ランダムにパネルが選ばれる
/// パネルの親にアタッチ
/// </summary>
public class PanelSelectedRandom : Singleton<PanelSelectedRandom>
{
    [SerializeField,Header("スライダーからレベルを取得")]
    LevelGetFromSlider m_levelGetFromSlider;

    [SerializeField, Header("選ばれる間隔を操作")]
    float m_interval;

    List<GameObject> m_tmpListObj = new List<GameObject>();

    //外部から呼び出してリストを操作
    [HideInInspector]
    public List<GameObject> g_listObj = new List<GameObject>();

    float m_randomValue;
 
    void Start()
    {
        //子のオブジェクトを全部リストに入れとく
        foreach(Transform child in this.gameObject.transform)
        {
            m_tmpListObj.Add(child.gameObject);
        }
    }

    //Startボタン押したら呼ばれる
    public void SelectStart()
    {
        StartCoroutine(SelectStartCoroutine());
    }

    //レベルに応じて自動で選ばれる回数を変える
    IEnumerator SelectStartCoroutine()
    {
        for(int i = 0; i < m_levelGetFromSlider.GetLevel(); i++)
        {
            //間隔は後からレベルに応じて変えるかも
            yield return new WaitForSeconds(m_interval);

            //ここでどれが選ばれるかランダムに決める
            m_randomValue = Random.Range(0, m_tmpListObj.Count);

            //選ばれたときのイベント呼び出し
            m_tmpListObj[(int)m_randomValue].GetComponent<PanelSelectedEvent>().BlinkPanel();
            m_tmpListObj[(int)m_randomValue].GetComponent<PanelSelectedEvent>().CorrectSoundPlay();

            //選んだオブジェクトをリスト化する
            g_listObj.Add(m_tmpListObj[(int)m_randomValue]); 
        }

        //自動で選ばれる処理終わったらパネルを選択可能にする
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PanelSelectedRandom))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("ランダムにパネルが選ばれる", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}
