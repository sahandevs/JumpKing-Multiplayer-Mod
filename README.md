# Jump King Twitch Raven Mod
This Mod allows your Twitch Chat to lend words of encouragement whilst you climb to the babe! 

## Features
### Twitch Ravens
Twitch Chat can now communicate with you within the world of Jump King! Messages from chat will be parroted by small ravens that land on the stage as you traverse it. You have the option of gating this behind a Channel Point reward for busier chats!
<p align="center">
  <img src="https://user-images.githubusercontent.com/9095972/135728881-c4a61ccb-663b-4218-8f22-9ece0366592a.gif" width="75%" height="75%" alt="Ravens land on the stage to relay messages from Twitch Chat!"/>
</p>

The Ravens can be triggered through different ways to suit your chat
- **Chat Message:** Spawns a Raven for every Chat Message that appears
- **Channel Point:** Spawns a Raven when a chatter redeems a specified Channel Point (Uses the text from that channel point!)
- **Insult:** Spawns Ravens when you fall, they choose from a pre-determined list of insults

### Chat Display
Alternatively, you can opt to display the Twitch Chat directly in your game instead!
<p align="center">
  <img src="https://user-images.githubusercontent.com/9095972/135768756-8af8db85-4c54-4ea3-9cbe-fafcebb6bf27.gif" width="75%" height="75%"/>
</p>

### Free Flying
Toggle a 'free flying' mode for the Jump King, which will let you explore or practice jumps without consequence!

_**Note:** Enabling this aspect of the mod will disable achievements in the game until the Free Flying mod is turned off in the settings!_

## Installation

**If you're not using Steam, make a copy of the MonoGame.Framework.dll in your Game's install directory first! It will make uninstalling easier!**

Check the [Release](https://github.com/PhantomBadger/JumpKingMod/releases/) page for the latest download

**Before unzipping the downloaded .zip, Right Click, select 'Properties' and if there is an option at the bottom to 'Unblock' the file, tick it, and click 'Apply'**

![image](https://user-images.githubusercontent.com/9095972/137400443-37a037bf-b1b2-407e-acea-06cea1232fdd.png)

This will ensure that the mod loader isn't treated as a harmful application when running.
- Run the Installer.UI.exe inside the Installer Folder

![image](https://user-images.githubusercontent.com/9095972/135728412-5d00983e-8827-416d-8d55-3a87a5f9f6d7.png)
- Click the '...' next to the Game Directory text box and point this at your Jump King Install Directory (the place where JumpKing.exe is)
- Click the '...' next to the Mod Directory text box and point this at the Mod folder in the Install package
- Click 'Install', a pop-up should appear confirming it has succeeded
- You should fill in and Save the Settings via the Installer first, to ensure everything runs smoothly :)
- You can now launch the game normally from Steam/however you normally launch. You can be sure the mod has been installed correctly because a Console Window will open alongside

### Common Issues
<details>
  <summary>Operation is not supported. HRESULT: 0x80131515</summary>
  
  <p align="center">
    <img src="https://user-images.githubusercontent.com/9095972/137400957-8d5399be-3e28-46de-b589-d8fea48cbe2b.png" width="75%" height="75%"/>
  </p>

  Occurs when running the game after installing.
  This is because your computer is blocking the .dlls from being dynamically loaded by Jump King. You can either right click each .dll in the mods folder, go to 'properties' and then click 'Unblock'. Or alternatively, you can run a powershell command in the mod's directory such as `dir -Recurse | Unblock-File` to unblock the files all at once. 
</details>
  
## Updating the Settings

- You can re-launch the Installer UI at any point, and click the 'Load Settings' button. The Application will attempt to load a valid settings file from the specified Game Directory
- After which you can edit the settings you desire, click 'Save Settings', and your changes will take effect next time you launch the game 

## Uninstallation

- To Uninstall the game, right click on the game in Steam, go to _Properties_, then _Local Files_, then click _Verify Integrity of game files..._
- These instructions are mirrored in the 'About' window in the installer
- For those not using Steam for the running of Jump King, you may need to redownload the game (Or if you made a copy of the MonoGame.Framework.dll you can just replace the one in the game directory with your copy!) 

# Contact
You can reach out to me on [Twitter](https://twitter.com/PhantomBadger_)

![heartchatmessage](https://user-images.githubusercontent.com/9095972/135729076-857302a4-7878-4654-b288-73283ae76090.png)

# Future Features

- [ ] Configurable exclusion word list
- [ ] Custom Insults for the Insult Ravens
- [ ] Text Wrapping for Longer Messages
- [ ] Runtime Sub-Only Toggles
- [ ] Twitch Emote Support
- [ ] Support for Different Raven Sprites
