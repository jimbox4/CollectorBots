using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int _worth;

    public Vector3 Position => transform.position;

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }

    public int Collect()
    {
        Destroy(gameObject);

        return _worth;
    }
}
