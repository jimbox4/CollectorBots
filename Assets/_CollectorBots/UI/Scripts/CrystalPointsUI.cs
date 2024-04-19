using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CrystalPointsUI : MonoBehaviour
{
    [SerializeField] private CrystalStorage _storage;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _storage.ValueChanged += UpdateText;
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = _storage.Value.ToString();
    }
}
