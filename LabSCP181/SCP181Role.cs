using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSCP181
{
    public class SCP181Role
    {
        public static List<int> SCP181PlayerIds = new List<int>();
        public static void spawn(Player player)
        {
            player.SetRole(Plugin.StatusPlugin.Config.Role);
            player.AddItem(ItemType.KeycardJanitor);
            player.SetRank("SCP181", "pick");
            player.MaxHealth = Plugin.StatusPlugin.Config.MaxHealth;
            player.Health = player.MaxHealth;
            SCP181PlayerIds.Add(player.PlayerId);
            player.SendHint("你是[SCP181]\n概率打开权限门", 10);
        }
    }
}
