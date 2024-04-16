using UnityEngine;

public class RadarDishAnimator : MonoBehaviour
{
    private static int IsSearch = Animator.StringToHash(nameof(IsSearch));

    [SerializeField] private Animator _animator;

    public void SetIsSearch(bool isSearch)
    {
        _animator.SetBool(IsSearch, isSearch);
    }
}
