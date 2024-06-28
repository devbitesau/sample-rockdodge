using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RockDodge.Engine;

public class FollowMouseAction : ISpriteAction
{
    public void Update(Sprite sprite, GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        sprite.MoveTo(mouseState.Position.ToVector2());
    }
}