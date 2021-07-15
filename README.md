# SoD2-Skill-Reroll
A program that automatically re-rolls survivor skills in State of Decay 2 until the desired skills are present. Uses screenshots and a screen reader to determine what skills are currently shown on screen.

This project uses the Tesseract library (https://tesseract.patagames.com/) and Input Simulator (https://archive.codeplex.com/?p=inputsimulator).

This program takes screenshots of the game window where survivors' skills appear and uses a screen reader to determine when the desired skill is present. The program automatically gives virtual keybaord inputs (arrow keys and enter) to press the UI buttons.

Before you start:

This tool does not speed up the process very much but it does automate it. In my testing it takes on average 7 minutes to get a singlee rare skill like lichenology, so in the worst case the progrram will take on average 21 minutes to finish when searching for 3 rare skills.

Instructions:

To run the program, download the .zip file from the latest release (https://github.com/BenjoKazooie/SoD2-Skill-Reroll/releases) and extract the files. Make sure they are all in the same folder and run "SoD2 Reroll.exe".
Pick any skills you want to search for in the dropdon boxes or leave any blank to not search for any skills for that survivor. 
Hold the control button at any time to stop the program when it is searching otherwise it will keep giving virtual inputs.
The numeric field ith the label 'wait after start (seconds)' refers to the number of seconds the program will wait after pressing start before giving any inputs. This is to give time to tab into the game. Make sure the game is open and ready to reroll survivorrs before starting the program.
