using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image iconImage;
    
    private UpgradeData upgradeData;
    private int optionIndex;

    public event Action<int> OnSelected;

    public void Setup(
        UpgradeData data,
        int index)
    {
        upgradeData = data;
        optionIndex = index;

        nameText.text = data.DisplayName;
        descriptionText.text = data.Description;
        iconImage.sprite = data.Icon;
    }

    public void Select()
    {
        OnSelected?.Invoke(optionIndex);
    }
}