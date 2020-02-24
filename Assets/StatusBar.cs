using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Crow Crow;
    public GameObject TextObject;

    private Text maxText;
    private Text currentText;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Crow.OnStaminaChanged.AddListener(UpdateStatus);
        slider.maxValue = Crow.MaxStamina;
        if (TextObject != null)
        {
            maxText = TextObject.transform.Find("Max").GetComponent<Text>();
            currentText = TextObject.transform.Find("Current").GetComponent<Text>();
        }
    }

    public void UpdateStatus()
    {
        slider.value = Mathf.FloorToInt(Crow.Stamina);

        if (currentText != null)
        {
            currentText.text = slider.value.ToString();
        }

        if (maxText != null)
        {
            maxText.text = Crow.MaxStamina.ToString();
        }
        
    }
}

