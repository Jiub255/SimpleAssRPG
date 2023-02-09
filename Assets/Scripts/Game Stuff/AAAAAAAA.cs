public class AAAAAAAA
{
    /*
     
    (MINIMAP MARKERS AND CAMERA (CHILD OF PLAYER) ARE TEMPORARILY DISABLED, TURN BACK ON EVENTUALLY) 


                    DO FIRST
                ________________

    -swords getting equipped as helmet

    -get equipment ui to update
        MakeEquipmentSlots in InventoryManager is the problem

    -improve ui/ make it work
        -kinda broken now, fixable though
        -make the health/magic/arrows etc show better in inv menu
        -make all borders and fonts the same size
        -make it scale properly to any resolution

    -finish fishing system

    -start world building soon, enough mechanics
        -draw more sprites/tilesets

    -outline all items, buildings, etc in black,
        or get rid of black outlines on player/enemies/npcs?

    -make better save system

    -fine tune knockback/stun/invulnerability system

    -resize UI and make it fit multiple resolutions

    -make sword necessary for melee attacks

    -change variables/methods to private unless needed to be public


                    TO DO
                ______________

    -finish equipment system. have SO of current equipment. make helmet type, chest type, etc. equipment items
        have attack stats depend on this SO, and the equipment screen/ player appearance    

    -keep SO list of enemy health/magic.

    -make a fishing minigame

    -work the 17 frieze patterns into some dungeon or something

    -see through clouds and stuff for environment. swamp gas, etc

    -have TalkedTo bool list for NPCs. or store somehow in ink conditions? will that get saved?

    -make state machine script for animation states? use enum.

    -make shield
        -could have a button to hold
        -could have a chance to block, have "blocked" show up in floatingdamagenumbers

    -make pet/companion

    -fine tune stuff (projectile speeds/lifetimes, hitboxes, attack delays, speeds, etc)

    -fix/isolate knockback. four different places now?

    -fix key door system. make it openable, with scene changer behind door sprite?

    -improve screen shake effect (in player/sword components)
        -none/less intense for breaking jars, etc?
        -make it stop when touching pots, etc

    -another guard script, this one a waypoint follower

    -gonna need to deal with the playerposition scriptable object. maybe

    -make inventory sortable (custom, alphabetical, others?)

    -fix double picking stuff up (still a problem?)




            COMMON PROBLEMS/FIXES
        ______________________________

    -all scenes need an eventsystem

    -transitions from any state need to have "can transition to self" unchecked. fucks with animations getting stuck



                    BUGS
                ____________

    -knockback can push you past colliders, like into a house
        -Maybe have an oncollisionenter thing to stop the player?

    -minimap overlay flips from editor to play mode. fixed with x scale set to -1, but why?


                POSSIBLY FIXED BUGS
            ----------------------------

    -sword stops hitting sometimes. not sure why

    -can't break pots in house first time there after cutscene, but can later

    -sometimes enemies drop two loot items (ive seen a heart and a coin)
        -bug might be gone now

    -hearts give two health sometimes, but not always. Because of two player colliders? even though one is trigger?
        -might be gone now

    -enemies not hitting me
        -bips stop hitting after two hits. often




            GENERAL GAME OVERVIEW
        ______________________________

    -simple, basic rpg for practice

    -all my art, stick figure bullshit

    -Morrowind style menu
        -inventory system with usable items
        -equipment menu
        -character stats (as scriptable objects) and char screen
        -magic menu

    -quests eventually?

    -turn based fighting eventually?

    -have a pet eventually, follows you, attacks enemies with AI

    -keep stats as scriptable objects (IntValue, etc) to transfer between scenes



    */
}
