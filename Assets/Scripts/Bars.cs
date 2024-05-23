using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount=(float)currentValue / (float)maxValue;
        valueText.text= currentValue.ToString() + "/" + maxValue.ToString();
    }
}
