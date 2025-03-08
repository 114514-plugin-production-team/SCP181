using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Pickups;
using PlayerRoles;
using Scp914.Processors;
using Scp914;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;
using Exiled.API.Extensions;
using Exiled.API.Features.Doors;

namespace EX_SCP181
{
    [CustomRole(PlayerRoles.RoleTypeId.ClassD)]
    public class SCP181 : CustomRole
    {
        public static List<ushort> fuzhiitems = new List<ushort>();
        public override uint Id { get; set; } = 181;
        public override int MaxHealth { get; set; } = 200;
        public override string Name { get; set; } = "SCP181";
        public override string Description { get; set; } = "SCP181|概率开门|捡起物品的时候有概率升级/复制";
        public override string CustomInfo { get; set; } = "SCP181";
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.KeycardScientist}",
            $"{ItemType.Medkit}"
        };
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint()
                {
                    Name = "ClassDSpawn",
                    Room = Exiled.API.Enums.RoomType.LczClassDSpawn,
                    Chance = 100
                }
            }
        };
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= Onpick;
            Exiled.Events.Handlers.Player.InteractingDoor -= OnDoor;
            Exiled.Events.Handlers.Server.RestartingRound -= OnRest;
            Exiled.Events.Handlers.Player.Died -= Ondied;
            base.UnsubscribeEvents();
        }
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem += Onpick;
            Exiled.Events.Handlers.Player.InteractingDoor += OnDoor;
            Exiled.Events.Handlers.Server.RestartingRound += OnRest;
            Exiled.Events.Handlers.Player.Died += Ondied;
            base.SubscribeEvents();
        }
        protected void OnRest()
        {
            fuzhiitems.Clear();
        }
        protected void Ondied(DiedEventArgs ev)
        {
            if(Check(ev.Player))
            {
                RemoveRole(ev.Player);
            }
        }
        protected void Onpick(PickingUpItemEventArgs ev)
        {
            if(Check(ev.Player) && ev.IsAllowed)
            {
                if (new Random().Next(1, 100) >= 70)
                {
                    if (ev.Pickup.Type != ItemType.KeycardO5 && ev.Pickup.Type != ItemType.SCP500 && ev.Pickup.Type != ItemType.SCP1576)
                    {
                        ev.IsAllowed = false;
                        Scp914ItemProcessor scp914ItemProcessor;
                        Scp914Upgrader.TryGetProcessor(ev.Pickup.Info.ItemId, out scp914ItemProcessor);
                        Scp914Result scp914Result = scp914ItemProcessor.UpgradePickup(Scp914KnobSetting.Fine, ev.Pickup.Base);
                        ItemPickupBase itemPickupBase;
                        if (scp914Result.ResultingPickups.TryGet(0, out itemPickupBase))
                        {
                            ItemPickupBase randomValue = (from x in scp914Result.ResultingPickups
                                                          where !x.Info.ItemId.IsAmmo()
                                                          select x).GetRandomValue<ItemPickupBase>();
                            if (randomValue != null)
                            {
                                ev.Player.AddItem(randomValue.Info.ItemId);
                            }
                        }
                        ev.Pickup.Destroy();
                        return;
                    }
                }
                else if (new Random().Next(1, 100) <= 20 && (ev.Pickup.Serial == 0 || !fuzhiitems.Contains(ev.Pickup.Serial)))
                {
                    if (ev.Pickup.Serial == 0)
                    {
                        ev.IsAllowed = false;
                        fuzhiitems.Add(ev.Player.AddItem(ev.Pickup.Type).Serial);
                        fuzhiitems.Add(ev.Player.AddItem(ev.Pickup.Type).Serial);
                        ev.Pickup.Destroy();
                        return;
                    }
                    fuzhiitems.Add(ev.Pickup.Serial);
                    fuzhiitems.Add(ev.Player.AddItem(ev.Pickup.Type).Serial);
                }
            }
        }
        protected void OnDoor(InteractingDoorEventArgs ev)
        {
            if (Check(ev.Player) && new Random().Next(1, 100) >= 80 && !ev.Door.IsLocked && !ev.IsAllowed)
            {
                ev.IsAllowed = true;
            }
        }
    }
}
