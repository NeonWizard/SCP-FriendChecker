using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace FriendChecker
{
	class MiscEventHandler : IEventHandlerWaitingForPlayers
	{
		private readonly FriendChecker plugin;

		public MiscEventHandler(FriendChecker plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (!this.plugin.GetConfigBool("friendchecker_enable")) this.plugin.pluginManager.DisablePlugin(plugin);
		}
	}
}
