# Pacman.v2

Welcome to Pacman!
Use the arrow keys to control pacman through the maze and eat all the pellets. Avoid being eaten by ghosts!

Build the docker image
````
docker build --tag pacmandev -f Pacman2/Dockerfile .
````
and then run the game!
``````
docker run -it pacmandev 
``````
