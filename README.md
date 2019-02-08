#Sources 🤔

### Lai optimal strategy
Depth-first backtracking algorithm.

[An Optimal Mastermind (4,7) Strategy](https://arxiv.org/pdf/1305.1010.pdf)

[Here](http://serkangur.freeservers.com) is Excel file with optimal algo as VBA script

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

# Stats

For Swaszek algo on Mastermind(4, 6):
Mean rounds per solution is 5.03163580246914
Example with most rounds - 8 - is 5622
Mean execution time is 951.299252546296 ms
Longest execution time 1917.6486 ms found for 6666

For Knuth algo on Mastermind(4, 6):
Mean rounds per solution is 4.47608024691358
Example with most rounds - 5 - is 3111
Mean execution time is 161179.983200309 ms
Longest execution time 344346.8244 ms found for 6666

For EduInf algo on Mastermind(4, 6):
Mean rounds per solution is 4.65663580246914
Example with most rounds - 8 - is 4356
Mean execution time is 871.767198919753 ms
Longest execution time 1745.1757 ms found for 6666

For Swaszek algo on Mastermind(5, 8):
Mean rounds per solution is 6.83413696289062
Example with most rounds - 12 - is 76588
Mean execution time is 728734.390547989 ms
Longest execution time 1431102.497 ms found for 88888

For Knuth algo on Mastermind(5, 8):
Mean rounds per solution is 5.393
Example with most rounds - 7 - is 55321
Mean execution time is 23781656.7436904 ms
Longest execution time 63546541.754 ms found for 85821
