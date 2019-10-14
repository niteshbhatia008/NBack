using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// クリアの処理まとめ
/// Text自身にはアタッチしたらダメ
/// </summary>
public class Clear : MonoBehaviour
{
    [SerializeField, Header("クリアの文表示言")]
    Text m_clearText;

    [SerializeField, Header("タイトルバックのボタン表示")]
    Image m_backButtonImage;

    [SerializeField, Header("パネルのアニメーション")]
    Animator m_panelAnimator;

    [SerializeField, Header("物理レーザー消去")]
    GameObject physicsLaser;

    [SerializeField, Header("クリア後の処理をここに登録")]
    UnityEvent m_onClear;

  

    //クリア時のテキストが出現 パネルのアニメーションも再生
    public void ClearTextAppear()
    {
        StartCoroutine(ClearTextAppearCoroutine());
    }


    IEnumerator ClearTextAppearCoroutine()
    {
        physicsLaser.SetActive(false);

        //パネルのアニメーション
        m_panelAnimator.SetTrigger("MoveBack");

        //アニメーション終わるまで待機
        yield return new WaitForAnimation(m_panelAnimator, 0);

        DOTween.Sequence()
            .OnStart(() =>
            {
                m_clearText.text = "CLEAR!";
                m_clearText.gameObject.SetActive(true);
                m_backButtonImage.gameObject.SetActive(true);
            })
            .Join(m_clearText.DOFade(1.0f, 2.0f))
            .Join(m_backButtonImage.DOFade(1.0f,2.0f))
            .Join(m_backButtonImage.gameObject.GetComponentInChildren<Text>().DOFade(1.0f,2.0f))
            .OnComplete(() =>
            {
                m_onClear.Invoke();
            });

        yield return null;
    }


    //シーンをリセットして最初の状態に戻す
    public void ResetScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Clear))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("クリア時の処理まとめ", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}
