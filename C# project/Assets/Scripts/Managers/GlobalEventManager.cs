namespace CSToU.Core
{

    /// <summary>
    /// Evento
    /// </summary>
    public static class GlobalEventManager 
    {
        public delegate void GlobalEvent();

        /// <summary>
        /// Evento prima del setup
        /// </summary>
        public static GlobalEvent OnPreSetUp;
        public static GlobalEvent OnPostSetUp;
       
    }
}