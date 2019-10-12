using UnityEngine.UI;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    [SerializeField]
    LevelGetFromSlider _levelGetFromSlider;
    
    Text levelText;

    void Start()
    {
        levelText = this.gameObject.GetComponent<Text>();
    }

    public void LevelTextChange()
    {
        levelText.text = _levelGetFromSlider.GetLevel().ToString();
    }

}
