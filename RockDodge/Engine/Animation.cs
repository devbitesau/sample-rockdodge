using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine;

public class Animation
{
    private Atlas _atlas;
    private int _currentFrame;
    private double _secondsPerFrame;
    private bool _loop;
    private int _totalFrames;
    private double _countDown;

    public Animation(Atlas atlas, bool loop, double secondsPerFrame)
    {
        _atlas = atlas;
        _loop = loop;
        _secondsPerFrame = secondsPerFrame;
        _totalFrames = _atlas.GetFrameCount();

        _countDown = _secondsPerFrame;
    }

    public Vector2 GetFrameSize()
    {
        return _atlas.GetFrameSize();
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flipX, float rotation, SpriteOrigin origin)
    {


        _atlas.Draw(spriteBatch, _currentFrame, position, flipX, rotation, GetOriginPoint(origin));
    }
    private Vector2 GetOriginPoint(SpriteOrigin origin)
    {
        var _sourceFrameSize = _atlas.GetFrameSize();
        return  origin switch
        {
            SpriteOrigin.Center => new Vector2((int)(_sourceFrameSize.X / 2), (int)(_sourceFrameSize.Y / 2)),
            SpriteOrigin.TopLeft => Vector2.Zero,
            SpriteOrigin.TopRight => new Vector2(_sourceFrameSize.X, 0),
            SpriteOrigin.BottomLeft => new Vector2(0, _sourceFrameSize.Y),
            SpriteOrigin.BottomRight => new Vector2(_sourceFrameSize.X, _sourceFrameSize.Y),
            SpriteOrigin.CenterLeft => new Vector2(0, (int)(_sourceFrameSize.Y / 2)),
            SpriteOrigin.CenterRight => new Vector2(_sourceFrameSize.X, (int)(_sourceFrameSize.Y / 2)),
            SpriteOrigin.CenterTop => new Vector2((int)(_sourceFrameSize.X / 2), 0),
            SpriteOrigin.CenterBottom => new Vector2((int)(_sourceFrameSize.X / 2), _sourceFrameSize.Y),
            _ => Vector2.Zero
        };
    }
    public void Update(GameTime gameTime)
    {
        _countDown -= gameTime.ElapsedGameTime.TotalSeconds;

        if (_countDown <= 0 && _loop)
        {
            _currentFrame++;

            if (_currentFrame >= _totalFrames)
            {
                _currentFrame = 0;
            }

            _countDown = _secondsPerFrame;
        }
    }
}