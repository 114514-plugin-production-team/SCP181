using GameCore;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSCP181
{
    public static class API
    {
        public static readonly System.Random Random = new System.Random();
        private static readonly HashSet<Player> PreviouslySelectedPlayers = new HashSet<Player>();
        public static void SetRank(this Player player, string Text, string Color)
        {
            player.ReferenceHub.serverRoles.SetColor(Color);
            player.ReferenceHub.serverRoles.SetText(Text);
        }
        public static Player Randomplayer(List<Player> players)
        {
            try
            {
                // Filter out null players and those already selected
                var availablePlayers = players
                    .Where(p => p != null && !PreviouslySelectedPlayers.Contains(p))
                    .ToList();

                if (availablePlayers.Count == 0)
                {
                    // Reset the selection pool if all players have been selected
                    PreviouslySelectedPlayers.Clear();
                    availablePlayers = players.Where(p => p != null).ToList();

                    if (availablePlayers.Count == 0)
                        return null;
                }

                // Select random player
                var selectedPlayer = availablePlayers[Random.Next(availablePlayers.Count)];
                PreviouslySelectedPlayers.Add(selectedPlayer);

                return selectedPlayer;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Randomplayer: {ex}");
                return null;
            }
        }
    }
}
