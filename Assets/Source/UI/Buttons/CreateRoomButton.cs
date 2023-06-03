using Photon.Pun;
using Photon.Realtime;

namespace UI.Buttons
{
    public sealed class CreateRoomButton : LobbyButton
    {
        protected override void OnClick()
        {
            if (CanBePressed())
                CreateRoom();
        }

        private void CreateRoom()
        {
            PhotonNetwork.CreateRoom(GetRoomName(), new RoomOptions { MaxPlayers = LobbyConnector.MaxPlayers });
        }
    }
}