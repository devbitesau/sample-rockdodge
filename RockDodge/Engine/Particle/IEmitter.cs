using Microsoft.Xna.Framework;

namespace RockDodge.Engine.Particle;

public interface IEmitter
{
    Vector2 EmitPosition { get; }
}