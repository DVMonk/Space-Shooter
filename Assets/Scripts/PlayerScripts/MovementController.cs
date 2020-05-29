using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.5f;
        [SerializeField] private float _speedMultiplier = 1.5f;

        private GameManager _gameManager;
        private PowerupsController _powerupsController;
        private Player _player;

        private void Start()
        {
            _gameManager = ManagersAggregator.Get<GameManager>();

            _powerupsController = GetComponent<PowerupsController>();
            _powerupsController.SpeedMultiplierStateChanges += ChangeSpeed;

            _player = GetComponent<Player>();
        }

        private void Update()
        {
            SetupPlayerMovement();
        }

        private void SetupPlayerMovement()
        {
            float horizontalInput;
            float verticalInput;

            if ( _gameManager.GameMode.Equals(GameConstants.SinglePlayer) ) //Both type of control
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                switch (_player.PlayerNumber)
                {
                    case 0: //wasd
                        horizontalInput = Input.GetAxis("Horizontal_1");
                        verticalInput = Input.GetAxis("Vertical_1");
                        break;
                    default: //arrows
                        horizontalInput = Input.GetAxis("Horizontal_2");
                        verticalInput = Input.GetAxis("Vertical_2");
                        break;
                }
            }

            var direction = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(direction * (_speed * Time.deltaTime));

            SetupMovementBounds();
        }

        private void SetupMovementBounds()
        {
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            var yBounded = Mathf.Clamp(yPos, -4, 4);

            transform.position = new Vector3(xPos, yBounded, 0);

            if (xPos >= 11.3f)
            {
                transform.position = new Vector3(-11.3f, yBounded, 0);
            }
            else if (xPos <= -11.3f)
            {
                transform.position = new Vector3(11.3f, yBounded, 0);
            }
        }

        private void ChangeSpeed(bool isBoostActive)
        {
            if (isBoostActive)
            {
                _speed *= _speedMultiplier;
            }
            else
            {
                _speed /= _speedMultiplier;
            }
        }
    }
}