# Zap - Code Documentation

## Description

Zap is a demo of an original rogue-like platformer game for computers. Specifics of the game can be found in the user manual.

## Libraries

This game is made using the Unity2D game engine and uses a few of its libraries. Specifically UnityEngine for communicating with the Unity engine, Unity.SceneManagement for switching scenes and Unity.Mathematics for some calculations. Then I'm using TMPro for updating text in TextMeshPro objects and Systems.Collections for Dictionary (used for an item effect - color map) and List and System.Random and Unity.Random for generating random values.

## Decomposition

Let's start from the character-centric classes:
    - CharacterBase - this is the parent class of all characters, it contains mainly some default values and definitions, which all characters need
    - Player - inherits CharacterBase. This is the class tied to the Player GameObject and contains everything around the Player
    - EnemyBase - inherits CharacterBase and is pretty much just an extension of it for enemies, since they have a bit more in common
    - MovingEnemies - inherits EnemyBase. Contains all movement related methods and values for moving enemies
    - Enemy# - inherits EnemyBase or MovingEnemies, if it needs movement. These classes are really simple, because they just call the methods defined in their parents inside of Update()
Then there is LevelManager, which is a class, that, as it's name suggests, controlls the switching of levels as well as the initialization of the levels and scene switching.
Class Item is used for controlling items. After spawning an item, it randomly chooses it's effect and handles it's interaction with the Player class.
Other kind of self-explanatory classes are Bullet, VanishingBlockScript and MenuButtonController.
There is also class CameraScript, which is used only for adjusting the camera for different aspect ratios, which I struggled to solve myself, so it is taken from https://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html

## Known Bugs

    - enemies sometimes get stuck when next to each other and another obstacle
    - bullet speed gets affected by the movement of other things it bounces from
    - player can get stuck under a vanishing block close to the ground. they can still move to the side, so it's not really a problem
    - if there are two blocks next to each other (this isn't anywhere in the game), even if they have the same height, characters can get stuck on them and be unable to move one way without jumping

## Unaccomplished things

    - better enemy ai, right now it works, but the enemies are not very smart
    - some boss fight or another kind of a better ending to the game
    - more levels
    - actual art instead of rectangles and triangles
    - sound and maybe music
    - better ui, maybe an option to pause the game, death screen, overall just some things that make the player experience better

## Conclusion

The goal of this project was to create at least somewhat fun and challenging roguelike platformer. It still feels a little unfinished because of how it looks and how the enemies behave, but despite that, the gameplay feels pretty fun and I'm pretty happy with how it turned out.