using System.Data;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public interface ISpriteAction
{
    public void Update(Sprite sprite, GameTime gameTime);
}