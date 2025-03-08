using Exiled.API.Features;
using Exiled.CustomRoles.API;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_SCP181
{
    public class Plugin :Plugin<Class1>
    {
        public override string Author => "114514(QQ3145186196)";
        public override string Name => "SCP181";
        public override Version Version => new Version(1,0);
        
        public override void OnEnabled()
        {
            Log.Info("欢迎使用[114514]的SCP181插件|已在GITHUB上开源");
            Config.SCP181.Register();
            Exiled.Events.Handlers.Server.RoundStarted += Onstart;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Config.SCP181.Unregister();
            Exiled.Events.Handlers.Server.RoundStarted -= Onstart;
            base.OnDisabled();
        }
        public void Onstart()
        {
            foreach (Player player in Player.List)
            {
                 Player scp181 = GetRandom(RoleTypeId.ClassD, 1);
                
                Config.SCP181.AddRole(scp181);
                
            }
        }
        public static Player GetRandom(RoleTypeId targetRole, int amount = 1, bool checkAlive = true)
        {
            // 使用新版API筛选玩家
            var candidates = Player.List
                .Where(p => p.Role == targetRole && (!checkAlive || p.IsAlive));

            // EXILED 6.0+ 专用随机逻辑
            return candidates
                .OrderBy(p => UnityEngine.Random.Range(0, int.MaxValue))
                .Take(amount)
                .FirstOrDefault();
        }
    }
}
