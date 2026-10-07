using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image iconImage;

    private IUpgrade upgrade;
    private int optionIndex;

    public event Action<int> OnSelected;

    public void Setup(
        IUpgrade upgrade,
        int index)
    {
        this.upgrade = upgrade;
        optionIndex = index;

        nameText.text = upgrade.Data.DisplayName;
        descriptionText.text = upgrade.Data.Description;
        iconImage.sprite = upgrade.Data.Icon;
        levelText.text = "Level " + (upgrade.Level + 1);
    }

    public void Select()
    {
        OnSelected?.Invoke(optionIndex);
    }
}