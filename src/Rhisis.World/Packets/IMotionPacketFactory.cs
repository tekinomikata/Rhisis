using Rhisis.World.Game.Entities;

namespace Rhisis.World.Packets
{
    public interface IMotionPacketFactory
    {
        /// <summary>
        /// Sends a motion packet to all players around the current player.
        /// </summary>
        /// <param name="player">Player</param>
        /// <param name="motion">Motion.</param>
        void Send(IPlayerEntity player, int motion);

        /// <summary>
        /// Sends a motion to a given player only.
        /// </summary>
        /// <remarks>
        /// Used in NPC oral text.
        /// </remarks>
        /// <param name="fromEntity">Entity.</param>
        /// <param name="toPlayer">Destination player.</param>
        /// <param name="motion">Motion.</param>
        void SendTo(IWorldEntity fromEntity, IPlayerEntity toPlayer, int motion);
    }
}
