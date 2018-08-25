﻿# **SmartBotKit**

**SmartBotKit** is a personal collection of plugins developed for **SmartBot**, a bot for **Hearthstone** videogame.

Please be aware that using bots/cheats is against **Blizzard** EULA:

http://eu.blizzard.com/en-gb/company/legal/anti-cheating.html

These plugins were developed just for fun, and are shared here only for educative purposes. 

I don't use bots, I'm just a enthusiastic programmer that like to learn new things about how are built and how functions this kind of software for video games.

# **The Plugins**

## SmartBotKit.Core
This is the main assembly required ﻿for all my plugins. So you need to install this.﻿ 

This is a public API that extends the official SB API (nothing special, just a little bit of reusable code used ﻿for my plugins).

The library exposes members to interoperate with SmartBot process and Win32 API, which are required by my plugins

Installation:

![](https://i.imgur.com/wdB95Aa.png)

## Plugin Template
This is just a plugin template written in VB.NET language.

You can start writting a new plugin for SmartBot taking this template project as a startup point.

![](https://i.imgur.com/PbH9PRY.png)

## App Launcher
A plugin that automate external files and programs execution at SmartBot's startup.

You can run any kind of executable file type, which is not limited to .exe files.

You can even run Blizzard's Battle.net client if you like. 

Settings:

![](Images/App%20Launcher%20-%20Settings.png)

## Auto-Injector
A plugin that automate SmartBot injection to Hearthstone process.

That is, everytime you run Hearthstone while SmartBot is running and this plugin is enabled,
the plugin will auto-inject the Hearthstone process, and auto-start the bot if you specified to do that in the plugin settings.

Settings:

![](Images/Auto-Injector%20-%20Settings.png)

## Bounty Hunter
A plugin that completes quests, schedules ranked mode and level up heroes.

Settings:

![](Images/Bounty%20Hunter%20-%20Settings.png)

## Challenge Notifier
A plugin that notifies when a friend challenge is received, like the 'Play a Friend' challenge.

PLEASE NOTE THAT THIS PLUGIN IS EXPERIMENTAL, UNFINISHED, AND IT WAS INTENDED FOR MY PERSONAL USAGE ONLY,

BUT IM SHARING IT SO YOU CAN USE IT IN CASE OF YOU ACCOMPLISH THE CONDITIONS TO USE IT.

FOR BEST RESULTS I SUGGEST TO USE THIS PLUGIN IN ADDITION TO MY OTHER PLUGIN: 'HEARTHSTONE RESIZER', SPECIFYING A RESOLUTION OF 640X480, THAT WAY IT WILL NEVER FAIL TO DETECT A CHALLENGE INVITATION.

IF YOU WANT TO SEE MORE RESOLUTIONS SUPPORTED BY THIS PLUGIN,
YOU CAN CONTRIBUTE BY SENDING ME A SCREENSHOT OF HEARTHSTONE WINDOW RUNNING IN THE DESIRED SIZE THAT YOU WANT TO SUPPORT WHEN A FRIEND CHALLENGES YOU. SEE THIS SCREENSHOT AS A EXAMPLE

![](https://i.imgur.com/pIeRQyy.jpg)

Settings:

![](Images/Challenge%20Notifier%20-%20Settings.png)

## Emote Factory
A plugin that builds configurable rule conditions to send or answer to opponent emotes.

The plugin has also a condition to squelch/mute the enemy. 

Settings:

![](Images/Emote%20Factory%20-%20Settings.png)

## Garbage Cleaner
A plugin that cleans temporary files generated by SmartBot and its plugins.

Settings:

![](Images/Garbage%20Cleaner%20-%20Settings.png)

## Hearthstone Resizer
A plugin that maintains a fixed size and location for Hearthstone window.

You can configure the plugin to resize the window every timer tick, or each 5 ticks, for example.

The plugin is aware of when Hearthstone window is maximized, and when it is at fullscreen mode.

It will not try to move/resize its window in those circunstances.
(however, I didn't tested the fullscreen mode detection on multi-monitor configurations)

Note that I didn't provided any 16:9 resolution just because Hearthstone process does not like those resolutions;

when attempting to resize Hearthstone window to a 16:9 size, its process will automatically change to a different size.

Settings:

![](Images/Hearthstone%20Resizer%20-%20Settings.png)

## Offline Server handler
A plugin that handles the bot behavior when the server gets down. 

Note that it ﻿does not ﻿handle ﻿lag, ﻿local network inactivity﻿﻿ neither ﻿authentication connection problems.

I totally suggest to use this plugin to avoid losing rank stars o farmed gold botting in ArenaAuto mode when the server is down.

Settings:

![](Images/Offline%20Server%20handler%20-%20Settings.png)

## Panic Button
A plugin that stops or terminates SmartBot process when a specified hotkey combination is pressed.

You can stablish a hotkey combination of 1, 2 or 3 simultaneous keys.

You can literally specify any single keyboard key or override any special hotkey (like CTRL+C) from the available range of keys that I provided.

The plugin registers a new, temporary system-wide hotkey.

During the lifetime of SmartBot process and while the plugin is activated, you can press the hotkey combination anywhere on the screen.

When the plugin is deactivated or SmartBot process is terminated, the system-wide hotkey is unregistered.

Don't be worried about, Windows operating system will ensure itself that the temporary hotkey ﻿gets unregistered,
so the functionality of any modified key (or overriden operating system hotkey) will return to normal. 

Settings:

![](Images/Panic%20Button%20-%20Settings.png)

## PlayVIG Adverts Remover
PlayVIG is a software that pays you for playing Arena or Ranked mode in Hearthstone (and for other games).

The program automatically shows a top-most advert window for around 20 seconds every time you end a game in Hearthstone while a PlayVIG quest is active.

This plugin will mute the audio volume of PlayVIG process and remove any advert window shown.

Settings:

![](Images/PlayVIG%20Adverts%20Remover%20-%20Settings.png)

## System Tray Icon
A plugin that creates a system tray icon with menu commands to handle SmartBot and Hearthstone visibility.

Preview:

![](https://i.imgur.com/Kpx6rXQ.png)

Settings:

![](Images/System%20Tray%20Icon%20-%20Settings.png)

## TaskBar Informer
A plugin that prints statistical information on the taskbar icon and also displays a progress bar for Arena mode.

For example, when you are in a arena run, it will display the current wins and losses,
this way you can keep track of your progress when SmartBot window is minimized.

For other game modes, it will only display your hero's class name and the enemy's class name﻿﻿.
Since the visible text capacity of a taskbar icon is very small, I decided to just display that info. 

Preview:

![](https://i.imgur.com/4kU0sbu.png)

Settings:

![](Images/Taskbar%20Informer%20-%20Settings.png)

## Window Restorator
A plugin that reminds the last SmartBot's window size and position and restores it at the next program startup.

It also restores the maximized state if SmartBot was maximized when you terminated its ﻿process.

It will don't restore the minimized state since I consider it useless. (who wants to run minimized a program?)

Settings:

![](Images/Window%20Restorator%20-%20Settings.png)
