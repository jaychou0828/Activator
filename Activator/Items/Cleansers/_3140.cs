﻿using System;
using Activator.Base;
using Activator.Handlers;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Items.Cleansers
{
    class _3140 : CoreItem 
    {
        internal override int Id => 3140;
        internal override string Name => "Quicksilver";
        internal override string DisplayName => "Quicksilver Sash";
        internal override int Priority => 6;
        internal override int Duration => 1000;
        internal override float Range => float.MaxValue;
        internal override MenuType[] Category => new[] { MenuType.Cleanse, MenuType.ActiveCheck };
        internal override MapType[] Maps => new[] { MapType.Common };
        internal override int DefaultHP => 5;
        internal override int DefaultMP => 0;

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>() || !IsReady())
                return;

            foreach (var hero in Activator.Allies())
            {
                if (hero.Player.NetworkId == Player.NetworkId)
                {
                    if (!Parent.Item(Parent.Name + "useon" + hero.Player.NetworkId).GetValue<bool>())
                        continue;

                    if (hero.ForceQSS)
                    {
                        UseItem();
                        hero.QSSBuffCount = 0;
                        hero.QSSHighestBuffTime = 0;
                    }

                    Buffs.CheckQSS(hero.Player);

                    var d = Convert.ToDouble(Menu.Item("use" + Name + "time").GetValue<StringList>().SelectedValue);
                    if (hero.QSSBuffCount >= Menu.Item("use" + Name + "number").GetValue<Slider>().Value &&
                        hero.QSSHighestBuffTime >= d * 1000)
                    {
                        if (!Menu.Item("use" + Name + "od").GetValue<bool>())
                        {
                            Utility.DelayAction.Add(Game.Ping + Menu.Item("use" + Name + "delay").GetValue<Slider>().Value, delegate
                            {
                                UseItem(Menu.Item("mode" + Name).GetValue<StringList>().SelectedIndex == 1);
                                hero.QSSBuffCount = 0;
                                hero.QSSHighestBuffTime = 0;
                            });
                        }
                    }
                }
            }
        }
    }
}
