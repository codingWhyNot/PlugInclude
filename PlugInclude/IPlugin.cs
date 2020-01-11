namespace PlugInclude
{
    /// <summary>
    /// Provides basic frame for a plugin
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Contains the plugin name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Contains the plugin description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Indicates if the plugin needs the execute command/ if not it will use initial control
        /// </summary>
        bool UsesExecuteCommand { get; }

        /// <summary>
        /// Contains the initial user control
        /// </summary>
        object InitialControl { get; }

        /// <summary>
        /// Contains the plugin execute/start method
        /// </summary>
        object Execute(object parameter);
    }
}
