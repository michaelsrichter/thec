# Projects
## Richter.THEC.Data

This project 
* is for transforming the source data into a training format for ML.NET. 
* also has methods that can be used to transform the API's user input. 

## Richter.THEC.Model

This project 
* building ML.NET models that the API will use to return predictions.


# Data Models

## Notes
* The requirement asks for Borough to Borough, but that won't reveal anything useful in terms of distance/cost. A Manhattan/Queens trip can 10 minutes. A Brooklyn/Brooklyn trip can take an hour. 
* Including a "distance" model. That should provide more accurate results, regardless of borough.
* Requirements call for filterig on datetime. I think a POC implementation of this would consider the time of day and the day of the week. A better model would be more granular, take into account starting location, the weather, the season, etc.
* FHV does not contain distance or cost information. Not using that data in the model.
* Using CsvHelper nuget package - not sure about the license - but this is just for POC.

## Cost Data Model by Borough
* Provider (Yellow,Green)
* StartBorough
* EndBorough
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* Duration (Mins)
* **Label**: Total

## Cost Data Model by Distance
* Provider (Yellow,Green)
* Distance
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* Duration (Mins)
**Label**: Total

## Duration Data Model by Borough

* Provider (Yellow,Green)
* StartBorough
* EndBorough
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* **Label**: Duration (Mins)
( Total

## Duration Data Model by Distance

* Provider (Yellow,Green)
* Distance
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* **Label**: Duration (Mins)
Total

For FHV, there is no payment or distance data.

FHV 