using Smod2.Commands;
using Smod2;
using Smod2.API;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FriendChecker
{
	class FriendCheckCommand : ICommandHandler
	{
		private readonly FriendChecker plugin;

		public FriendCheckCommand(FriendChecker plugin) => this.plugin = plugin;

		public string GetCommandDescription()
		{
			return "Checks for any friend circles on the server. If a player ID is provided, it will list friends of the target that are also on the server.";
		}

		public string GetUsage()
		{
			return "FCHECK [PLAYER]";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			Server server = PluginManager.Manager.Server;

			// -- Searching for friend circles for specific user
			if (args.Length == 1)
			{
				// -- Find target player by args[0]
				Player target = null;
				if (short.TryParse(args[0], out short pID))
				{
					foreach (Player p in server.GetPlayers())
					{
						if (p.PlayerId == pID)
						{
							target = p;
						}
					}
					if (target == null) return new string[] { "Nobody with that player ID exists." };
				}
				else
				{
					return new string[] { "Can't parse player ID." };
				}

				// -- Lookup target's friends from SteamAPI
				string[] friendIDs = this.plugin.steamAPI.GetFriends(target.SteamId);

				// -- Cross-reference target's friends with online players
				string[] onlineFriendIds = server
					.GetPlayers()
					.Where(x => friendIDs.Contains(x.SteamId))
					.Select(x => x.Name)
					.ToArray();

				return new string[] { "Listing friends...", "["+target.Name+"] " + string.Join(", ", onlineFriendIds) };
			}
			// -- Searching for all friend circles
			else if (args.Length == 0)
			{
				//foreach (Player p in server.GetPlayers())
				//{
				//}
			}

			return new string[] { "Invalid number of arguments." };
		}
	}
}
