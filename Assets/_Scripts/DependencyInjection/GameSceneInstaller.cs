using UnityEngine;
using Zenject;
using PhotonTest.Input;
using PhotonTest.Weapons.Bullets;
using PhotonTest.Weapons;
using PhotonTest.Signals;
using UnityEngine.InputSystem.UI;

namespace PhotonTest.DependencyInjection 
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private BulletDecalManager _bulletDecalManager;
        [SerializeField] private InputSystemUIInputModule _inputModel;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputModel);
            Container.BindInstance(_bulletDecalManager);

            //This command creates and instance of InputLogger and automatically binds it all the zenject interfaces
            //implemented inside InputLogger
            Container.BindInterfacesAndSelfTo<InputLogger>().AsSingle();

            Container.DeclareSignal<WeaponEquippedSignal>().OptionalSubscriber();
            Container.DeclareSignal<PlayerDisconnectedSignal>().OptionalSubscriber();
            Container.DeclareSignal<ScoreChangedSignal>().OptionalSubscriber();
            Container.DeclareSignal<LocalPlayerScoreSetSignal>().OptionalSubscriber();
            Container.DeclareSignal<DecalPlacedSignal>().OptionalSubscriber();
        }
    }
}