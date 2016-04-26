using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && !t.IsValidTarget(Vars.AARange) &&
                            t.IsValidTarget(Vars.Q.Range - 100f) && t.Health < Vars.Q.GetDamage(t) * 2))
                {
                    Vars.Q.Cast(
                        Vars.Q.GetPrediction(Targets.Target)
                            .CastPosition.Extend((Vector2) GameObjects.Player.ServerPosition, -140));
                }
            }
        }
    }
}