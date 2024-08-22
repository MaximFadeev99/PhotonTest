using Photon.Pun;

namespace PhotonTest.Signals 
{
    public class LocalPlayerScoreSetSignal
    {
        public readonly string Nickname;
        public readonly int Score;

        public LocalPlayerScoreSetSignal(int score)
        {
            Nickname = PhotonNetwork.LocalPlayer.NickName;
            Score = score;
        }
    }
}