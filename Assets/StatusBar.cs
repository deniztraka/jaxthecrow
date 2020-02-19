using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Crow Crow;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Crow.OnStaminaChanged.AddListener(UpdateStatus);
        slider.maxValue = Crow.MaxStamina;
    }

    public void UpdateStatus()
    {        
        slider.value = Crow.Stamina;
    }
}

