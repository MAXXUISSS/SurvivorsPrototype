using UnityEngine;
using System.Collections.Generic;
using System;
public class UpgradeSelectionUI : MonoBehaviour
{
    [SerializeField] private UpgradeOptionUI optionPrefab;
    [SerializeField] private Transform optionsContainer;

    private List<UpgradeOptionUI> optionUIs;
    
    public event Action<int> OnUpgradeSelected;

    private void Awake()
    {
        optionUIs = new List<UpgradeOptionUI>();
    }

    public void Show(IReadOnlyList<IUpgrade> upgrades)
    {
        gameObject.SetActive(true);

        foreach (UpgradeOptionUI optionUI in optionUIs)
        {
            Destroy(optionUI.gameObject);
        }

        optionUIs.Clear();

        for (int i = 0; i < upgrades.Count; i++)
        {
            UpgradeOptionUI optionUI =
                Instantiate(optionPrefab, optionsContainer);

            optionUI.Setup(
                upgrades[i].Data,
                i
            );

            optionUI.OnSelected += HandleOptionSelected;

            optionUIs.Add(optionUI);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void HandleOptionSelected(int index)
    {
        Debug.Log("Selected option: " + index);

        OnUpgradeSelected?.Invoke(index);
    }
}