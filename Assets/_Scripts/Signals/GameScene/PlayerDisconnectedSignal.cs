namespace PhotonTest.Signals 
{
    public class PlayerDisconnectedSignal 
    {
        public readonly string Nickname;

        public PlayerDisconnectedSignal(string nickname)
        {
            Nickname = nickname;
        }
    }
}