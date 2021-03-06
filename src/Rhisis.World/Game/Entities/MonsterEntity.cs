﻿using Rhisis.Core.Common;
using Rhisis.Core.Structures.Game;
using Rhisis.World.Game.Behaviors;
using Rhisis.World.Game.Components;
using Rhisis.World.Game.Maps.Regions;

namespace Rhisis.World.Game.Entities
{
    /// <summary>
    /// Describes the Monster entity.
    /// </summary>
    public class MonsterEntity : WorldEntity, IMonsterEntity
    {
        /// <inheritdoc />
        public override WorldEntityType Type => WorldEntityType.Monster;

        /// <inheritdoc />
        public IMapRespawnRegion Region { get; set; }

        /// <inheritdoc />
        public MovableComponent Moves { get; set; }

        /// <inheritdoc />
        public IBehavior Behavior { get; set; }

        /// <inheritdoc />
        public TimerComponent Timers { get; set; }

        /// <inheritdoc />
        public FollowComponent Follow { get; set; }

        /// <inheritdoc />
        public InteractionComponent Interaction { get; set; }

        /// <inheritdoc />
        public BattleComponent Battle { get; set; }

        /// <inheritdoc />
        public HealthComponent Health { get; set; }

        /// <inheritdoc />
        public StatisticsComponent Statistics { get; set; }

        /// <inheritdoc />
        public AttributeComponent Attributes { get; set; }

        /// <inheritdoc />
        public MoverData Data { get; set; }

        /// <summary>
        /// Creates a new <see cref="MonsterEntity"/> instance.
        /// </summary>
        /// <param name="context"></param>
        public MonsterEntity()
        {
            Moves = new MovableComponent();
            Timers = new TimerComponent();
            Follow = new FollowComponent();
            Interaction = new InteractionComponent();
            Battle = new BattleComponent();
            Health = new HealthComponent();
            Attributes = new AttributeComponent();
        }
    }
}
