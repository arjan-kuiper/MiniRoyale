using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MINI_ROYALE
{
    class TileMap
    {
        int _tileWidth;
        int _tileHeight;
        int _width;
        int _height;
        Tile[,] bitmap;
        private Camera2D cam;
        Game game;
        
        
        public Camera2D Camera
        {
            get
            {
                return cam;
            }
        }
        public TileMap(GraphicsDevice gd, int Tilewidth, int TileHeight, int width, int height)
        {
            _tileWidth = Tilewidth;
            _tileHeight = TileHeight;
            _width = width;
            _height = height;
            cam = new Camera2D(gd);
            bitmap = generateMap(null);
            
        }
        public int getTileSize()
        {
            return _tileHeight;
        }
        public Tile[,] generateMap(int[,] mapArray)
        {
            return new Tile[,] {
                {new Tile("sprite", false) }
        };
            }
        public void setGameDevice(Game g)
        {
            this.game = g;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 tilePos = Vector2.Zero;
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix());
            for (int x = 0; x< _width; x++)
            {
                for(int y = 0; y < _height; y++)
                {
                    Tile t = bitmap[x, y];
                    //spriteBatch.FillRectangle(tilePos, new Size2(_tileWidth, _tileHeight), t.getSprite());
                    spriteBatch.Draw(game.Content.Load<Texture2D>(t.file) , new Rectangle(x, y, _tileWidth, _tileHeight), Color.Black);
                    tilePos.X += _tileWidth;
                }
                tilePos.Y += _tileHeight;
                tilePos.X = 0;
            }
            spriteBatch.End();
        }
    }
}
