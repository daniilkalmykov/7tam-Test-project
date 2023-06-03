using Photon.Pun;

namespace UI.Buttons
{
    public sealed class JoinRoomButton : LobbyButton
    {
        protected override void OnClick()
        {
            if (CanBePressed())
                JoinRoom();
        }

        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom(GetRoomName());
        }
    }
}