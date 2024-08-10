using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace ForkKnight.Levels
{
    internal class TileMapManager
    {
            private TmxMap _map;
            private Texture2D _mapTexture;
            private int _tileSetTilesWide;
            private int _tileWidth;
            private int _tileHeight;

            public TileMapManager(TmxMap map, Texture2D mapTexture, int tileSetTilesWide, int tileWidth, int tileHeight)
            {
                _map = map;
                _mapTexture = mapTexture;
                _tileSetTilesWide = tileSetTilesWide;
                _tileWidth = tileWidth;
                _tileHeight = tileHeight;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                foreach (var t in _map.Layers)
                {
                    for (var j = 0; j < t.Tiles.Count; j++)
                    {
                        var gid = t.Tiles[j].Gid;
                        if (gid == 0) continue;

                        var tileFrame = gid - 1;
                        var column = tileFrame % _tileSetTilesWide;
                        var row = (int)Math.Floor(tileFrame / (double)_tileSetTilesWide);
                        float x = (j % _map.Width) * _tileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                        var tileSetRect = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth,
                            _tileHeight);
                        spriteBatch.Draw(_mapTexture, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight),
                            tileSetRect, Color.White);
                    }
                }
            }
        }
    }
