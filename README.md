<p align="center">
    <img src="https://i.imgur.com/CixzxkB.png" width="1000" height="200" />
</p>

This is the repo readme for the project "Pro-Gen" City Builder, a procedurally generated city builder.
This repo will contain the projects files for Pro-Gen as well as a tutorial on how to use it for yourself and how you can tweak it if required.

<p align="center">
    <img src="https://i.imgur.com/54NxN7H.gif" width="270" height="180" />
    <img src="https://i.imgur.com/PS1CIG1.gif" width="270" height="180" />
    <img src="https://i.imgur.com/3DcjJXf.gif" width="270" height="180" />
    <img src="https://i.imgur.com/Zoc2Pfc.gif" width="270" height="180" />
    <img src="https://i.imgur.com/PcqCSAF.gif" width="270" height="180" />
</p>
  
## Technology Used
  The primary software used is Visual studio and Unity. The language C# was used as this the primary language for unity.
  Windows Version: Windows 10 19041.985
  Visual Studio Version: Community 2019 16.10.0
  Unity Version: 2019.4.14f1
  
## Understanding the code
  The way this project and code works is all based around the use of procedural generation to help create a city scape. In order for it to do this the process is broken down into multiple parts. The first task is to generate a Perlin noise texture using Unity’s built in Perlin Noise function. It assigns each pixel a colour using the function and places them into a texture. This texture is then used to decide the height of the buildings when they are placed by multiplying the height of the building by the float assigned to the colour of the pixel and an external factor. In order for the program to decided where to put the prefabs (buildings, streets, spawners, objects etc) a grid is first created using the bounds of the Perlin grid step size. This variable is the factor of which the Perlin noise generated relates to the grid inside unity, basically how big you want your city to be. This grid is what keeps track of the street positions and everything else we want to generate by assigning different sections a different value. This value is then used when placing the objects into the scene by telling the program what to place and where to place it. For example, when the streets are created, both the horizontal and vertical streets are given different values to remove the need of doing a rotation sometimes on the street prefab. By doing this it also tells us when the crossroads are used so the program know knows when to use the horizontal street, vertical street, and crossroad street. At the end, the program loops through a nested for loop the size of the Perlin grid step size variable placing the objects where specified until the entire grid is completed.
  
<p align="center">
    <img src="https://i.imgur.com/zBUyq7E.png" width="250" height="200" />
    <img src="https://i.imgur.com/VlPse4P.png" width="250" height="200" />
    <img src="https://i.imgur.com/3L00QCG.png" width="250" height="200" />
    <img src="https://i.imgur.com/K2CSQcQ.png" width="250" height="200" />
    <img src="https://i.imgur.com/Jq9OWJ2.png" width="250" height="200" />
    <img src="https://i.imgur.com/Abi5ev0.png" width="250" height="200" />
</p>

 ## How to use it
<img src="https://i.imgur.com/SOWFvXD.png" width="450" height="280" align="left"/> 
 The image you see to the left is the primary way that you can tweak and edit how the city is generated. Each of the variables will be individually described below to help understand what each of them does but for the high-level description the first 5 determine the size of the Perlin noise and how it directly correlates to the scene. Generally, the first 2 can be left untouched as they determine the size of the texture for the Perlin noise and the scale of it (how far zoomed in it is) is the one you want to change. This variable can make the transitions between colours more fluid or much harsher. This will affect how the height of the buildings look.
 The latter 8 variables are the prefabs for the objects used to construct the city. If you want to change the buildings or the streets for other models this is where you would do it. The car spawner and player spawner would be best tweaked in the code as they are a collection of objects grouped together rather than one model.
  
## The variables and what they mean
This is a brief description of each of the variables that you can change and tweak and what they correlate to. This will help give a better understanding of how everything works together.

* **Width:** This variable determines the width of the Perlin noise texture.
* **Height:** This variable determines the height of the Perlin noise texture.
* **Scale:** This variable determines the scale or zoom level of the Perlin noise texture. Lower value more zoomed out, smoother transition. Higher value more zoomed in, harsher              transition. 
* **Perlin Gird Step Size X:** The horizontal size of which the Perlin noise texture will apply to the scene in unity.
* **Perlin Gird Step Size Y:** The vertical size of which the Perlin noise texture will apply to the scene in unity.
* **Prefab(Now called Building in code:** The building prefab. Have to be scaled to the size of 1 unity square otherwise collisions will occur.
* **Street Vert:** The vertical street prefab.
* **Street Hor:** The horizontal street prefab.
* **Street Cross:** The crossroads street prefab.
* **Lamp:** The lamp prefab, this code can also be used to add other objects to the street such as bus stops or trash cans etc.
* **Carspawner:** The car spawner prefab. This also blocks the user from being able to walk off the edge of the city (works in conjunction with the boundaries also defined).
* **Player Spawner:** The player spawner. This is attached to on of the street prefabs and is placed on the first horizontal street segment placed in the scene.
* **Objects:** These are the objects that the player has to collect in order to complete the game, not nessisary for the city generated but again like the lamp, can be used to help bring other objects into the city or a game function etc.
  
## Link to developer walkthough and images
  Here is link to a YouTube video where I go over the code in a bit more depth to help understand the finer mechanics, controls, and usage of this program: 
[**Youtube video link of developer walkthrough**](https://youtu.be/2YF7OOan8tI)

## Sumamry and Game link
Download the code and have an experiment with it using the help provided and the trial and error with editing the code. If you want an example of how this program can be used, I created a simple game as a proof of concept ([**Link here**](https://fich019.itch.io/pro-gen-city-explorer)) but you don't have to stick to it. Take it and make it your own!
Thanks for downloading and reading, have fun,  
  
Zack  


