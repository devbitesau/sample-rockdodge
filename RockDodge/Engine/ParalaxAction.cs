using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class ParalaxAction : ISpriteAction
{
    private Sprite _referencePlayer;
    private Vector2 _homePosition = Vector2.Zero;
    private float _paralaxFactor;

    public ParalaxAction(Sprite referencePlayer, float paralaxFactor)
    {
        _referencePlayer = referencePlayer;
        _paralaxFactor = paralaxFactor;
    }
    public void Update(Sprite sprite, GameTime gameTime)
    {
        if (_homePosition == Vector2.Zero)
            _homePosition = sprite.GetPosition();

        Vector2 pos = _homePosition + (_referencePlayer.GetPosition() * _paralaxFactor);

        sprite.MoveTo(new Vector2(pos.X,_homePosition.Y));
    }
}