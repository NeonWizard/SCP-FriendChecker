using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Config;

namespace FriendChecker
{
	[PluginDetails(
		author = "Spooky",
		name = "FriendChecker",
		description = "List any friend circles within a server.",
		id = "xyz.wizardlywonders.FriendChecker",
		version = "1.0.0",
		SmodMajor = 3,
		SmodMinor = 2,
		SmodRevision = 2
	)]
	public class FriendChecker : Plugin
    {
		internal static FriendChecker plugin;

		public override void OnDisable()
		{
			this.Info("FriendChecker has been disabled.");
		}

		public override void OnEnable()
		{
			this.Info("FriendChecker has loaded successfully.");
		}

		public override void Register()
		{
			// Register config
			this.AddConfig(new ConfigSetting("friendchecker_enable", true, SettingType.BOOL, true, "Whether FriendChecker should be enabled on server start."));

			// Register events
			this.AddEventHandlers(new MiscEventHandler(this), Priority.Highest);

			// Register commands
			this.AddCommand("friendcheckerdisable", new FCheckerDisableCommand(this));
		}
	}
}
