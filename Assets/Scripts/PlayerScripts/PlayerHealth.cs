using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHealth : Health
    {
    
        [SerializeField] private GameObject _shieldPrefab;
    
        private bool _isShieldActive = false;
        private PowerupsController _powerupsController;
    
        private new void Start()
        {
            base.Start();
            _powerupsController = GetComponent<PowerupsController>();
            _powerupsController.ShieldStateChanges += ActivateShield;
        }
    
        public override void Damage()
        {
            if (_isShieldActive)
            {
                ActivateShield(false);
                return;
            }

            base.Damage();
        }
    
    
        private void ActivateShield(bool isActive)
        {
            _isShieldActive = isActive;
            _shieldPrefab.SetActive(isActive);
        }

    }
}