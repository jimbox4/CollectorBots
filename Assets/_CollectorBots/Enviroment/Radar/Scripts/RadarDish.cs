using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class RadarDish : MonoBehaviour
{
    [SerializeField] private float _radarRadius;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botSpawnPoint;
    [SerializeField] private LayerMask _crystalLayer;
    [SerializeField] private int _cristalPoints;
    [SerializeField] private RadarDishAnimator _animator;

    private Queue<Crystal> _crystals = new Queue<Crystal>();
    private List<Bot> _bots = new List<Bot>();

    public void Awake()
    {
        var bot = Instantiate(_botPrefab, _botSpawnPoint.position, Quaternion.identity);
        bot.Initialize(transform.position);
        _bots.Add(bot);
    }

    private void Start()
    {
        StartTasks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot) && bot.TryCollectCrystal(out Crystal crystal))
        {
            _cristalPoints += crystal.Collect();
        }
    }

    private void StartTasks()
    {
        ScanArea().Forget();
        UpdateBots().Forget();
    }

    private async UniTaskVoid ScanArea()
    {
        while (enabled)
        {
            _animator.SetIsSearch(true);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radarRadius, _crystalLayer);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Crystal crystal))
                {
                    _crystals.Enqueue(crystal);
                }
            }

            await UniTask.Delay(1000);
            _animator.SetIsSearch(false);
            await UniTask.Delay(2000);
        }
    }

    private async UniTaskVoid UpdateBots()
    {
        while (enabled)
        {
            if (_crystals.Count > 0)
            {
                foreach (Bot bot in _bots)
                {
                    if (bot.HasCrystalRef == false)
                    {
                        bot.SetCrystalRef(_crystals.Dequeue());
                    }
                }
            }

            await UniTask.Delay(1000);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radarRadius);
    }
}
