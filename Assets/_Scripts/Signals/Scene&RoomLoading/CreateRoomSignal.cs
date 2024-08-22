namespace PhotonTest.Signals 
{
    public class CreateRoomSignal
    {
        public string RoomName { get; private set; }

        public CreateRoomSignal(string roomName)
        {
            RoomName = roomName;
        }
    }
}