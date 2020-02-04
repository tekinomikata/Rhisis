using Rhisis.World.Game.Entities;

namespace Rhisis.World.Systems.Motion
{
    public interface IMotionSystem
    {
        /// <summary>
        /// Sends a motion to the game.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="motion">The selected motion</param>
        void Motion(IPlayerEntity player, int motion);
    }
}