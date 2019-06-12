using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{

    public Text nameText;
    public Text valueText;


    private void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];
    }


}
