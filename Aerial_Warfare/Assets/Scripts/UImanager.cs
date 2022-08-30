using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Slider speedSlider;
    public Scrollbar heightBar;
    public Slider team1HP;
    public Slider team2HP;
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

    public void team1Slider(float value)
    {
        team1HP.value = value;
    }
    public void team2Slider(float value)
    {
        team2HP.value = value;
    }
}
