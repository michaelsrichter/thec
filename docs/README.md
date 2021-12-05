
# Take Home Engineering Challenge (For Mike Richter)

## Overview
* This project is a .Net 6 Minimal Web Api.
* The API takes some ride information from the user: 
  * The provider (e.g. Yellow, Green)
  * The distance in miles (e.g. 10)
  * The date
* The API returns an array of objects that show the user the predicted duration and cost for that ride. It also shows some options for other providers and other departure times and suggests the best option from a cost perspective. 
## Notes
* The requirement asks for Borough to Borough, but that won't reveal anything useful in terms of distance/cost. A Manhattan/Queens trip can 10 minutes. A Brooklyn/Brooklyn trip can take an hour. 
* Including a "distance" model. That should provide more accurate results, regardless of borough.
* Requirements call for filterig on datetime. I think a POC implementation of this would consider the time of day and the day of the week. A better model would be more granular, take into account starting location, the weather, the season, etc.
* FHV does not contain distance or cost information. Not using that data in the model.
* Using CsvHelper nuget package - not sure about the license - but this is just for POC.

## Build And Run

In the `src/Richter.THEC/Richter.THEC.Api` directory run:
```
docker build -f Dockerfile -t richterthecapi ..
docker run -d -p 8080:80 --name richterthecapi --env ASPNETCORE_ENVIRONMENT=Development richtertechapi

```
You can test it using CURL, like this
```
curl -X POST http://localhost:8080/api/distance -H 'Content-Type: application/json' -d '{"provider":"Yellow","distance":10,"when":"2021-12-05T17:29:11.670Z"}'
```
You can also test it using Swagger by visiting
```
http://localhost:8080/swagger/index.html
```
![Swagger Home](swagger-home.png "Swagger Home")
Click POST /api/distance
![Swagger Try](swagger-try.png "Swagger Try")
Click Try it out
Past in sample Json
```
{
  "provider": "Green",
  "distance": 5,
  "when": "2021-12-03T17:29:11.670Z"
}
```
![Swagger Execute](swagger-exec.png "Swagger Execute")
Click "Execute"

# Projects
## Richter.THEC.Data

This project 
* is for transforming the source data into a training format for ML.NET. 
* also has methods that can be used to transform the API's user input. 

## Richter.THEC.Model

This project 
* building ML.NET models that the API will use to return predictions.


# Data Models



## Cost Data Model by Distance
* Provider (Yellow,Green)
* Distance
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* Duration (Mins)
**Label**: Total
## Duration Data Model by Distance

* Provider (Yellow,Green)
* Distance
* Time of Day (Hour 0-23)
* Day of the Week (0-6)
* **Label**: Duration (Mins)
* Total

