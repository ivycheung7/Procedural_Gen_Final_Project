# Procedural_Gen_Project_3

Capture the Dog

Goal is to capture the dog and take it back to your base.

WASD or Directional arrows to move
Space to dash attack

You can hold the dog and admire it for a while but there are enemies that can kill you. 
(You stole their dog, why wouldn't they attack you)

Once you have saved a dog, you go to the next level and steal some more dogs!
The enemies however will learn from before and EVOLVE their behavior. 
(Evolutionary Algorithm)

How it works for now:

So humans have XY, XX, there are rare cases like XXXY but anyway.
Pink squares have XXYY-XXXY but instead of X and Y, we have directions!
0,1,2,3,4,5,6,7,8
Up, Right, Down, Left, Northeast, Southeast, Southwest, Northwest, and None

They're initially randomized so it can look like
1252-8888

This pattern will be their movements that they will repeat in their searching (for the player) phase.

When the player gets within range of the enemy, the enemy will chase the player until the player is out of range, then it resumes their set path. While chasing, they are able to dash attack and reclaim their pride by killing you.

Note: Short-sightedness is a terrible affliction for these enemies. But at least they have a dog.

Another Note: Both the player and the enemy may dash and collide together, but nothing happens cause it'll cancel out. This is a feature not a bug. (next update they will bounce off each other okay ;_;)

Heuristic: 

The amount of time it spent chasing the player + how many times it killed the player - if it died

		return (isDead) ? (killedPlayerCount * 100) + timeClosestToPlayer - 10 : (killedPlayerCount * 100) + timeClosestToPlayer; 

The enemies will mutate at the end of the level.

The lowest performing enemy is fired and then replaced with someone entirely different.

Since some of this code was rushed before the final presentation, there were shortcuts like, how the superior enemy will be chosen to share its genes with the rest of the parents (except the loser of course. entirely random)

		int amtGenesPassed = Random.Range(5,8);
		int amtMutation = Random.Range(0,3);
        
When mutating, the enemy will share the genes with the best performer. (From now on, let's call it, the Enemy of the Month - EOM) 

There will be a random chance of mutation, 8 - ([5,8]) genes will randomly be changed.
Then, there will be 0-3 mutations passed on from EOM.

And now you have your new children!

Music is not incorporated yet, despite the present files in resources.