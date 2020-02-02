using Microsoft.Extensions.Logging;
using Rhisis.Core.Common;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;

namespace Rhisis.World.Game.Chat.Commands
{
    [ChatCommand("/say", AuthorityType.Player)]
    [ChatCommand("/s", AuthorityType.Player)]
    public class SayChatCommand : IChatCommand
    {
        private readonly ILogger<SayChatCommand> _logger;
        private readonly IChatPacketFactory _chatPacketFactory;
        private readonly ITextPacketFactory _textPacketFactory;
        private readonly IWorldServer _worldServer;

        /// <summary>
        /// Creates a new <see cref="SayChatCommand"/> instance.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="chatPacketFactory"></param>
        /// <param name="textPacketFactory"></param>
        /// <param name="worldServer"></param>
        public SayChatCommand(ILogger<SayChatCommand> logger, IChatPacketFactory chatPacketFactory, ITextPacketFactory textPacketFactory, IWorldServer worldServer)
        {
            _logger = logger;
            _chatPacketFactory = chatPacketFactory;
            _textPacketFactory = textPacketFactory;
            _worldServer = worldServer;
        }

        /// <inheritdoc />
        public void Execute(IPlayerEntity player, object[] parameters)
        {
            if (parameters.Length > 1)
            {
                _chatPacketFactory.SendChat(player, string.Join(" ", parameters));
                return;
            }

            string playerName = (string)parameters[0];
            IPlayerEntity playerLookup = _worldServer.GetPlayerEntity(playerName);
            _textPacketFactory.SendSnoop(player, playerLookup == null
                ? $"{playerName} is not online."
                : $"{playerName} is online.");
        }
    }
}