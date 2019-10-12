using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ランダムにパネルが選ばれる
/// </summary>
public class PanelSelectedRandom : Singleton<PanelSelectedRandom>
{
    [SerializeField,Header("スライダーからレベルを取得")]
    LevelGetFromSlider m_levelGetFromSlider;

    [SerializeField, Header("パネルの親から子を全部取得")]
    GameObject panelParent;

    [SerializeField, Header("選ばれる間隔を操作")]
    float m_interval;

    List<GameObject> m_tmpListObj = new List<GameObject>();

    //外部から呼び出してリストを操作
    [HideInInspector]
    public List<GameObject> listObj = new List<GameObject>();

    float m_randomValue;

    
    void Start()
    {
        //子のオブジェクトを全部リストに入れとく
        foreach(Transform child in panelParent.transform)
        {
            m_tmpListObj.Add(child.gameObject);
        }
    }

    void Update()
    {
        //ここでどれが選ばれるかランダムに決める
        m_randomValue = Random.Range(0, m_tmpListObj.Count);
    }

    //レベルに応じて選ばれる回数を変える
    IEnumerator SelectStartCoroutine()
    {
        for(int i = 0; i < m_levelGetFromSlider.GetLevel(); i++)
        {
            //間隔は後からレベルに応じて変えるかも
            yield return new WaitForSeconds(m_interval);
            //m_tmpListObj[(int)m_randomValue].GetComponent<パネルについてるコンポーネント>().いろいろ登録したイベント;

            //選んだオブジェクトをリスト化する
            listObj.Add(m_tmpListObj[(int)m_randomValue]); 
        }
    }
}
