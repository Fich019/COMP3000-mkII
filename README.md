# Pro-Gen City Builder
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
 My project is a aim trainer built inside of OPENGL. I started with a camera and player movement script from learnOpenGL and a shader and built up from there. I started by creating walls, a floor and a ceiling to enclose the player in a box. From there I stopped the player leaving said box so they would remain in the playable area. There is a different coloured wall on the back which draws the players eyes to it. This is the main wall where the targets are located. The player can then aim at the targets with the crosshair provided on screen and use left click on their mouse to "shoot" at them and destroy them. The target is then randomly placed somewhere else on the wall. I got the idea from playing first person games myself, I want somewhere to warm up my aim and to practice before playing. 
  
## The variables and what they mean
This is a brief description of each of the variables that you can change and tweak and what they correlate to. This will help give a better understanding of how everything works together.

* **Width:** 
* **Height:** 
* **Scale:** 
* **Perlin Gird Step Size X:** 
* **Perlin Gird Step Size Y:** 
* **Prefab:** 
* **Street Vert:**
* **Street Hor:**
* **Street Cross:**
* **Lamp:**
* **Carspawner:**
* **Player Spawner:**
* **Objects:**       
  
## Link to developer walkthough and images

[**Youtube video link of developer walkthrough**](https://www.youtube.com/watch?v=RzFBgkkDAlQ&feature=youtu.be)
