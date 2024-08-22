namespace PhotonTest.Signals 
{
    public class JoinClickedSignal
    {
        public string RoomName { get; private set; }

        public JoinClickedSignal(string roomName)
        {
            RoomName = roomName;
        }
    }
}