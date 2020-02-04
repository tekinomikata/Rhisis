using System;
using Sylver.Network.Data;

namespace Rhisis.Network.Packets.World.Motion
{
    public class MotionPacket : IPacketDeserializer
    {
        /// <summary>
        /// Gets the motion used.
        /// </summary>
        public int Motion { get; private set; }

        /// <inheritdoc />
        public void Deserialize(INetPacketStream packet)
        {
            Console.WriteLine(BitConverter.ToString(packet.Buffer).Replace("-", " "));
            Motion = packet.Read<int>();
        }
    }
}
