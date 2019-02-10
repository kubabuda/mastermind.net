#Sources 🤔

### Lai optimal strategy
Depth-first backtracking algorithm.

[An Optimal Mastermind (4,7) Strategy](https://arxiv.org/pdf/1305.1010.pdf)

[Here](http://serkangur.freeservers.com) is Excel file with optimal algo as VBA script

### Misc
[YET ANOTHER MASTERMIND STRATEGY](http://www.philos.rug.nl/~barteld/master.pdf)

[Efficient solutions for Mastermind using geneticalgorithms](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.496.276&rep=rep1&type=pdf)

[Defeating Mastermind By Justin Dowell](http://mercury.webster.edu/aleshunas/Support%20Materials/Analysis/Dowelll%20-%20Mastermind%20v2-0.pdf)

#Todo

- Knuth multithreadinf
- Optimal algo
- Genetic algo

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

## Mastermind(4, 6)

For EduInf algo on Mastermind(4, 6):
Mean rounds per solution is 4.65663580246914
Example with most rounds - 8 - is 4356
Mean execution time is 1.46097962962963 ms
Longest execution time 7.7727 ms found for 1111

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

For Swaszek algo on Mastermind(4, 6):
Mean rounds per solution is 5.03163580246914
Example with most rounds - 8 - is 5622
Mean execution time is 1.4441050154321 ms
Longest execution time 3.4595 ms found for 2653


## Mastermind(5, 8)


For EduInf algo on Mastermind(5, 8):
Mean rounds per solution is 5.92037963867188
Example with most rounds - 9 - is 43321
Mean execution time is 52.277607244873 ms
Longest execution time 134.2737 ms found for 21655

For Knuth algo on Mastermind(5, 8):
Mean rounds per solution is 5.393
Example with most rounds - 7 - is 55321
Mean execution time is 55343.4596009 ms
Longest execution time 149153.2444 ms found for 83721

For Swaszek algo on Mastermind(5, 8):
Mean rounds per solution is 6.83413696289062
Example with most rounds - 12 - is 76588
Mean execution time is 51.69709503479 ms
Longest execution time 232.4573 ms found for 21762


