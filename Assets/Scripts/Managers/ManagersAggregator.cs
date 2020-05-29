using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public static class ManagersAggregator
    {
        private static readonly Dictionary<Type, IManager> ManagersDictionary = new Dictionary<Type, IManager>();

        public static T Get<T>() where T : IManager
        {
            var key = typeof(T);
            if (ManagersDictionary.ContainsKey(key) == false)
            {
                Debug.LogError($"{key} not registered.");
                throw new InvalidOperationException();
            }

            return (T) ManagersDictionary[key];
        }

        public static void Register(IManager newManager)
        {
            var key = newManager.GetType();
            if (ManagersDictionary.ContainsKey(key))
            {
                Debug.LogError($"{key} manager already registered");
                return;
            }
        
            ManagersDictionary.Add(key, newManager);
        }

        public static void Unregister(IManager manager)
        {
            var key = manager.GetType();
            if (ManagersDictionary.ContainsKey(key) == false)
            {
                Debug.LogError($"{key} is not registered.");
                return;
            }

            ManagersDictionary.Remove(key);
        }
    }
}
