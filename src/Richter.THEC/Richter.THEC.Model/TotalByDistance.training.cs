// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML;

namespace Richter_THEC_Model
{
    public partial class TotalByDistance
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Provider", @"Provider"),new InputOutputColumnPair(@"DayAndHour", @"DayAndHour")})      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"Distance", @"Distance"),new InputOutputColumnPair(@"Duration", @"Duration")}))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Provider",@"DayAndHour",@"Distance",@"Duration"}))      
                                    .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options(){NumberOfLeaves=79,MinimumExampleCountPerLeaf=128,NumberOfIterations=75,MaximumBinCountPerFeature=1024,LearningRate=6.23862375838565E-07F,LabelColumnName=@"Total",FeatureColumnName=@"Features",Booster=new GradientBooster.Options(){SubsampleFraction=1F,FeatureFraction=0.912939566461541F,L1Regularization=2E-10F,L2Regularization=53996494.4613363F}}));

            return pipeline;
        }
    }
}
