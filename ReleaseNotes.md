# The Release Notes #

# Unreleased #

# 1.1.7 #
  * [Issue 43](https://code.google.com/p/coab/issues/detail?id=43), multi-class HP mis-calculated
  * [Issue 65](https://code.google.com/p/coab/issues/detail?id=65), notes a few crashes, one related to SpellConfusion is now fixed
  * [Issue 66](https://code.google.com/p/coab/issues/detail?id=66), fixed attacks (missile,spells) going through walls
  * Fixed bug the you cannot modify stats of characters, introduced in 1.1.6 refactoring

# 1.1.6 #
  * [Issue 62](https://code.google.com/p/coab/issues/detail?id=62), Fixed crash in coin drop code, if you pressed return on empty input, and other fix-ups related to number input
  * Fixed issue with player name input/saving
  * Fixed crash in quick combat
  * Fixed crash with ResistCold and ResistFire spell

# 1.1.5 #
  * [Issue 57](https://code.google.com/p/coab/issues/detail?id=57) & 52, Fixed crash in game script engine
  * [Issue 58](https://code.google.com/p/coab/issues/detail?id=58), Fixed crash when removing Ring of Wizardry
  * Fixed spelling and formatting mistakes

# 1.1.4 #
  * [Issue 59](https://code.google.com/p/coab/issues/detail?id=59), Fixed treasure distribution code missing gems & jewellery
  * [Issue 60](https://code.google.com/p/coab/issues/detail?id=60), Fixed the reset of Area flags, meaning events in latter areas would not occur

# 1.1.3 #
  * [Issue 54](https://code.google.com/p/coab/issues/detail?id=54), Fixed join items command, to not loose items
  * [Issue 55](https://code.google.com/p/coab/issues/detail?id=55), Fixed crash from readying items from wrong class
  * [Issue 56](https://code.google.com/p/coab/issues/detail?id=56), Fixed pooling of party money loosing gems and jewellery
  * [Issue 61](https://code.google.com/p/coab/issues/detail?id=61), Fixed the group view display of AC
  * Fixed colouring of sprites, missing black pixels
  * Fixed sharing not removing treasure from ground


# 1.1.2 #
  * [Issue 53](https://code.google.com/p/coab/issues/detail?id=53), Casting Cloud Kill in combat crashed the game
  * [Issue 49](https://code.google.com/p/coab/issues/detail?id=49), Fixed crash related to loading monsters spell's (random pre-combat crash)
  * [Issue 48](https://code.google.com/p/coab/issues/detail?id=48), Temple purchases set you money to the cost, not subtracting the cost from money
  * [Issue 37](https://code.google.com/p/coab/issues/detail?id=37), Magically killed foes, are not truly dead

# 1.1.1 #
  * Mac OS X release

# 1.1.0 #
  * Game now installs and plays on Windows Vista (thus minor version update)
  * Saved games and other generated data is now located under "My Documents\Curse of the Azure Bonds"
  * Installer now has custom graphics, and cleans up older installers correctly
  * Added basic crash logging

# 1.0.21 #
  * [Issue 36](https://code.google.com/p/coab/issues/detail?id=36), Fixed staff-sling weapon targeting
  * [Issue 44](https://code.google.com/p/coab/issues/detail?id=44), Fixed the modify player screen so edits to exceptional strength stay
  * [Issue 45](https://code.google.com/p/coab/issues/detail?id=45), Fixed the Order menu (found off Encamp, Alter), so you can select and place the party members
  * [Issue 46](https://code.google.com/p/coab/issues/detail?id=46), Fixed horizontal menu scroll via comma and dot keys

# 1.0.20 #
  * [Issue 32](https://code.google.com/p/coab/issues/detail?id=32), Fixed manual targeting
  * [Issue 33](https://code.google.com/p/coab/issues/detail?id=33), Fixed Magic-Users spell selection on levelling to show new spells, not current
  * [Issue 34](https://code.google.com/p/coab/issues/detail?id=34), Fixed the combat Aiming crash at the edges of the screen
  * [Issue 35](https://code.google.com/p/coab/issues/detail?id=35), Fixed Magic-Users spell selection on levelling to show level 5 spells
  * [Issue 38](https://code.google.com/p/coab/issues/detail?id=38), Loss of excess Exp when level, was in original DOS game, but I've removed it
  * Add new cheat to sort treasure (most valuable at top)
  * Screen Capture now save to "My Pictures" as "Curse - xxxx.png"
  * Settings are now kept across upgrades

# 1.0.19 #
  * Fixed the Pool of Radiance player import code. You may now transfer you older saved games
  * Fixed shop items displaying so the long items display correctly (same as DOS)
  * Fixed the way the list code controls when to display 'Prev' as it was not showing always correctly

# 1.0.18 #
  * Add UI to Installer, and Short-cut added to Start Menu
  * Fixed issue with the icon editor getting stuck if you try change the head of your icon

# 1.0.17 #
  * Fixed the order of items in the shop, to be the same as original (after changes in 1.0.15)
  * Fixed Character creation class choosing
  * Made the new cheat actually work
  * Add explicit close of a .dax file when loading to allow the original to run concurrently in DOSBox better

# 1.0.16 #
  * Added new cheat, to allows demi-humans to play any class.
  * Fixed crash when you lose a battle (introduced in 1.0.15)
  * Fixed [issue 30](https://code.google.com/p/coab/issues/detail?id=30), cast screen missing apostrophe in players name

# 1.0.15 #
  * some major refactoring that may have broken game play.
  * Fixed the 1.0.14 Installer which was missing an .dll
  * Added ripped sounds to the games as working out how the PC Speaker worked was taking too long, and sounded to bad. Ripped samples are from part PC Speaker and part Tandy sounds, which are much better
  * Enhanced the 'Dump Monsters' debugging, it now dumps all monsters plus all details shown in my blog post, to an monsters.html file in the current working directory
  * Started working on decoding the affects and their meaning, to clean-up above monsters.html

# 1.0.14 #
  * Added cheat to remove the race based level limits.

# 1.0.13 #
  * Fixed issues stopping the end-game sequence from working, so the game is now winnable
  * Fixed the crash the happened when sword of Frost, or Flame readied
  * Fixed the way text wrapping happens, so the end game text is displayed the same as DOS
  * Fixed the animation picture code, so end-game animations are drawn correctly
  * Fixed small 3D view rendering problem with the far distance wall
  * Cheats settings are now saved in your user profile, so you don't have to keep turning them on
  * Time now passes correctly when resting or searching and after combat
  * Altered the Area Map to now overlay the arrow icon cleanly
  * Added new cheat "Improved Area Map" that shows doors in blue

# 1.0.12 #
  * Fixed the Fear spell, so it is removed when combat is ended
  * The icon designer now reverts to your original colours when you ‘Exit’ a sub-menu instead of keeping the changes
  * Fixed the price text when buying healing from a Temple
  * Fixed crash when removing cursed items


# 1.0.11 #
  * Fixed crash, caused by trying to load the 8 frame Medusa animation - [Issue 27](https://code.google.com/p/coab/issues/detail?id=27)
  * Using the 'Dust of Disappearance' crashed the game in casting stage
  * Displaying magic effects after using the Dust showed Invisible as well as <no spell effects>
  * Effects were not be restored correctly from a saved game
  * Player items were loaded in the opposite order compared to the DOS version
  * Effects were wearing off too quickly. In the Mulmaster Beholder Corps battle the Dust was gone after round 3


# 1.0.10 #
  * [Issue 9](https://code.google.com/p/coab/issues/detail?id=9) - (Was fixed in rev 121), but now tested to prove that is fixed
  * [Issue 8](https://code.google.com/p/coab/issues/detail?id=8) - Combatant placement is now correct for large groups
  * [Issue 25](https://code.google.com/p/coab/issues/detail?id=25) - Trading money between party members was broken
  * [Issue 14](https://code.google.com/p/coab/issues/detail?id=14) - Attacks of opportunity now occur
  * [Issue 18](https://code.google.com/p/coab/issues/detail?id=18) - Scribing spells, now works (tested magic users + rangers)
  * [Issue 24](https://code.google.com/p/coab/issues/detail?id=24) - Trying to leave Tilverton crashed game
  * [Issue 2](https://code.google.com/p/coab/issues/detail?id=2) - Installing on Win XP x64 - Tested this works
  * [Issue 11](https://code.google.com/p/coab/issues/detail?id=11) - Shambling Mounds should take half damage - Tested this works

# 1.0.9 #
  * [Issue 5](https://code.google.com/p/coab/issues/detail?id=5) - The correct exit message now plays when you try leave the Moander temple
  * [Issue 6](https://code.google.com/p/coab/issues/detail?id=6) - Moander sigil now fades at the correct speed
  * [Issue 23](https://code.google.com/p/coab/issues/detail?id=23) - Magic Darts are now pluralised correctly
  * [Issue 10](https://code.google.com/p/coab/issues/detail?id=10) - Ranged weapons (fireball spell) can no longer target through walls
  * [Issue 21](https://code.google.com/p/coab/issues/detail?id=21) - The treasure picture now is displayed when sharing out treasure
  * [Issue 22](https://code.google.com/p/coab/issues/detail?id=22) - The world map icon now flashes
  * [Issue 20](https://code.google.com/p/coab/issues/detail?id=20) - This encounter now works
  * Fixed text problem on the large map, where the last four characters were not cleared off the screen
  * [Issue 19](https://code.google.com/p/coab/issues/detail?id=19) - The correct event happen when dealing with Dracolich

# 1.0.8 #
  * [Issue 17](https://code.google.com/p/coab/issues/detail?id=17) - Missing targeting cursor - a DAX file was missing from the installer
  * [Issue 16](https://code.google.com/p/coab/issues/detail?id=16) - Copying items between players resulted in duplication
  * Overhauled the cheats - You can now activate the original cheats via context menu, plus a few extras. Remember to press '-' to activate "god intervene"
  * Fixed a mistake, that stopped you from memorising spells when resting (more work needed in the area of spells)
  * Fixed a mistake, that said you had cast a combat spell in non-combat when you chose to cancel

# 1.0.7 #
  * Fixed the "You have lost the fight"/"Zero EXP" noticed fighting Drow patrols in Save E
  * Fixed crash when lightning bolt spell hits a wall
  * Turned off the "always on" detect magic effect that was left in after debugging the issue
  * Fixed 'Paul's save game F' forward into big fight. The combat now actually begins
  * Added a cheat, to allow Saving Throws to always succeed for the party. Some spells however do not make saving throws, and other have lesser spells cast when the major fails
  * Fixed a number of small mistaken translations
  * Fixed the installer so older installations are removed, and from 1.0.7 on wards, older installers will not over install newer installations
  * As always, **lots** of renaming of the variables/functions

# 1.0.6 #
  * [Issue 3](https://code.google.com/p/coab/issues/detail?id=3) - Detect magic cast when sharing treasure now works, therefore you can see the magic treasure
  * Fixed the function that checks if any team member has a spell, to return the correct team member (and therefore not crash)
  * Paladin's can now cure other players from the 'view player' screen
  * [Issue 4](https://code.google.com/p/coab/issues/detail?id=4) - combat screen background colour is now gray not black
  * Fixed 'Paul's save game E' play problem correctly
  * Fixed combat targeting via the Aim sub-menu
  * Fixed logic in the remove affect code, that meant they were not being removed
  * Translated more assembly, and name/renamed functions and variables as there purpose became clear
  * Fixed the clipping of the combat screen. Large monsters were over drawing the board, which is not redrawn
  * Implemented video ram save/restore so the missile items draw correctly in combat
  * Changed the DaxFileCache to copy data before returning. This slows the game down more, but stops merged pictures from being wrong on second viewings (camp fire in DEMO)

# 1.0.5 #
  * Loading saved games with Druids/Ranger/Paladins/Magic-Users/Clerics was not translation complete, and would therefore crash
  * Saving newly created characters incorrectly ask if you wanted to overwrite the file.
  * The display of a characters AC was +/- flipped.
  * The save game from the training menu did not work
  * The item name list was missing a blank string, thus causes items part way through to display with a off by one name.
  * Fixed the calculation of AC bonus from Dexterity
  * Fixed how losing combat was treated, so you now don't get shown the EXP rewarded screen.
  * Scrolling through the party list was broken.
  * Fixed small typo in the code-wheel (even though it turned off in the game)
  * Fixed the display names on spells. There was many blank strings missing.
  * Fixed spell memorizing screen
  * Fixed some menu that would not accept input like Exit
  * Fixed the treasure found code to handle not being in combat (SSI bug)

# 1.0.4 #
  * Scripted portrait pictures are displayed. This can be seen in the DEMO, with the village picture.
  * Fighter hit points are now correctly limited/managed in the modify player screen
  * The [and ](.md) keys have been mapped to keypad 'home' and 'end' so the game is playable on laptop keyboards

# 1.0.3 #
  * Fixed the CPU thrashing that occurred while waiting for user input in parts of the game.
  * Many changes have been made so the Demo runs to completion, and now it seams to be exception (crash) free. I had it running for 5 hours at work, with no exceptions.
  * Fixed some code translations issues.
  * Fixed some data translations issues. Used to control menu etc.
  * Worked on Spell memorization menu (not complete)
  * Fixed the synchronisation between the engine and UI so they both shutdown when the window is closed, or the game exited.
  * Made some improvements to how the installer upgrades itself (not very tested or checked in yet)

# 1.0.2 #
  * mistake release

# 1.0.1 #
  * Very first installer build, very alpha.