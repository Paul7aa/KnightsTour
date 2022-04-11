# The Knight's Tour
## Contents Description
Application created in C# using WPF framework and MVVM architectural pattern to illustrate The Knight's Tour Problem

## Explanation
A knight's tour is a sequence of moves of a knight on a chessboard such that the knight visits every square exactly once. If the knight ends on a square that is one knight's move from the beginning square (so that it could tour the board again immediately, following the same path), the tour is closed (or re-entrant); otherwise, it is open.
GIF showing the Knight's Tour example on a 8x8 board:

![Knights Tour Gif](/githubimages/Knights-Tour-Animation.gif){
    display: block;
    float: none;
    margin-left: auto;
    margin-right: auto;
    width: 50%;
}

![Tours_Count](/githubimages/TourCount.png)

## Warnsdorff's Rule
Warnsdorff's rule is a heuristic for finding a single knight's tour. The knight is moved so that it always proceeds to the square from which the knight will have the fewest onward moves. When calculating the number of onward moves for each candidate square, we do not count moves that revisit any square already visited. It is possible to have two or more choices for which the number of onward moves is equal
In the following image, the knight will select the cell with least possibilities (the cell containing 2).

![Warnsdorffs Rule](/githubimages/WarnsdorffsRule.png)

## MVVM Structure
Model–view–viewmodel (MVVM) is a software architectural pattern that facilitates the separation of the development of the graphical user interface (the view) – be it via a markup language or GUI code – from the development of the business logic or back-end logic (the model) so that the view is not dependent on any specific model platform. The viewmodel of MVVM is a value converter, meaning the viewmodel is responsible for exposing (converting) the data objects from the model in such a way that objects are easily managed and presented. In this respect, the viewmodel is more model than view, and handles most if not all of the view's display logic.[1] The viewmodel may implement a mediator pattern, organizing access to the back-end logic around the set of use cases supported by the view.

![MVVM_Structure](/githubimages/MVVMStructure.png)

Source: https://en.wikipedia.org/wiki/Model–view–viewmodel

##Algorithm code

![Algorithm1](/githubimages/Algorithm1.png)
![Algorithm2](/githubimages/Algorithm1.png)
![Algorithm3](/githubimages/Algorithm1.png)
![Algorithm4](/githubimages/Algorithm1.png)

## Screenshots

![Screenshot 1](/githubimages/Screenshot1.png)

![Screenshot 2](/githubimages/Screenshot2.png)
