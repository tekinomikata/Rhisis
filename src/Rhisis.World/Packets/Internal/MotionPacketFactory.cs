using Microsoft.Extensions.DependencyInjection;
using Rhisis.Core.DependencyInjection;
using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.World.Game.Entities;

namespace Rhisis.World.Packets.Internal
{
    [Injectable(ServiceLifetime.Singleton)]
    internal class MotionPacketFactory : PacketFactoryBase, IMotionPacketFactory
    {
        /// <inheritdoc />
        public void Send(IPlayerEntity player, int motion)
        {
            using var packet = new FFPacket();

            packet.StartNewMergedPacket(player.Id, SnapshotType.MOTION);
            packet.Write(motion);

            SendToVisible(packet, player, sendToPlayer: true);
        }

        /// <inheritdoc />
        public void SendTo(IWorldEntity fromEntity, IPlayerEntity toPlayer, int motion)
        {
            using var packet = new FFPacket();

            packet.StartNewMergedPacket(fromEntity.Id, SnapshotType.MOTION);
            packet.Write(motion);

            SendToPlayer(toPlayer, packet);
        }
    }
}
