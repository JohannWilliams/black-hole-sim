# black-hole-sim

## About
This project was designed and developed between my classmate Tom and myself for our Senior project at St. Edwards University.
The problem we were trying to solve was the ability for students and those interested in gravity and black holes to have a fun and easy to use black hole simulator. There are many out there, but the ones that are easy to find are ether used in labs to simulate, study, and research new frontiers on black holes, or simple and basic web apps that don't offer much insite into what is known today about them. What we developed was a Unity project that gives users the ability to run a simulation on a black hole with many objects and when ready, have the ability to save the date from that simulation run for viewing late. This data that can be saved is placed into one of two CSV files. Users have the ability to then go through the data and see what has happened to the black hole over time as well as see information on the objects orbiting the black hole.
- Date like:
    - mass
    - velocity
    - momentum
    - status (orbit, dead, excapted orbit)
    - among a few other things

Because of the constraints of Unity and processing power, we used the mass of our sun and then converted that into the mass of our objects on the screen by converting one solar mass into one mass in Unity. With this we also calcuate things like the radius of the black hole and its gravitational influience on the objects in its reach, as well as the gravitational influence each object has on one another. Now, because of time constraints as well as developing this project in unity, we decided to go with Newtonian Physics for simplicity. It needs to be noted that as one gets closer to a black hole the laws as we know it, by Newton, begin to break down and because of this other laws should be used. Ones that work with Space and Time.

## Future Work
In this repository, I am going to be modifying our work to run more efficiently and closer to that of a true black hole simulator that what it is now. Graphics for visual representation will be updated, and the scenes for discribing a black hole will be updated to guide users through not only how black holes as we know it form, but guild a user through the physics related to gravity and space time.