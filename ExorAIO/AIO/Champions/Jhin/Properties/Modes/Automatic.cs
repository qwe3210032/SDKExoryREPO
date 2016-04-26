using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
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
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The R Manager.
            /// </summary>
            Variables.Orbwalker.SetAttackState(!Vars.R.Instance.Name.Equals("JhinRShot"));
            Variables.Orbwalker.SetMovementState(!Vars.R.Instance.Name.Equals("JhinRShot"));

            if (GameObjects.Player.IsRecalling() || Vars.R.Instance.Name.Equals("JhinRShot"))
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.HasBuff("JhinPassiveReload") &&
                Vars.Menu["spells"]["q"]["lasthit"].GetValue<MenuBool>().Value)
            {
                foreach (var minion in
                    Targets.Minions.Where(m => m.IsValidTarget(Vars.Q.Range) && m.Health < Vars.Q.GetDamage(m)))
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() && !GameObjects.Player.IsUnderEnemyTurret() &&
                Vars.Menu["spells"]["w"]["auto"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && t.HasBuff("jhinespotteddebuff") &&
                            t.IsValidTarget(Vars.W.Range) &&
                            Vars.Menu["whitelist.{t.ChampionName.ToLower()}"]
                                .GetValue<MenuBool>().Value))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).CastPosition);
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() && Vars.Menu["spells"]["e"]["auto"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t => Bools.IsImmobile(t) && !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.Cast(Vars.E.GetPrediction(target).CastPosition);
                }
            }
        }
    }
}