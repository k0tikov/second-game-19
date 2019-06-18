using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class ItemTooltip : MonoBehaviour
{

    [SerializeField] Text itemNameText;
    [SerializeField] Text itemSlotText;
    [SerializeField] Text itemStatsText;

    private StringBuilder sb = new StringBuilder();


    public void ShowTooltip(Equipment item)
    {
        itemNameText.text = item.name;
        itemSlotText.text = item.equipSlot.ToString();

        sb.Length = 0;
        AddStat(item.armorMod, "Armor");
        AddStat(item.damageMod, "Damage");

        itemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void AddStat(int value, string statName)
    {
        if (value != 0)

            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
    }
}
