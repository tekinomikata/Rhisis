using System;
using Microsoft.Extensions.Logging;
using Rhisis.Core.Common;
using Rhisis.Core.Data;
using Rhisis.Database;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;

namespace Rhisis.World.Game.Chat.Commands
{
    [ChatCommand("/say", AuthorityType.Player)]
    [ChatCommand("/s", AuthorityType.Player)]
    public class SayChatCommand : IChatCommand
    {
        private readonly ILogger<SayChatCommand> _logger;
        private readonly IDatabase _database;
        private readonly IChatPacketFactory _chatPacketFactory;
        private readonly ITextPacketFactory _textPacketFactory;
        private readonly IWorldServer _worldServer;

        /// <summary>
        /// Creates a new <see cref="SayChatCommand"/> instance.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="database"></param>
        /// <param name="chatPacketFactory"></param>
        /// <param name="textPacketFactory"></param>
        /// <param name="worldServer"></param>
        public SayChatCommand(ILogger<SayChatCommand> logger, IDatabase database, IChatPacketFactory chatPacketFactory, ITextPacketFactory textPacketFactory, IWorldServer worldServer)
        {
            _logger = logger;
            _chatPacketFactory = chatPacketFactory;
            _textPacketFactory = textPacketFactory;
            _worldServer = worldServer;
            _database = database;
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
            if (string.IsNullOrWhiteSpace(playerName))
                return;

            if (playerName.Equals(player.Object.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                _textPacketFactory.SendDefinedText(player, (DefineText)348);
                return;
            }

            IPlayerEntity playerLookup = _worldServer.GetPlayerEntity(playerName);
            if (playerLookup == null)
            {
                var characterExists = _database.Characters.HasAny(x => x.Name.IndexOf(playerName, StringComparison.InvariantCultureIgnoreCase) > -1);
                _textPacketFactory.SendDefinedText(player, characterExists 
                    ? (DefineText)1297 
                    : (DefineText)349, playerName);
                return;
            }

            // ToDO: start chat
            _textPacketFactory.SendSnoop(player, $"{playerName} is online.");
        }
    }
}