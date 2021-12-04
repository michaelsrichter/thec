﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace Richter_THEC_Model
{
    public partial class DurationByDistance
    {
        /// <summary>
        /// model input class for DurationByDistance.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"Provider")]
            public string Provider { get; set; }

            [ColumnName(@"Distance")]
            public float Distance { get; set; }

            [ColumnName(@"Duration")]
            public float Duration { get; set; }

            [ColumnName(@"Total")]
            public float Total { get; set; }

            [ColumnName(@"DayAndHour")]
            public string DayAndHour { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for DurationByDistance.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            public float Score { get; set; }
        }
        #endregion

        private static string MLNetModelPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DurationByDistance.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
