using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        _rectTransform.LookAt(_camera.transform.position);
    }
}
