using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// カウントダウン
/// Text自身にはアタッチしたらダメ
/// </summary>
public class CountDown : MonoBehaviour
{
    [SerializeField, Header("カウントダウンさせる")]
    Text m_countDownText;

    [SerializeField, Header("アニメーション終わりにカウントダウンスタート")]
    Animation m_animation;

    [SerializeField, Header("カウントダウンの音")]
    AudioClip m_countDownSE;

    [SerializeField, Header("開始音")]
    AudioClip m_startSE;

    [SerializeField, Header("カウントダウンが終わった後の処理をここに登録")]
    UnityEvent m_onStartNBack;

    AudioSource m_countDownAudio;

    void Start()
    {
        m_countDownAudio = this.gameObject.GetComponent<AudioSource>();
    }

    //カウントダウンをスタートさせる 音も一緒に鳴らす
    public void StartCountDown()
    {
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        //アニメーション終わるまで待機
        m_animation.Play();
        yield return new WaitWhile(() => m_animation.isPlaying);
       
        int cnt = 3;

        while (cnt > 0)
        {
            DOTween.Sequence()
                .OnStart(() =>
                {
                    m_countDownAudio.clip = m_countDownSE;
                    m_countDownAudio.Play();
                    m_countDownText.text = cnt.ToString();
                    m_countDownText.gameObject.SetActive(true);
                })
                .Join(m_countDownText.DOFade(0, 0.6f))
                .OnComplete(() =>
                {
                    cnt--;
                    Color textColor = m_countDownText.color;
                    textColor.a = 1.0f;
                    m_countDownText.color = textColor;
                    m_countDownText.gameObject.SetActive(false);
                });

            yield return new WaitForSeconds(1.0f);

        }

        m_countDownText.text = "GO";
        DOTween.Sequence()
                .OnStart(() =>
                {
                    //m_countDownAudio.clip = m_startSE;
                    //m_countDownAudio.Play();
                    m_countDownText.gameObject.SetActive(true);
                })
                .Join(m_countDownText.DOFade(0, 1.0f))
                .OnComplete(() =>
                {
                    m_countDownText.gameObject.SetActive(false);
                    // 終わったらやりたい処理詰め込む
                    m_onStartNBack.Invoke();
                });
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CountDown))]
    public class ExplanationWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("カウントダウンとその後の処理登録", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

        }
    }
#endif
}
