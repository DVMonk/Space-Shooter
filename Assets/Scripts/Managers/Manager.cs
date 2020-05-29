using System;
using UnityEngine;

namespace Managers
{
    public abstract class Manager : MonoBehaviour, IManager
    {
        private void Awake()
        {
            ManagersAggregator.Register(this);
        }

        private void OnDestroy()
        {
            ManagersAggregator.Unregister(this);
        }
    }
}