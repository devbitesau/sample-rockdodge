using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class BreakCollideAction : ISpriteAction
{


    public void Update(Sprite sprite, GameTime gameTime)
    {
        var thisRect = sprite.GetCollisionRectangle();

        foreach (var rect in sprite.GetStaticCollisions())
        {
            if (thisRect.Intersects(rect))
            {
                sprite.MarkDestroy(true);
            }
        }
    }


}