﻿using static Day09.Functions;

var sequences = File.ReadAllLines("Input.txt").Select(x => x.Split(' ').Select(int.Parse).ToArray()).ToArray();

var extrapolatedValues = sequences.Select(ExtrapolatePreviousValue);

Console.WriteLine(extrapolatedValues.Sum());