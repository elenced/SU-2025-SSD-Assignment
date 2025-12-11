# SU-2025-SSD-Assigment

──────‧₊˚ Introduction  ‧₊──────

Eve Downs
Student ID: bi76tr
Module: CET2007 - Software Design and Development

For this assigment I have chosen Scenario 1:
"A small indie game studio has requested a Game Library and Player Statistics Manager to
track players and their gaming performance. The system should allow new players to be
added, record their gameplay statistics (such as hours played and high scores), and update
these stats over time. Players should be searchable by ID or username, and reports should
be generated to display the most active players or top scores. Data must be saved and
loaded using files so that progress is not lost, and important actions should be logged."

Using C# I will make an interactive program for the users of the manager. I will focus on adding these six features listed below:
1) Adding, updating and searching for players

2) Update player gameplay statistics such as hours played and high scores

3) Be able to add new games and have unique statistics for them

4) Generate reports such as most active players or highest scorers

5) Save and load player data using JSON

6) Log important actions to a text file

This application is written in C# and follows object-oriented principles throughout its design and implementation.


──────‧₊˚ Project Purpose ‧₊──────

The purpose of the system I am creating is to make users and the game studio be able to access a console-based management system to track players and their performance. The system allows users to manage player data, view statistics and generate reports. I aim to rigorously follow the teachings of OOP throughout this entire assigment to try and achieve the most marks I can. From the mark scheme I intend to demonstrate to you the following:
- Object-Oriented Design (such as classes, inheritance and interfaces)
- Data persistence using JSON (Using JSON as it's already built into C#)
- Exception handling and logging 
- Design patterns (Singleton and Interface)
- Searching and sorting algorithms

──────‧₊˚ How to run the program ‧₊──────

Below I will go into detail on how to run the system I have made:
Before doing anything you will need to make sure you have the right system requirements including:
- Visual Studio 2022 or Visual Studio Code installed - Used to write, build and run the C# program
- .NET 8.0 SDK (or latest compatible version) - Provides the compiler and runtime needed to execute C# programs
- Access to this GitHub repository(optional) - Allows you to clone the project repository directly from GitHub instead of downloading manually

After making sure you have the system requirements to run the program you have to options!
Option 1:
Go to the repository page -  https://github.com/elenced/SU-2025-SSD-Assigment
1) Click the green Code button
2) Choose Download ZIP
3) Extract the ZIP file to your computer
4) Open the solution file (.sln) in Visual Studio
5) Run the project using F5 or dotnet run in VS Code's terminal

Option 2:
If you have already have Git installed on your computer open your terminal in Visual Studio and run the following:
git clone https://github.com/elenced/SU-2025-SSD-Assigment.git

After deciding one or the other option you will end up running the solution within Visual Studio code, if all was downloaded correctly you should be greeted by a UI. Similar to this as shown below:

===== Player Statistics Manager =====
1. Add New Player
2. View Players
3. Update Player Stats
4. Search Player by ID or Username
5. Generate Report
6. Exit

Use the corresponding number keys to navigate the menu.  When the user exists the program all player data is automatically saved to a JSON file. Then, a log entry is recorded in the 'activity_log.txt' file.
All player data is stored frequently in a JSON file (players.json). Key actions are logged to a text file (activity_log.txt) to meet file I/O and persistence requirements.

──────‧₊˚ Design Choices and Justification ‧₊──────

I decided to base my system off the foundations of OOP as it allows for flexibility and reusability. Each feature of the scenario is represented by its own class with their responsibilities split as follows:

Player – Represents an individual player with attributes such as username, hours played and their high score. This shows that I can use encapsulation through private fields and public getters/setters.

PlayerController – Handles adding, updating and searching for players. It controls the  logic between the user interface and data handling. Serves as the main logic layer between UI and data storage.

FileController and IReportGenerator – Responsible for saving and loading player data. This uses the concept of polymorphism through the interface which will allow for flexibility for different file types in future. IReportGenerator demonstrates abstraction, allowing report functionality to be extended easily.

ActivityLog – Uses the Singleton pattern so only one log file is created which ensures efficient and consistent logging of the users actions.

StatisticsManager and ReportGenerator – Manages calculations for reports such as most active player and top scorer.

GameLibrary and GameClass – Represents the list of games and their details. This will use composition to show how multiple games are linked together.

PlayerReport - Generates player summaries such as top players as well as most active players. This demonstrates seperation of concerns, keeping reporting logic seperate from the core system.

Misc - For data persistence, the program uses JSON because it is built into C# which is easy to read, humans can read it meaning the indie developers can understand the data they are collecting. It allows players’ data to be saved and reloaded automatically. The search and sort functions demonstrate basic algorithm design — players can be found by ID or username and sorted by hours played or high scores.
Finally, all significant actions (adding, updating, deleting players, etc) are logged to a text file (activity_log.txt) for potential and error fixing.
