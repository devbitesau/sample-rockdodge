using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public struct Tile
{
    public int TileId { get; set; }
    public Vector2 GridPosition { get; set; }
    public bool IsCollidable { get; set; }
}