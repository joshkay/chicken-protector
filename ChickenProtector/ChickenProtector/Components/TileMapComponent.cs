using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChickenProtector.Components
{
    using Artemis.Interface;

    public class TileMapComponent : IComponent
    {
        public TileMapComponent(int[,] map, int tileWidth, int tileHeight)
        {
            this.MapArray = map;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
        }

        public int TileHeight { get; set; }
        public int TileWidth { get; set; }

        public int[,] MapArray { get; set; }

        public enum TileMapSpatial { TMS_GRASS = 0, TMS_WATER = 1, TMS_DIRT = 2, TMS_SNOW = 3 }

        public string GetTileMapSpatial(TileMapSpatial tms)
        {
            string spatial = "";
            switch (tms)
            {
                case TileMapSpatial.TMS_GRASS:
                    spatial = "GrassTile";
                    break;
                case TileMapSpatial.TMS_WATER:
                    spatial = "WaterTile";
                    break;
                case TileMapSpatial.TMS_DIRT:
                    spatial = "DirtTile";
                    break;
                case TileMapSpatial.TMS_SNOW:
                    spatial = "SnowTile";
                    break;
            }
            return spatial;
        }
    }
}
