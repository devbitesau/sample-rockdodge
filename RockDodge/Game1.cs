using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RockDodge.Engine;

namespace RockDodge;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Sprite _player;
    private Tilemap _tilemap;
    private Sprite _cursor;
    private SpriteFont _spriteFont;

    private double _difficultyTimer = 5;
    private int _difficulty = 1;

    private Vector2 _paralaxRef = Vector2.Zero;

    private List<Rectangle> _tileCollisions;

    private List<Sprite> _props;

    private List<Sprite> _falling;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;

        _falling = new List<Sprite>();
        _props = new List<Sprite>();
        _tileCollisions = new List<Rectangle>();
    }

    protected override void Initialize()
    {
        //_graphics.SynchronizeWithVerticalRetrace = false;

        //this.IsFixedTimeStep = false;
        TargetElapsedTime = TimeSpan.FromTicks((long)(TimeSpan.TicksPerSecond / 120));


        _graphics.PreferredBackBufferWidth = 336;
        _graphics.PreferredBackBufferHeight = 624;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _spriteFont = Content.Load<SpriteFont>("BaseFont");

        var tileMapJson = File.ReadAllText("Content/map.json") ;
        Texture2D tileMapTexture = Content.Load<Texture2D>("Terrain");
        Atlas tileMapAtlas = new Atlas(tileMapTexture, new Vector2(6,8));
        _tilemap = new Tilemap(tileMapAtlas, new Vector2(-16,-16), false);
        _tilemap.AddSpriteFusionTiles(tileMapJson);
        _tileCollisions = _tilemap.GetCollisionRectangles();


        Texture2D characterIdle = Content.Load<Texture2D>("CharacterIdle");
        Atlas idleAtlas = new Atlas(characterIdle, new Vector2(1,11));
        Animation idleAnimation = new Animation(idleAtlas, true, .1);

        Texture2D characterRun = Content.Load<Texture2D>("CharacterRun");
        Atlas runAtlas = new Atlas(characterRun, new Vector2(1,12));
        Animation runAnimation = new Animation(runAtlas, true, .05);

        ISpriteAction moveAction = new MoveAction("default", "run",350);

        _player = new Sprite(idleAnimation, new Vector2(200,300));
        _player.AddAnimation("run", runAnimation);
        _player.AddAction("keyboard", moveAction);
        _player.AddStaticCollisions(_tileCollisions);

        Texture2D cloudPropTexture = Content.Load<Texture2D>("cloud8");
        Atlas cloudPropAtlas = new Atlas(cloudPropTexture, new Vector2(1,1));
        Animation cloudPropAnimation = new Animation(cloudPropAtlas, false, .2);

        Texture2D firePropTexture = Content.Load<Texture2D>("FireBox");
        Atlas propAtlas = new Atlas(firePropTexture, new Vector2(1,3));
        Animation propAnimation = new Animation(propAtlas, true, .2);

        Texture2D mountainsTexture = Content.Load<Texture2D>("mountains");
        Atlas mountainsAtlas = new Atlas(mountainsTexture, new Vector2(1,1));
        Animation mountainsAnimation = new Animation(mountainsAtlas, false, .2);

        Texture2D cloudParalax1Texture = Content.Load<Texture2D>("cloudLayer1");
        Atlas cloudParalax1Atlas = new Atlas(cloudParalax1Texture, new Vector2(1,1));
        Animation cloudParalax1Animation = new Animation(cloudParalax1Atlas, false, .2);

        ISpriteAction paralaxAction = new ParalaxAction(_player, 0.1f);
        ISpriteAction paralaxAction2 = new ParalaxAction(_player, 0.2f);
        ISpriteAction paralaxAction3 = new ParalaxAction(_player, 0.7f);
        ISpriteAction cloudFloatyAction = new InfiniScrollAction(120, new Vector2(-200, 400), true);
                ISpriteAction moonBob = new BobAction();

        Sprite cloudParalax1 = new Sprite(cloudParalax1Animation, new Vector2(-400,300));
        Sprite cloudParalax2 = new Sprite(cloudParalax1Animation, new Vector2(-200,270));
        Sprite mountainParalax1 = new Sprite(mountainsAnimation, new Vector2(-250,260));
        Sprite cloudFloater = new Sprite(cloudPropAnimation, new Vector2(200,100));


        cloudParalax1.AddAction("paralax",paralaxAction);
        cloudParalax2.AddAction("paralax",paralaxAction2);
        mountainParalax1.AddAction("paralax",paralaxAction3);
        cloudFloater.AddAction("scroll",cloudFloatyAction);

        Texture2D moonProp = Content.Load<Texture2D>("moonFull");
        Atlas propMoonAtlas = new Atlas(moonProp, new Vector2(1,1));
        Animation propMoonAnimation = new Animation(propMoonAtlas, false, .2);
        Sprite moon = new Sprite(propMoonAnimation, new Vector2(200, 50));
        moon.AddAction("bob",moonBob);

        _props.Add(cloudParalax2);

        _props.Add(mountainParalax1);
        _props.Add(cloudFloater);
        _props.Add(cloudParalax1);
        _props.Add(new Sprite(propAnimation, new Vector2(50,400)));
        _props.Add(new Sprite(propAnimation, new Vector2(180,400)));
        _props.Add(new Sprite(propAnimation, new Vector2(260,400)));
        _props.Add(moon);


        Texture2D cursorTexture = Content.Load<Texture2D>("Strawberry");
        Atlas cursorAtlas = new Atlas(cursorTexture, new Vector2(1,17));
        Animation cursorAnimation = new Animation(cursorAtlas, true, .3);
        ISpriteAction cursorAction = new FollowMouseAction();

        _cursor = new Sprite(cursorAnimation, new Vector2(0,0));
        _cursor.AddAction("FollowMouse",cursorAction);

        _falling.Add(MakeSpikeBall(ref _difficulty));
    }

    private Sprite MakeSpikeBall(ref int difficulty)
    {
        Texture2D spikeBallTexture = Content.Load<Texture2D>("SpikeBall");
        Atlas spikeBallAtlas = new Atlas(spikeBallTexture, new Vector2(1,1));
        Animation spikeBallAnimation = new Animation(spikeBallAtlas, false, 1);
        ISpriteAction spikeBallRotateAction = new RotateAction(160.0f);
        ISpriteAction spikeFallingAction = new FallingAction();
        Random r = new Random();
        Sprite spikeBall = new Sprite(spikeBallAnimation, new Vector2(-20,r.Next(0,350)), SpriteOrigin.Center);
        spikeBall.AddAction("spin",spikeBallRotateAction);
        spikeBall.AddAction("falling",spikeFallingAction);
        return spikeBall;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(gameTime);
        _cursor.Update(gameTime);

        _difficultyTimer -= gameTime.ElapsedGameTime.TotalSeconds;
        if (_difficultyTimer <= 0)
        {
            _difficulty++;
            _difficultyTimer = 10;
            _falling.Add(MakeSpikeBall(ref _difficulty));
        }

        foreach (var prop in _props)
        {
            prop.Update(gameTime);
        }

        foreach (var falling in _falling)
        {
            falling.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(25,25,45));

        _spriteBatch.Begin();

        foreach (var prop in _props)
        {
            prop.Draw(_spriteBatch);
        }

        foreach (var falling in _falling)
        {
            falling.Draw(_spriteBatch);
        }
        _tilemap.Draw(_spriteBatch);





        _player.Draw(_spriteBatch);

        _cursor.Draw(_spriteBatch);

        double fps = Math.Round(1.0 / gameTime.ElapsedGameTime.TotalSeconds) ;
        _spriteBatch.DrawString(_spriteFont, $"FPS: {fps}", new Vector2(32+16, 32+16), Color.White);
        _spriteBatch.DrawString(_spriteFont,$"Time: {gameTime.TotalGameTime.TotalSeconds:f2}", new Vector2(65, 560), Color.White);
        _spriteBatch.DrawString(_spriteFont,$"Difficulty: {_difficulty}", new Vector2(200, 560), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}