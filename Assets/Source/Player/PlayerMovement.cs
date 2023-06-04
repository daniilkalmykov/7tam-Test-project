using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(float xDirection, float yDirection)
        {
            _characterController.Move(new Vector2(xDirection, yDirection) * (_speed * Time.deltaTime));
        }
    }
}