// See https://aka.ms/new-console-template for more information
using Richter.THEC.Data;

Console.WriteLine("Building Distance based training model");
DataTransforms.CreateTrainingDistanceTripFile(args[0], args[1], args[2]);