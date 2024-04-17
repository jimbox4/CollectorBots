using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private Vector3 cameraPosition;

    private void FixedUpdate()
    {
        cameraPosition = Camera.main.transform.position;
        _rectTransform.LookAt(cameraPosition);
    }
}
