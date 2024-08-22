using Photon.Realtime;
using System.Collections.Generic;

namespace PhotonTest.Signals
{
    public class RoomUpdateSignal
    {
        public IReadOnlyList<RoomInfo> RoomData { get; private set; }

        public RoomUpdateSignal(IReadOnlyList<RoomInfo> roomData)
        {
            RoomData = roomData;
        }
    }
}