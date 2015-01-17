using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using TShockAPI.Hooks;
using Terraria;
using TerrariaApi.Server;


namespace ChatHookTutorial
{
    [ApiVersion(1, 16)]
    public class ChatHookTutorial : TerrariaPlugin
    {
        #region Plugin Info
        public override Version Version
        {
            get { return new Version("1.0.0.0"); }
        }

        public override string Name
        {
            get { return "LoginChanger"; }
        }

        public override string Author
        {
            get { return "Bippity"; }
        }

        public override string Description
        {
            get { return "Changes command prefix for login and register"; }
        }

        public ChatHookTutorial(Main game)
            : base(game)
        {
            Order = 501;
        }
        #endregion

        public override void Initialize()
        {
            ServerApi.Hooks.ServerChat.Register(this, OnChat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerChat.Deregister(this, OnChat);
            }
            base.Dispose(disposing);
        }

        private void OnChat(ServerChatEventArgs args)
        {
            if (args.Handled)
                return;
            TSPlayer player = TShock.Players[args.Who];

            if (player == null)
            {
                args.Handled = true;
                return;
            }


            if (args.Text.ToLower().StartsWith("/login ") || args.Text.Length == 6 && args.Text.ToLower().StartsWith("/login"))
            {
                args.Handled = true;

                player.SendInfoMessage("Instead of /login, please use .login");
            }
            if (args.Text.ToLower().StartsWith(".login ") || args.Text.Length == 6 && args.Text.ToLower().StartsWith(".login"))
            {
                args.Handled = true;

                if (args.Text.Length == 6)
                {
                    TShockAPI.Commands.HandleCommand(player, "/login");
                    return;
                }
                else
                {
                    string[] words = args.Text.Split();

                    string login;

                    if (words.Count() >= 3)
                    {
                        login = words[1] + " " + words[2];
                    }
                    else
                    {
                        login = words[1];
                    }

                    TShockAPI.Commands.HandleCommand(player, "/login " + login);
                }
            }

            if (args.Text.ToLower().StartsWith("/register ") || args.Text.Length == 9 && args.Text.ToLower().StartsWith("/register"))
            {
                args.Handled = true;

                player.SendInfoMessage("Instead of /register, please use .register");
            }
            if (args.Text.ToLower().StartsWith(".register ") || args.Text.Length == 9 && args.Text.ToLower().StartsWith(".register"))
            {
                args.Handled = true;

                if (args.Text.Length == 9)
                {
                    TShockAPI.Commands.HandleCommand(player, "/register");
                    return;
                }
                else
                {
                    string[] words = args.Text.Split();

                    string login;

                    if (words.Count() >= 3)
                    {
                        login = words[1] + " " + words[2];
                    }
                    else
                    {
                        login = words[1];
                    }

                    TShockAPI.Commands.HandleCommand(player, "/register " + login);
                }
            }
        }
    }
}
