using UnityEngine;
using UnityEngine.UI;

public class LevelGetFromSlider : MonoBehaviour
{
    Slider levelSlider;
    
    void Start()
    {
        levelSlider = this.gameObject.GetComponent<Slider>();
    }

    public int GetLevel()
    {
        return (int)levelSlider.value;
    }
}

