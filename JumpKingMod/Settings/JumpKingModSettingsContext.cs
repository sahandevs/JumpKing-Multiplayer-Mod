﻿using System.Collections.Generic;

namespace Settings
{
    public abstract class JumpKingModSettingsContext
    {
        public const string SettingsFileName = "JumpKingMod.settings";
        public const string GameDirectoryKey = "GameDirectory";
        public const string ModDirectoryKey = "ModDirectory";
        public const string ChatListenerTwitchAccountNameKey = "ChatListenerTwitchAccountName";
        public const string TargetChatTwitchAccountNameKey = "TargetChatTwitchAccountName";
        public const string OAuthKey = "OAuth";

        public static Dictionary<string, string> GetDefaultSettings()
        {
            return new Dictionary<string, string>()
            {
                { GameDirectoryKey, "" },
                { ModDirectoryKey, "" },
                { ChatListenerTwitchAccountNameKey, "" },
                { TargetChatTwitchAccountNameKey, "" },
                { OAuthKey, "" },
            };
        }
    }
}