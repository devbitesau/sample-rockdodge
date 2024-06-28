using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine;

/// <summary>
/// Class to manipulate a texture as an atlas.
/// </summary>
public class Atlas
{
    private Texture2D _texture;
    private int _tileWidth;
    private int _tileHeight;
    private Vector2 _gridSize;
    private Dictionary<int,Rectangle> _frames;

    /// <summary>
    /// Handles reading a texture as an atlas.
    /// </summary>
    /// <param name="texture">Texture to load as an atlas.</param>
    /// <param name="gridSize">Vector2 representing the number of columns and rows in this tileset.</param>
    public Atlas(Texture2D texture, Vector2 gridSize)
    {
        _texture = texture;
        _tileWidth = _texture.Width / (int) gridSize.Y; // Divide width by the columns
        _tileHeight = _texture.Height / (int) gridSize.X; // Divide height by the rows
        _gridSize = gridSize;

        _frames = new Dictionary<int,Rectangle>();

        int frame = 0;
        for (int x = 0; x < gridSize.X; x++)
        {
            for (int y = 0; y < gridSize.Y; y++)
            {
                // Swap X and Y, remembering x is the number of rows and y is the number of columns
                // Ie, 10 Rows (grid.x) would split vertically (Texture.Y)
                _frames.Add(frame, new Rectangle(y * _tileWidth, x * _tileHeight, _tileWidth, _tileHeight));
                frame++;
            }
        }
    }

    /// <summary>
    /// Returns the number of frames in this atlas.
    /// </summary>
    /// <returns>Count of frames.</returns>
    /// <remarks>
    /// This is the count of frames, not the index. When using this in a loop, use GetFrameCount() - 1
    /// </remarks>
    public int GetFrameCount()
    {
        return _frames.Count;
    }

    public Vector2 GetFrameSize()
    {
        return new Vector2(_tileWidth, _tileHeight);
    }

    /// <summary>
    /// Draws respective frame from the atlas to the spritebatch.
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="frame">frame number ot use.</param>
    /// <param name="position">X and Y position.</param>
    public void Draw(SpriteBatch spriteBatch, int frame, Vector2 position, bool flipX, float rotation, Vector2 origin)
    {
        spriteBatch.Draw(_texture, position, _frames[frame], Color.White, rotation, origin, 1, flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.0f);
    }


}