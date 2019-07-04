#pragma warning disable 649
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class ItemTooltip : MonoBehaviour
{

    #region Singleton

    public static ItemTooltip instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion


    [SerializeField] Text itemNameText;
    [SerializeField] Text itemSlotText;
    [SerializeField] Text itemStatsText;

    private StringBuilder sb = new StringBuilder();

    private void Start()
    {
        HideTooltip();
    }
    // Показываем тултип
    public void ShowTooltip(InventorySlot slot)
    {
        Equipment equipment = slot.item as Equipment;
        if (equipment != null)
        {
            ShowTooltip(equipment);
        }
    }
    private void ShowTooltip(Equipment item)
    {
        itemNameText.text = item.name;
        itemSlotText.text = item.equipSlot.ToString();

        sb.Length = 0;
        AddStat(item.armorMod, "Armor");
        AddStat(item.damageMod, "Damage");

        itemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }
    // Скрываем тултип
    public void HideTooltip(InventorySlot slot)
    {

        HideTooltip();
    }
    private void HideTooltip()
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
