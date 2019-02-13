#Mastermind

This project is implementing known Mastermind algorithms and evaluating them in order to build best Mastermind game solver possible.
Rules of Mastermind game are well described on [Wikipedia](https://en.wikipedia.org/wiki/Mastermind_(board_game)#Five-guess_algorithm)
Language choosed is multiplatform C# (.NET Core). Mastermind variants choosen to benchmark on are classic Mastermind(4, 6) - 4 pegs, 6 colors - and Deluxe Mastermind.
Benchmarked values are mean rounds to solution, worst case of rounds to solution, mean and pessimistic time to solution.

#Todo

- Knuth multithreading
- Optimal algo
- Genetic algo

#Done ✔️

### Knuth 5 guess algo

Classic method from [Knuth 1976 paper "Computer as Mastermind"](https://www.cs.uni.edu/~wallingf/teaching/cs3530/resources/knuth-mastermind.pdf) that started all this.

[C++ implementation](https://github.com/nattydredd/Mastermind-Five-Guess-Algorithm)

[Wikipedia](https://en.wikipedia.org/wiki/Mastermind_(board_game)#Five-guess_algorithm)

Parallelized version was also implemented to improve performance.

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

# Stats

Benchmarks run on i5 3250M on Ubuntu 18.04
Randomized algorithms results are not 100% reproducible due to randomness

## Mastermind(4, 6)

For Knuth algo on Mastermind(4, 6):
Mean rounds per solution is 4.47608024691358
Example with most rounds - 5 - is 3111
Mean execution time is 259.446481944444 ms
Longest execution time 506.1308 ms found for 4664

For KnuthParallel algo on Mastermind(4, 6):
Mean rounds per solution is 4.47608024691358
Example with most rounds - 5 - is 3111
Mean execution time is 162.108285416667 ms
Longest execution time 336.5064 ms found for 4443

For KnuthRandomizedParallel algo on Mastermind(4, 6):
Mean rounds per solution is 4.48070987654321
Example with most rounds - 5 - is 3111
Mean execution time is 185.622644521605 ms
Longest execution time 353.3236 ms found for 5656

For Swaszek algo on Mastermind(4, 6):
Mean rounds per solution is 5.03163580246914
Example with most rounds - 8 - is 5622
Mean execution time is 1.4441050154321 ms
Longest execution time 3.4595 ms found for 2653

For SwaszekRandomized algo on Mastermind(4, 6):
Mean rounds per solution is 4.65663580246914
Example with most rounds - 8 - is 4356
Mean execution time is 1.46097962962963 ms
Longest execution time 7.7727 ms found for 1111

For Swaszek algo with initial guess 3456 on Mastermind(4, 6):
Mean rounds per solution is 4,68287037037037
Example with most rounds - 7 - is 4341
Mean execution time is 1,44617199074074 ms
Longest execution time 12,8643 ms found for 3623

For SwaszekRandomized algo with initial guess 3456 on Mastermind(4, 6):
Mean rounds per solution is 4,66126543209877
Example with most rounds - 7 - is 3162
Mean execution time is 1,40633966049383 ms
Longest execution time 5,9019 ms found for 1111


## Mastermind(5, 8)


For Knuth algo on Mastermind(5, 8) (Take(1000)):
Mean rounds per solution is 5.393
Example with most rounds - 7 - is 55321
Mean execution time is 55343.4596009 ms
Longest execution time 149153.2444 ms found for 83721

For KnuthParallel algo on Mastermind(5, 8) (Take(1000)):
Mean rounds per solution is 5.393
Example with most rounds - 7 - is 55321
Mean execution time is 36764.7679221 ms
Longest execution time 107419.7978 ms found for 37321

For KnuthRandomizedParallel algo on Mastermind(5, 8) (Take(1000)):
Mean rounds per solution is 5.24
Example with most rounds - 7 - is 43321
Mean execution time is 38366.1176792 ms
Longest execution time 110355.2379 ms found for 68421

For Swaszek algo on Mastermind(5, 8):
Mean rounds per solution is 6.83413696289062
Example with most rounds - 12 - is 76588
Mean execution time is 51.69709503479 ms
Longest execution time 232.4573 ms found for 21762

For SwaszekRandomized algo on Mastermind(5, 8):
Mean rounds per solution is 5.92037963867188
Example with most rounds - 9 - is 43321
Mean execution time is 52.277607244873 ms
Longest execution time 134.2737 ms found for 21655

For Swaszek algo with initial 45678 on Mastermind(5, 8):
Mean rounds per solution is 5,95498657226562
Example with most rounds - 9 - is 52451
Mean execution time is 49,1665158203125 ms
Longest execution time 99,7687 ms found for 14475

For SwaszekRandomized algo with initial 45678 on Mastermind(5, 8):
Mean rounds per solution is 5,88348388671875                                                                                        
Example with most rounds - 10 - is 65461
Mean execution time is 51,8383764038086 ms
Longest execution time 125,6308 ms found for 34278

#Sources 🤔

### Lai optimal strategy
Depth-first backtracking algorithm.

[An Optimal Mastermind (4,7) Strategy](https://arxiv.org/pdf/1305.1010.pdf)

[Here](http://serkangur.freeservers.com) is Excel file with optimal algo as VBA script

### Misc
[YET ANOTHER MASTERMIND STRATEGY](http://www.philos.rug.nl/~barteld/master.pdf)

[Efficient solutions for Mastermind using geneticalgorithms](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.496.276&rep=rep1&type=pdf)

[Defeating Mastermind By Justin Dowell](http://mercury.webster.edu/aleshunas/Support%20Materials/Analysis/Dowelll%20-%20Mastermind%20v2-0.pdf)
