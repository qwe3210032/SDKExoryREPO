
#pragma warning disable 1587

namespace ExorAIO.Champions.Caitlyn
{
    using System;
    using System.Linq;

    using ExorAIO.Utilities;

    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;
    using LeagueSharp.SDK.UI;
    using LeagueSharp.SDK.Utils;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || GameObjects.Player.Mana < Vars.E.Instance.ManaCost + Vars.Q.Instance.ManaCost)
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t => t.IsValidTarget(650f) && !Invulnerable.Check(t) && !t.HasBuff("caitlynyordletrapinternal"))
                    )
                {
                    if (!Vars.E.GetPrediction(target).CollisionObjects.Any()
                        && Vars.E.GetPrediction(target).Hitchance >= HitChance.Medium)
                    {
                        Vars.E.Cast(
                            GameObjects.Player.ServerPosition.Extend(
                                Vars.E.GetPrediction(target).CastPosition,
                                (float)
                                (GameObjects.Player.Distance(Vars.E.GetPrediction(target).CastPosition)
                                 + Vars.E.Width / 1.5)));
                    }
                }
            }
        }

        #endregion
    }
}