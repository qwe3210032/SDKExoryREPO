using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace ExorAIO.Champions.Sivir
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                GameObjects.Player.ManaPercent > 
                    Vars.Menu["spells"]["q"]["harass"].GetValue<MenuSliderButton>().SValue +
                    (int)(GameObjects.Player.Spellbook.GetSpell(Vars.Q.Slot).ManaCost / GameObjects.Player.MaxMana * 100) &&
                Vars.Menu["spells"]["q"]["harass"].GetValue<MenuSliderButton>().BValue)
            {
                if (GameObjects.Player.Distance(Targets.Target) > 650 &&
                    Vars.Q.GetPrediction(Targets.Target).Hitchance >= HitChance.Medium)
                {
                    Vars.Q.Cast(
                        Vars.Q.GetPrediction(Targets.Target)
                            .UnitPosition.Extend((Vector2)GameObjects.Player.ServerPosition, -140));
                }
            }
        }
    }
}