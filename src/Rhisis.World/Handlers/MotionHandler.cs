using Rhisis.Network.Packets;
using Rhisis.Network.Packets.World.Motion;
using Rhisis.World.Client;
using Rhisis.World.Systems.Motion;
using Sylver.HandlerInvoker.Attributes;

namespace Rhisis.World.Handlers
{
    /// <summary>
    /// Handles every packets related to the chat system.
    /// </summary>
    [Handler]
    public sealed class MotionHandler
    {
        private readonly IMotionSystem _motionSystem;

        /// <summary>
        /// Creates a new <see cref="MotionHandler"/> instance.
        /// </summary>
        /// <param name="motionSystem">Motion system.</param>
        public MotionHandler(IMotionSystem motionSystem)
        {
            _motionSystem = motionSystem;
        }

        /// <summary>
        /// Handles a motion request.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        [HandlerAction(PacketType.MOTION)]
        public void OnMotion(IWorldClient client, MotionPacket packet)
        {
            _motionSystem.Motion(client.Player, packet.Motion);
        }
    }
}
