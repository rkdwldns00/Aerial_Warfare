using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Slider speedSlider;
    public Scrollbar heightBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void speedUI(float speed,float maxSpeed)
    {
        speedSlider.value = speed/maxSpeed;
    }

    public void heightUI(float height, float maxHeight)
    {
        heightBar.value = height / maxHeight;
    }
}
