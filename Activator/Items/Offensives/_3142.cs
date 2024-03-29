﻿using System;
using Activator.Base;
using LeagueSharp.Common;

namespace Activator.Items.Offensives
{
    class _3142 : CoreItem
    {
        internal override int Id => 3142;
        internal override int Priority => 5;
        internal override string Name => "Youmuus";
        internal override string DisplayName => "Youmuus Ghostblade";
        internal override int Duration => 45000;
        internal override float Range => 750f;
        internal override MenuType[] Category => new[] { MenuType.SelfLowHP, MenuType.EnemyLowHP };
        internal override MapType[] Maps => new[] { MapType.Common };
        internal override int DefaultHP => 95;
        internal override int DefaultMP => 0;

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>() || !IsReady())
                return;

            if (Tar != null)
            {          
                if (!Parent.Item(Parent.Name + "useon" + Tar.Player.NetworkId).GetValue<bool>())
                    return;

                if (Tar.Player.Health / Tar.Player.MaxHealth * 100 <= Menu.Item("enemylowhp" + Name + "pct").GetValue<Slider>().Value)
                {
                    UseItem(true);
                }

                if (Player.Health / Player.MaxHealth * 100 <= Menu.Item("selflowhp" + Name + "pct").GetValue<Slider>().Value)
                {
                    UseItem(true);
                }
            }
        }
    }
}
