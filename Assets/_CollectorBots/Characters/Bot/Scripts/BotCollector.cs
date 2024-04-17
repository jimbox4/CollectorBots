using System;
using UnityEngine;

[Serializable]
public class BotCollector
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _inteartionRadius;
    [SerializeField] private LayerMask _layerMask;

    public bool TryGetCrystal(out Crystal crystal)
    {
        crystal = null;
        Collider[] colliders = Physics.OverlapSphere(_interactPoint.position, _inteartionRadius, _layerMask) ?? Array.Empty<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out crystal))
            {
                return true;
            }
        }

        return false;
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactPoint.position, _inteartionRadius);
    }
}
