using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Crystal : MonoBehaviour
{
    [SerializeField] private int _worth;

    private Collider _collider;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }

    public void EnableCollider(bool isEnable)
    {
        _collider.enabled = isEnable;
    }

    public int Collect()
    {
        Destroy(gameObject);

        return _worth;
    }
}
