# Zap - user manual

## What is Zap?

Zap is a demo of an original rogue-like platformer game for computers.

## UI

When you first open the game, you will see a simple menu screen with only two buttons; "Play" and "Quit". The "Quit" button exits the game and the "Play" button starts the game.

When in-game, the only UI aspect is a red number in the top left corner which shows the players current health. A "YOU WON" text appears for three seconds after the player finishes the last level.

## Game world

The game world consists of:

### The Player

    A white triangle with a thin rectangle next to it, which is the players weapon.

### Enemies

    Red triangles. There are four types of enemies, which all look the same. They are different in how they behave. 
    - Type 1 - only walks back and forth (this is called roaming)
    - Type 2 - walks back and forth and if it sees the player, it starts following them
    - Type 3 - stationary, shoots rapidly at the player, if it can see them
    - Type 4 - roams, upon seeing the player starts shooting and following them

### Ground

    Grey rectangle covering the whole floor. It is always there at the first level, but on higher levels disappears for the player after their first jump and only reappears after getting to that level again from the bottom. Does not disappear for enemies.

### Basic blocks

    Black rectangles which the player or enemies can stand on.

### Vanishing blocks

    Grey rectangles that vanish after the player stops touching them after touching them. Also vanishes upon being shot by anyone.

## Items
    
    Have a chance to spawn after killing an enemy. Items have one of these effects:
    - Red - adds one health point
    - Blue - increases the time a bullet shot by the player exists
    - Green - increases the number of times a bullet can bounce
    - Yellow - decreases the minimum delay between shots
    - Cyan - increases the speed of the bullets shot by the player

## Controlls

The game is controlled using keyboard and mouse. Use A, D to move left and right, W to jump, LMB to shoot. The player character always aims towards the mouse cursor.

## The course of the game

When you enter the game a random order of existing levels is generated. The player is put at the starting level and has to try to progress to the next level by jumping up "out of screen" by which the player gets to the next level. On the way up, the player will meet enemies, which will try to kill them. Killing the enemies is not required to finish the game. The player can kill enemies by shooting them, after which they will not respawn upon reentering the level. Killing enemies has a 50% chance to drop an item, which provides a buff to the player and can be collected by touching it. After passing the last level, the player wins and is sent back to the menu.

