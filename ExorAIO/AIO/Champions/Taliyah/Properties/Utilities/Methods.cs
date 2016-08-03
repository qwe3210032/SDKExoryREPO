namespace ExorAIO.Champions.Taliyah
{
    using LeagueSharp;
    using LeagueSharp.SDK;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Taliyah.OnUpdate;
            GameObject.OnCreate += Taliyah.OnCreate;
            GameObject.OnDelete += Taliyah.OnDelete;
            Events.OnGapCloser += Taliyah.OnGapCloser;
            Events.OnInterruptableTarget += Taliyah.OnInterruptableTarget;
        }

        #endregion
    }
}