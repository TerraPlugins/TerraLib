using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;
using Terraria;
using Microsoft.Xna.Framework;

namespace TerraLib
{
    public static class Extensions
    {
        public static bool PvPEnabled(this TSPlayer ply)
        {
            return Main.player[ply.Index].hostile;
        }

        public static void EnablePvP(this TSPlayer ply, bool value)
        {
            Main.player[ply.Index].hostile = value;
            NetMessage.SendData((int)PacketTypes.TogglePvp, -1, -1, Terraria.Localization.NetworkText.Empty, ply.Index);
        }

        public static TSPlayer FindByID(this TSPlayer ply, int ID, bool loggedIn = true, bool active = true)
        {
            return TShock.Players.FirstOrDefault(p => p?.Active == active && p.IsLoggedIn == loggedIn && p.User.ID == ID);
        }

        public static bool TeleportTo(this TSPlayer ply, int tile_x, int tile_y)
        {
            // https://github.com/Pryaxis/TShock/blob/9a99671b769f6c44c0a3640715695d9ef856f39f/TShockAPI/Commands.cs#L2704
            return ply.Teleport(tile_x * 16, tile_y * 16);
        }

        public static Point GetTilePos(this TSPlayer ply)
        {
            return new Point(ply.TileX, ply.TileY);
        }

        /// <summary>
        /// Broadcast a message to all the players in the list.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        public static void BroadcastTo(this List<TSPlayer> players, string msg, Color color)
        {
            foreach(var ply in players)
            {
                ply.SendMessage(msg, color);
            }
        }

        /// <summary>
        /// Removes all players that match the given ID.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="ID"></param>
        public static void RemoveByID(this List<TSPlayer> players, int ID)
        {
            players.RemoveAll(x => x.User.ID == ID);
        }
    }
}
