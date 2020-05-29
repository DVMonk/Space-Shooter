
    using System;
    using System.Collections;
    using UnityEngine;

    namespace PlayerScripts
    {
        public class PowerupsController: MonoBehaviour
        {
            
            [SerializeField] private float _powerUpTime = 5.0f;
            
            public event Action<bool> TripleShotStateChanges = delegate {  };
            public event Action<bool> SpeedMultiplierStateChanges = delegate {  };
            public event Action<bool> ShieldStateChanges = delegate {  };
            
            public void ActivateTripleShot()
            {
                TripleShotStateChanges(true);
                StartCoroutine(TripleShotCountdown());
            }

            public void ActivateSpeedBoost()
            {
                SpeedMultiplierStateChanges(true);
                StartCoroutine(SpeedBoostCountdown());
            }

            public void ActivateShield()
            {
                ShieldStateChanges(true);
                StartCoroutine(ShieldPowerupCountdown());
            }

            private IEnumerator TripleShotCountdown()
            {
                yield return new WaitForSeconds(_powerUpTime);
                TripleShotStateChanges(false);
            }

            private IEnumerator SpeedBoostCountdown()
            {
                yield return new WaitForSeconds(_powerUpTime);
                SpeedMultiplierStateChanges(false);
            }
            
            private IEnumerator ShieldPowerupCountdown()
            {
                yield return new WaitForSeconds(_powerUpTime);
                ShieldStateChanges(false);
            }

        }
    }