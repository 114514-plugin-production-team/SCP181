using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSCP181
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;
        [Description("SCP181的血量")]
        public int MaxHealth { get; set; } = 120;
        [Description("SCP181的装备")]
        public List<string> ItemTypes = new List<string>()
        {
            ItemType.KeycardJanitor.ToString(),
            ItemType.Medkit.ToString(),
        };
        [Description("SCP181的角色")]
        public RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        [Description("SCP181死亡广播")]
        public string SCP181Died { get; set; } = "";
        [Description("SCP181成功开权限门的概率")]
        public float OpeanDoor { get; set; } = 80;
    }
}
