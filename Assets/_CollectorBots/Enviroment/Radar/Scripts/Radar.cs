using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Radar
{
    [SerializeField] Base _currentBase;
    [SerializeField] private CrystalSpawner _spawner;
    [Header("Animation")]
    [SerializeField] private RadarDishAnimator _animator;

    public async UniTaskVoid ScanArea()
    {
        while (this != null)
        {
            _animator.SetIsSearch(true);

            List<Crystal> crystals = _spawner.GetCrystals();

            foreach (Crystal crystal in crystals)
            {
                if (_currentBase.Crystals.Contains(crystal) == false && _currentBase.GivenCrystals.Contains(crystal) == false)
                {
                    _currentBase.Crystals.Enqueue(crystal);
                }
            }

            await UniTask.Delay(1000);
            _animator.SetIsSearch(false);
            await UniTask.Delay(2000);
        }
    }
}
