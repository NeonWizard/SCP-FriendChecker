using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.IO;

namespace FriendChecker
{
	class FCheckerDisableCommand : ICommandHandler
	{
		private FriendChecker plugin;

		public FCheckerDisableCommand(FriendChecker plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Disables FriendChecker";
		}

		public string GetUsage()
		{
			return "FRIENDCHECKERDISABLE";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			plugin.Info(sender + " ran the " + GetUsage() + " command!");
			this.plugin.pluginManager.DisablePlugin(this.plugin);
			return new string[] { "FriendChecker Disabled" };
		}
	}
}
