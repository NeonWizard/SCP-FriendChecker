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
			List<Player> targets = new List<Player>();

			// -- Fetch target(s)
			if (args.Length == 1) // -- Searching for friend circles for specific user
			{
				// -- Find target player by args[0]
				if (short.TryParse(args[0], out short pID))
				{
					foreach (Player p in server.GetPlayers())
					{
						if (p.PlayerId == pID)
						{
							targets.Add(p);
						}
					}
					if (targets.Count == 0) return new string[] { "Nobody with that player ID exists." };
				}
				else
				{
					return new string[] { "Can't parse player ID." };
				}
			}
			else if (args.Length == 0) // -- Searching for all friend circles
			{
				targets = server.GetPlayers();
			}
			else // -- poopy di scoop. scoop diddy whoop. whoop di scoop di poop.
			{
				return new string[] { "Invalid number of arguments." };
			}

			// -- Calculate friends
			List<string> output = new List<string> { "Listing friends..." };
			foreach (Player target in targets)
			{
				// -- Lookup target's friends from SteamAPI
				string[] friendIDs = this.plugin.steamAPI.GetFriends(target.SteamId);

				// -- Cross-reference target's friends with online players
				string[] onlineFriendIDs = server
					.GetPlayers()
					.Where(x => friendIDs.Contains(x.SteamId))
					.Select(x => x.Name)
					.ToArray();

				// -- Add to output if player only if player has any friends online
				if (onlineFriendIDs.Count() > 0) output.Add("[" + target.Name + "] " + string.Join(", ", onlineFriendIDs));
			}

			return output.ToArray();
		}
	}
}
