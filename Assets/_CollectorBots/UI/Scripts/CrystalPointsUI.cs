using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CrystalPointsUI : MonoBehaviour
{
    [SerializeField] private Base _base;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _base.CrystalStorage.ValueChanged += UpdateText;
    }

    private void OnDisable()
    {
        _base.CrystalStorage.ValueChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = _base.CrystalStorage.Value.ToString();
    }
}
