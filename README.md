# 🛳️ Battleships – Windows Forms Game
> A simple Battleship game (player vs computer) written in C# using Windows Forms. Try to sink the opponent’s fleet before yours goes down! 🔥
## 🎮 Gameplay Overview
This is a basic version of the Battleship game where the player competes against a computer-controlled opponent. The game logic is implemented in a single form (`Form1.cs`).
### 🔹 How the game works
- When the Start button is clicked:
  - The **player's board** is a **hardcoded 10x10 grid** with fixed ship positions (not user-editable).
  - The **computer's board** is **randomly generated** each time, ensuring correct ship placement (no overlaps, buffer zone around ships).
  - Both boards are displayed using two `DataGridView` components:
    - `dataGridView1`: Player's board
    - `dataGridView2`: Opponent's board (clickable)
### 🔄 Turns
- The player clicks on `dataGridView2` to shoot.
- The computer randomly selects a field on the player's board (with no memory or targeting logic).
- Each move is marked with:
  - `"X"` = hit (red cell)
  - `"+"` = miss (green cell)
- Diagonal fields surrounding a hit are automatically marked as empty – a simplified cleanup mechanic.
### 🏁 Game End
- The first side to reach 20 hits wins.
- A message is shown (`MessageBox.Show`) and the app closes (`Close()`).
### ⚠️ Limitations and Hardcoded Elements
- **Player's ship layout is hardcoded**
  - There's no option for the player to place their ships or have them randomized.
- **No smart AI**
  - The computer fires at random without tracking hits or applying any strategy.
- **Fixed ship setup**
  - The number and type of ships are predefined in code (1x4, 2x3, 3x2, 4x1). No option to customize.
- **Game logic tightly coupled to UI**
  - All game logic is embedded in `Form1.cs`. There's no separation into dedicated classes like `Board`, `Ship`, or `GameEngine`.
- **Basic visuals**
  - The UI is functional but minimal, relying on `DataGridView` cells and basic coloring for feedback.
### 🚀 How to Run
```bash
git clone https://github.com/ZiomeczekP/Statki.git
```
1. Open `Statki.sln` in Visual Studio.
2. Press ▶️ (F5) or select **Start Debugging**.
3. Click **Start** in the app window to begin playing.
### 🕹️ Game Layout

![Game Board1](Images/statki_ss.png)
![Game Board2](Images/statki_ss2.png)

> 🔹 **Left side** – Your board (ships placement)  
> 🔸 **Right side** – Opponent's board (click to attack)
