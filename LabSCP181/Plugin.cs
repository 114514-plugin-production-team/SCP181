using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using MapGeneration.Distributors;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSCP181
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP181";

        public override string Description => "SCP181";

        public override string Author => "灰(QQ3145186196)";

        public override Version Version => new Version(1,0);

        public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);
        public static Plugin StatusPlugin;
        public override void Enable()
        {
            ServerEvents.RoundStarted += OnStart;
            PlayerEvents.InteractingDoor += OnDoor;
            PlayerEvents.Death += OnDied;
        }
        public override void Disable()
        {
            ServerEvents.RoundStarted -= OnStart;
            PlayerEvents.InteractingDoor -= OnDoor;
            PlayerEvents.Death -= OnDied;
        }
        public void OnDied(PlayerDeathEventArgs ev)
        {
            if (ev.Player!=null)
            {
                if (SCP181Role.SCP181PlayerIds.Contains(ev.Player.PlayerId))
                {
                    SCP181Role.SCP181PlayerIds.Remove(ev.Player.PlayerId);
                    Cassie.Message($"{Config.SCP181Died}");
                    
                }
            }
        }
        public void OnDoor(LabApi.Events.Arguments.PlayerEvents.PlayerInteractingDoorEventArgs ev)
        {
            if (ev.Player!=null)
            {
                if (SCP181Role.SCP181PlayerIds.Contains(ev.Player.PlayerId))
                {
                    if(API.Random.Next(1,100) >= Config.OpeanDoor&&!ev.Door.IsLocked)
                    {
                        ev.Door.IsOpened = true;
                        ev.IsAllowed = true;
                    }
                }
            }
        }
        public void OnStart()
        {
            var SCP181 = API.Randomplayer(Player.List.Where(x => x.Role == RoleTypeId.ClassD).ToList());
            SCP181Role.spawn(SCP181);
        }
    }
}
