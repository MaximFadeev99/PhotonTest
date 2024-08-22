namespace PhotonTest.Signals 
{
    public class ScoreChangedSignal
    {
        public string PlayerNickname { get; private set; }
        public int ScoreDelta { get; private set; }

        public ScoreChangedSignal(string playerNickname, int scoreDelta)
        {
            PlayerNickname = playerNickname;
            ScoreDelta = scoreDelta;
        }
    }
}