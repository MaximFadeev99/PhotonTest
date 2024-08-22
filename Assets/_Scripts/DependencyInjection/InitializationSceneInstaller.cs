using PhotonTest.UI.Chatting;
using UnityEngine;
using Zenject;

namespace PhotonTest.DependencyInjection 
{
    public class InitializationSceneInstaller : MonoInstaller
    {
        [SerializeField] private Chat _chat;

        public override void InstallBindings()
        {
            Container.ParentContainers[0].Rebind<Chat>().FromInstance(_chat);
        }
    }
}