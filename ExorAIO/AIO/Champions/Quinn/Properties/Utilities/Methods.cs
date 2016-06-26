using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Quinn
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Quinn.OnUpdate;
            Events.OnGapCloser += Quinn.OnGapCloser;
            Events.OnInterruptableTarget += Quinn.OnInterruptableTarget;
            Variables.Orbwalker.OnAction += Quinn.OnAction;
        }
    }
}