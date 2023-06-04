using Player;

namespace UI.Buttons
{
    public sealed class ShootingButton : GameButton
    {
        private PlayerShooting _playerShooting;

        protected override void OnClick()
        {
            Shoot();
        }

        public void Init(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
        }
        
        private void Shoot()
        {
            _playerShooting.Shoot();
        }
    }
}