using Microsoft.Extensions.Logging;
using Rhisis.Core.DependencyInjection;
using Rhisis.World.Game.Chat;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rhisis.World.Systems.Motion
{
    public class MotionSystem : IMotionSystem
    {
        private readonly ILogger<MotionSystem> _logger;
        private readonly IMotionPacketFactory _motionPacketFactory;

        /// <summary>
        /// Creates a new <see cref="MotionSystem"/> instance.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="motionPacketFactory"></param>
        public MotionSystem(ILogger<MotionSystem> logger, IMotionPacketFactory motionPacketFactory)
        {
            _logger = logger;
            _motionPacketFactory = motionPacketFactory;
        }

        /// <inheritdoc />
        public void Motion(IPlayerEntity player, int motion)
        {
            _logger.LogTrace($"Received motion id {motion}(0x{motion:X8})...");
            //_motionPacketFactory.Send(player, motion);
        }
    }
}