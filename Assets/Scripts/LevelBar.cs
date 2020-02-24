using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Crow Crow;
    public GameObject TextObject;

    private Text maxText;
    private Text currentText;
    private Text levelText;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        //Crow.OnLevelChanged.AddListener(UpdateStatus);
        Crow.OnCollectableCountChanged.AddListener(UpdateStatus);
        
        if (TextObject != null)
        {
            maxText = TextObject.transform.Find("Max").GetComponent<Text>();
            currentText = TextObject.transform.Find("Current").GetComponent<Text>();
            levelText = TextObject.transform.Find("Level").GetComponent<Text>();
        }
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        var requiredMaxValue = GetMaxRequiredCollectableCount();
        slider.value = (float)Crow.CollectableCount;        

        if (currentText != null)
        {
            currentText.text = slider.value.ToString();
        }

        if (maxText != null)
        {
            maxText.text = requiredMaxValue.ToString();
        }

        if (levelText != null)
        {
            levelText.text = Crow.Level.ToString();
        }
        slider.maxValue = requiredMaxValue;
        CheckLevelUp();     
    }

    public int GetMaxRequiredCollectableCount()
    {
        return (Crow.Level == 0 ? 1 : Crow.Level) * 10;
    }

    public void CheckLevelUp(){
        if(slider.maxValue == slider.value){
            Crow.LevelUp();
        }
    }
}
