using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    #region Singleton

    public static StatPanel instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion


    // наши актуальные характеристики
    public Stat[] stats;

    // родительское ячеек характеристик
    public Transform statPanelParent;

    // массив характеристик с панели
    StatDisplay[] statDisplays;

    public string[] statNames;

    void Start()
    {
  
        statDisplays = statPanelParent.GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    // из PlayerStats сюда летят типы актуальных статов
    public void SetStats(params Stat[] charStats)
    {
        stats = charStats;
        Debug.Log("SetStats");
    }


    // обновляем значения характеристик в панели
    public void UpdateStatValues()
    {
        Debug.Log("UpdateStatValues");
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].valueText.text = stats[i].GetValue().ToString();
            Debug.Log("UpdateStatValuesTest");
        }
    }

    public void UpdateStatNames()
    {
      for (int i = 0; i < statNames.Length; i++)
       {
           statDisplays[i].nameText.text = statNames[i];
        }
    }

}
