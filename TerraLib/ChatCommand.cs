using System.Collections.Generic;
using TShockAPI;

namespace TerraLib
{
    /// <summary>
    /// Provides a abstract class to create chat commands.
    /// </summary>
    /// <example>
    ///     <code>
    ///         public class TestCommand : ChatCommand
    ///         {
    ///             public TestCommand() : base("testlib", "testlib.test")
    ///             {
    ///             }
    ///
    ///             public override void Command(CommandArgs args)
    ///             {
    ///                 throw new NotImplementedException();
    ///             }
    ///         }
    ///     </code>
    /// </example>
    public abstract class ChatCommand
    {
        /// <summary>
        /// The permissions required to run the command.
        /// </summary>
        public List<string> Permissions = new List<string>();

        /// <summary>
        /// The aliases for the command
        /// </summary>
        public List<string> Names = new List<string>();

        /// <summary>
        /// A detailed description of what the command does.
        /// </summary>
        public List<string> HelpDescription = new List<string>();

        public ChatCommand(string name, string permission, string helpdesc = "")
        {
            Permissions.Add(permission);
            Names.Add(name);
            HelpDescription.AddRange(helpdesc.Split('\n'));
        }

        public ChatCommand(List<string> names, List<string> permissions, string helpdesc = "")
        {
            Permissions = permissions;
            Names = names;
            HelpDescription.AddRange(helpdesc.Split('\n'));
        }

        public abstract void Command(CommandArgs args);
    }
}