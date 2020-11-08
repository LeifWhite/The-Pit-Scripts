using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Target target;
    void Start()
    {
        SetMaxHealth();
    }
    

    public Slider slider;

    public void SetMaxHealth()
    {
        slider.maxValue = target.health;
        slider.value = target.health;
    }

	void Update()
	{
        slider.value = target.health;
    }

	public void SetHealth()
    {
        slider.value = target.health;
    }
    
}
