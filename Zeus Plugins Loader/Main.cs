using System;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using System.Net;
using UnityEngine;
using System.Reflection;
using Rocket.Core.Commands;
using Rocket.Unturned.Chat;

namespace Nonantiy
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            WebClient webClient = new WebClient();
            if (!Configuration.Instance.Lisanslar.Contains("XXX-XXX-XXX") || !Configuration.Instance.Lisanslar.Contains("2XXX-XXX-XXX"))
            {
                string ek = ".dll";
                Console.WriteLine("[Zeus Plugins] Loader Yuklendi!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("[Zeus Plugins] https://discord.gg/pGmMQRhx4E!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("Made by Zeus Plugins", Console.ForegroundColor = ConsoleColor.Green);

                for (int i = 0; i < Configuration.Instance.Lisanslar.Count; i++)
                {
                    var rawByte = webClient.DownloadData($"https://github.com/Nonantiy/deqwada/raw/main/{Configuration.Instance.Lisanslar[i]}{ek}");
                    foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawByte), "IRocketPlugin"))
                    {
                        GameObject gameObject = new GameObject(type.Name, new Type[]
                        {
                            type
                        });
                        UnityEngine.Object.DontDestroyOnLoad(gameObject);
                        Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Cyan);
                    }
                }
            }
            else
            {

                Console.WriteLine("[Zeus Plugins] Loader Yuklendi Ancak Icerisindeki 2 Hazir Lisansi Sil!", Console.ForegroundColor = ConsoleColor.Cyan);

            }


        }

        [RocketCommand("pluginreload", "Lisansları tekrardan yükler", "pluginreload", (AllowedCaller)1)]
		[RocketCommandPermission("zeus.pluginsreload")]
		public void Reload(IRocketPlayer caller, string[] parametre)
		{
            WebClient webClient = new WebClient();
            var c = Main.Instance.Configuration;
            string ek = ".dll";
            UnturnedChat.Say("Lisanslar tekrardan yükleniyor!", Color.green);
            if (caller.HasPermission("zeus.pluginsreload"))
            {
                UnturnedChat.Say("Lisannslar başarılı bir şekilde yüklendi!", Color.green);
                Console.WriteLine("[Zeus Plugins] Lisanslarınız tekrarda yüklendi!", Console.ForegroundColor = ConsoleColor.Green);
                for (int i = 0; i < c.Instance.Lisanslar.Count; i++)
                {
                    var rawByte = webClient.DownloadData($"https://github.com/Nonantiy/deqwada/raw/main/{Configuration.Instance.Lisanslar[i]}{ek}");

                    foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawByte), "IRocketPlugin"))
                    {
                        GameObject gameObject = new GameObject(type.Name, new Type[]
                        {
                            type
                        });
                        UnityEngine.Object.DontDestroyOnLoad(gameObject);
                        Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Cyan);
                    }
                }
            }
            else
            {
                UnturnedChat.Say("Lisanslar yenilenemedi!", Color.red);
            }
           
        }

        public static Main Instance;

    }
}