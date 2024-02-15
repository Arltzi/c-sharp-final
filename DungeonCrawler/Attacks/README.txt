Alex Gulewich
Feb, 14, 2024
Attacks

The attacks classes are created to easily and quickly provide a variety of attacks that are assignable to anything.
As well as being very easy to swap out on a whim.
This will allow a greater flexibility to the enemies with ease of implementation of new attacks as well as,
The ease of flexibility and choice for the player


### USAGE

Best practice would be to create an attack field then assign the attack you would like to use to that.
From there you would call the Action method to activate the attack.

CREATE EXAMPLE)
	Attack myAttack = new Slash(ConsoleColor.Red);

Calling Example)
	myAttack.Action(4, 4, Direction.UP);