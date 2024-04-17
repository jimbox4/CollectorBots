using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] Radar _radar;
    [Header("Bots")]
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botSpawnPoint;
    [Header("Crystal")]
    [SerializeField] private LayerMask _crystalLayer;


    [NonSerialized] public CrystalStorage CrystalStorage = new CrystalStorage();

    [NonSerialized] public Queue<Crystal> Crystals = new Queue<Crystal>();
    [NonSerialized] public List<Crystal> GivenCrystals = new List<Crystal>();

    private List<Bot> _bots = new List<Bot>();

    public void Awake()
    {
        SpawnBots(3);
    }

    private void Start()
    {
        StartTasks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot) && bot.TryCollectCrystal(out Crystal crystal))
        {
            GivenCrystals.Remove(crystal);
            CrystalStorage.AddValue(crystal.Collect());
        }
    }

    private void StartTasks()
    {
        _radar.ScanArea().Forget();
        UpdateBots().Forget();
    }

    

    private async UniTaskVoid UpdateBots()
    {
        while (enabled)
        {
            if (Crystals.Count > 0 && _bots.Count > 0)
            {
                foreach (Bot bot in _bots)
                {
                    if (bot.HasCrystalRef == false && Crystals.Count > 0)
                    {
                        Crystal crystal = Crystals.Dequeue();
                        GivenCrystals.Add(crystal);
                        bot.SetCrystalRef(crystal);
                    }
                }
            }

            await UniTask.Delay(1000);
        }
    }

    private void SpawnBots(int count)
    {
        if (count <= 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var bot = Instantiate(_botPrefab, _botSpawnPoint.position, Quaternion.identity);
            bot.Initialize(transform.position);
            _bots.Add(bot);
        }
    }
}
