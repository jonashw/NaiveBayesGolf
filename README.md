# Implementation of a naive Bayes classifier for the Golf/Weather dataset.

Dataset and approach cribbed from [this video](https://www.youtube.com/watch?v=IlVINQDk4o8).

This is a console application that trains on the sample data set and then predicts the outcome for an arbitrary, sample data row.

NOTE for those who might watch the video: The fellow in the video mis-counts "Sunny" (he counts 2 Yes, 3 No instead of 3 Yes, 2 No), 
leading to incorrect probabilities (he calculates 2/9 Yes, 3/5 No rather than 3/9 Yes, 2/5 No) and likelihoods.

##Dependencies
[Microsoft Solver Foundation](https://msdn.microsoft.com/en-us/library/ff524499(v=vs.93).aspx) for the non-essential use of [Rational struct](https://msdn.microsoft.com/en-us/library/microsoft.solverfoundation.common.rational(v=vs.93).aspx). I wanted to be able to visualize my individual probabilities as fractions, like in the video starting at 3:20.  This lead me to discover Ingo's arithmetic error in the video, described above.