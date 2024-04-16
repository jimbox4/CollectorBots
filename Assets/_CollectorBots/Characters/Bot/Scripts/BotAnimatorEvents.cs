using System;
using UnityEngine;

public class BotAnimatorEvents : MonoBehaviour
{
    public event Action TakeFrame;

    private void ActivateTakeAction()
    {
        TakeFrame.Invoke();
    }
}
