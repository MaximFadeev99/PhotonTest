namespace PhotonTest.Signals 
{
    public class SceneLoadedSignal
    {
        public string SceneName { get; private set; }

        public SceneLoadedSignal(string sceneName) 
        {
            SceneName = sceneName;
        }
    }
}