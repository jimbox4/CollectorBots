using System;
using System.Threading.Tasks;
using UnityEngine;

[SelectionBase]
public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(0)] private float _detroyDelay;
    
    protected void DestroyThisObject()
    {
        int miliseconds = (int)_detroyDelay * 1000;
        Task.Delay(miliseconds);
        enabled = false;
        Destroy(gameObject, _detroyDelay + 0.1f);
        enabled = false;
    }
}
