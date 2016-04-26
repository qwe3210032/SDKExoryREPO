using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.Lucian
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
            ///     The Q Killsteal Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                if (
                    !GameObjects.EnemyHeroes.Any(
                        t =>
                            !Bools.HasAnyImmunity(t) && !t.IsValidTarget(Vars.Q.Range) &&
                            t.IsValidTarget(Vars.Q2.Range) && t.Health < Vars.Q.GetDamage(t)))
                {
                    return;
                }

                /// <summary>
                ///     Through enemy minions.
                /// </summary>
                foreach (var minion 
                    in from minion in Targets.Minions
                        let polygon =
                            new Geometry.Rectangle(
                                GameObjects.Player.ServerPosition,
                                GameObjects.Player.ServerPosition.Extend(minion.ServerPosition, Vars.Q2.Range),
                                Vars.Q2.Width)
                        where
                            !polygon.IsOutside(
                                (Vector2)
                                    Vars.Q2.GetPrediction(
                                        GameObjects.EnemyHeroes.FirstOrDefault(
                                            t =>
                                                !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range) &&
                                                t.Health < Vars.Q.GetDamage(t))).CastPosition)
                        select minion)
                {
                    Vars.Q.CastOnUnit(minion);
                }

                /// <summary>
                ///     Through enemy champions.
                /// </summary>
                foreach (var target
                    in
                    from target in
                        GameObjects.EnemyHeroes.Where(t => !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q.Range))
                    let polygon =
                        new Geometry.Rectangle(
                            GameObjects.Player.ServerPosition,
                            GameObjects.Player.ServerPosition.Extend(target.ServerPosition, Vars.Q2.Range),
                            Vars.Q2.Width)
                    where
                        !polygon.IsOutside(
                            (Vector2)
                                Vars.Q2.GetPrediction(
                                    GameObjects.EnemyHeroes.FirstOrDefault(
                                        t =>
                                            !Bools.HasAnyImmunity(t) && t.IsValidTarget(Vars.Q2.Range) &&
                                            t.Health < Vars.Q.GetDamage(t))).CastPosition)
                    select target)
                {
                    Vars.Q.CastOnUnit(target);
                }
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (Vars.W.IsReady() && Vars.Menu["spells"]["w"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && !t.IsValidTarget(Vars.Q.Range) &&
                            t.Health < Vars.W.GetDamage(t) && t.IsValidTarget(Vars.W.Range)))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).CastPosition);
                    return;
                }
            }
        }
    }
}