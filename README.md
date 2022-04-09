# The Knight's Tour
## Contents Description
Application created in C# using WPF framework and MVVM architectural pattern to illustrate The Knight's Tour Problem

## Explanation
A knight's tour is a sequence of moves of a knight on a chessboard such that the knight visits every square exactly once. If the knight ends on a square that is one knight's move from the beginning square (so that it could tour the board again immediately, following the same path), the tour is closed (or re-entrant); otherwise, it is open.
GIF showing the Knight's Tour example on a 8x8 board:

![Knights Tour Gif](https://github.com/Paul7aa/KnightsTour/blob/main/githubimages/Knights-Tour-Animation.gif)

## Warnsdorff's Rule
Warnsdorff's rule is a heuristic for finding a single knight's tour. The knight is moved so that it always proceeds to the square from which the knight will have the fewest onward moves. When calculating the number of onward moves for each candidate square, we do not count moves that revisit any square already visited. It is possible to have two or more choices for which the number of onward moves is equal
In the following image, the knight will select the cell with least possibilities (the cell containing 2).

![Warnsdorffs Rule png](https://github.com/Paul7aa/KnightsTour/blob/main/githubimages/WarnsdorffsRule.png)

## Screenshots

![Screenshot 1](https://github.com/Paul7aa/KnightTour/blob/main/githubimages/Screenshot1.png)

![Screenshot 2](https://github.com/Paul7aa/KnightTour/blob/main/githubimages/Screenshot2.png)
