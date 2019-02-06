#Sources 🤔

### Lai optimal strategy
Depth-first backtracking algorithm.
[An Optimal Mastermind (4,7) Strategy](https://arxiv.org/pdf/1305.1010.pdf)
[Here](http://serkangur.freeservers.com/MMOptimalPlayer.xlsm) is Excel file with optimal algo as VBA script

### Misc
[YET ANOTHER MASTERMIND STRATEGY](http://www.philos.rug.nl/~barteld/master.pdf)

[Efficient solutions for Mastermind using geneticalgorithms](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.496.276&rep=rep1&type=pdf)

[Defeating Mastermind By Justin Dowell](http://mercury.webster.edu/aleshunas/Support%20Materials/Analysis/Dowelll%20-%20Mastermind%20v2-0.pdf)


#Done ✔️

### Knuth 5 guess algo
[Computer as Mastermind - Knuth 1976 paper](https://www.cs.uni.edu/~wallingf/teaching/cs3530/resources/knuth-mastermind.pdf)
[C++ implementation](https://github.com/nattydredd/Mastermind-Five-Guess-Algorithm)
[Wikipedia](https://en.wikipedia.org/wiki/Mastermind_(board_game)#Five-guess_algorithm)

### Swaszek
Swaszek (1999-2000) gives an analysis of practical strategies that do not require complicated record-keeping or use of a computer. Making a random guess from the set of remaining candidate code sequences gives a surprisingly short average game length of 4.638, while interpreting each guess as a number and using the next higher number consistent with the known information gives a game of average length 4.758. 

A simple strategy which is good and computationally much faster than Knuth's is the following (I have programmed both)
Create the list 1111,...,6666 of all candidate secret codes
Start with 1122.
Repeat the following 2 steps:

* 1) After you got the answer (number of red and number of white pegs) eliminate from the list of candidates all codes that would not have produced the same answer if they were the secret code.

* 2) Pick the first element in the list and use it as new guess.

This averages no more than 5 guesses.

[On ResearchGate](https://www.researchgate.net/publication/268644635_The_mastermind_novice)

[In Polish](https://eduinf.waw.pl/inf/alg/001_search/0062.php)

[By Wolfram](http://mathworld.wolfram.com/Mastermind.html)