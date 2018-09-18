VIP:VR Assignment 1
---

Alright, now that we've assigned various jobs to everyone, we have to start figuring out A. how those parts work individually, and B. how those parts work in concert. Unfortunately, I'm not an omniscient being, so this is where everyone's individual legwork comes in. Individual assignments are after the break.

We'll be working in git because collaboration reasons. Create an empty scene called `SANDBOX-LASTNAME-FIRSTNAME` in the `SCENES` folder to prototype in and push it to the repository to share with the group. Everyone should be working in their own scene for now because merging scenes specifically is terrible and everyone should be experimenting concurrently.

To clarify, this project was never meant to be the end-all-be-all for this team. I neglected to mention this, but in a way, it's our "Hello World" as a group where we figure out how to work together on a project that can be broken down into many smaller parts. The hope is that we pull this off, and we pull it off well, and everyone begins to specialize. From there, we have a lot of options in terms of what else to work on, which we can figure out when we get there. One of the thoughts was to work on something for a museum display to take advantage of the fact they're stable installations bought by an institution with money to burn, but Dr. Barmaki may also have research to work towards once she's a bit more settled on campus.

So, in order to horde ideas for the next project, we'll start using [yet another Trello Board](https://trello.com/b/5cp6lI3r/vipvr-idea-board). Please feel free to just dump all of your VR/AR related pipe dreams and ideas here, in this phase there are no bad ideas. We'll figure out what to work on in the future.

Thanks all,
-Alina

---

Alena and JC, AI team:

Start to look into how enemies behave. Start with something small, for example have a sphere enemy wander aimlessly until it 'hits' a wall or gets close to a square player, in which case, have it turn a random direction and continue moving. [Unity In Action](https://livebook.manning.com/#!/book/unity-in-action-second-edition/chapter-3)'s chapter 3 might be a useful start. Also look into [raycasting](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html).

Anton and Eric, UI team:

Start to work on VR-based interaction. This is kind of why you have the key to the Vive. Figure out how to pick up and throw objects, flick levers, that sort of thing. The git repo has the SteamVR plugin imported. It has several example scenes of various built-in interactions you have to work with. In particular, take a look at the scene in `SteamVR/InteractionSystem/Samples/Scenes/Interactions_Example`. Poke around, see how it works, see how you can replicate some of the behavior in an empty scene of your own making.

Joy and Jon, networking team:

I'll be honest, I'm not really sure where to start you off on this beyond the [Unity Documentation](https://docs.unity3d.com/Manual/UNet.html). See if you can, say, make a demo that has two instances of Unity running where each controls a separate ball, but they both see each other and keep the other version's copy of the ball's position up to date.

Vinay, world gen:

This is another area I haven't worked in before, but here's a [stackoverflow answer](https://stackoverflow.com/questions/155069/how-does-one-get-started-with-procedural-generation). Let me know what you find out, maybe start with a pseudo-randomly generated grid.

Jimmy, music man:

Compile a playlist of 15-30 representative musical tracks for the project so we can par down on what to go for stylistically. I trust your judgement, the sample was a great start!

Alex, artist:

See if you can come up with enemy designs based on the aesthetic of the game so far. Concept art is fine, if you could compile maybe 5 different options for us to work with that would be a good place to begin.

Dr. Barmaki, if you happen to find the game AI online course that you mentioned, if you could link it to the AI team that would be fantastic, otherwise don't worry about it.
