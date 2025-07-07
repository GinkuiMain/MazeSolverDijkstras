Submisson for my algorithms class.
Pretty much everything is explained in the code itself.

Using a modified version of Dijkstra's.

( Summarised )
A pathfinding algorithm that navigates a maze while collecting coins and avoiding walls. The goal is to find the shortest path from a starting point to a goal, using a custom version of Dijkstra's algorithm.

    The solver looks in 8 directions (up, down, left, right, and diagonals).

    Each cell in the maze may:

        Be a wall (X) and block movement.

        Contain coins (represented by numbers) to be collected.

    The algorithm prioritizes paths with fewer steps, and in case of ties, with fewer coins collected (to simulate a minimal effort path).

    Uses a priority queue to explore the best options first (lowest steps/coins).
