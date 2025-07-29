using System.Text.Json.Serialization;

namespace WeatherApp
{
    public class APIDetailsRoot
    {
        public Weatherapidetail[] WeatherAPIDetails { get; set; }
    }

    public class Weatherapidetail
    {
        public string apiKeyExt { get; set; }
        public string baseURL { get; set; }
        public string forecastTypeStringURLExt { get; set; }
        public string latitudeStringURLExt { get; set; }
        public string longitudeStringURLExt { get; set; }
    }


    //classes for weather data codes definition json files
    public class WeatherTypeRoot
    {
        public Weathertype[] WeatherTypes { get; set; }
    }

    public class Weathertype
    {
        public string Description { get; set; }
        public string Value { get; set; }
    }


    //classes for weather visibility codes definition json files
    public class VisibilityRoot
    {
        public Visibility[] Visibility { get; set; }
    }

    public class Visibility
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }


    //classes for weather UV Index codes definition json files
    public class UVIndexRoot
    {
        public Uvindex[] UVIndex { get; set; }
    }

    public class Uvindex
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }


    //classes for weather region codes definition json files
    public class RegionRoot
    {
        public Region[] Regions { get; set; }
    }

    public class Region
    {
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string RegionCountry { get; set; }
    }


    //classes for weather location data json files
    public class LocationRoot
    {
        public Location[] LocationDetails { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("locationCountry")]
        public string LocationCountry { get; set; }
        [JsonPropertyName("locationCounty")]
        public string LocationCounty { get; set; }
        [JsonPropertyName("locationID")]
        public string LocationID { get; set; }
        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }
        [JsonPropertyName("locationRegion")]
        public string LocationRegion { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }


    //classes for weather forecast data json response from DataHub API

    public class ForecastResponseRoot
    {
        public Feature[] features { get; set; }
        public Parameter[] parameters { get; set; }
        public string type { get; set; }
    }

    public class Feature
    {
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
        public string type { get; set; }
    }

    public class Geometry
    {
        public float[] coordinates { get; set; }
        public string type { get; set; }
    }

    public class Properties
    {
        public string modelRunDate { get; set; }
        public float requestPointDistance { get; set; }
        public Timesery[] timeSeries { get; set; }
    }

    public class Timesery
    {
        public float dayLowerBoundMaxFeelsLikeTemp { get; set; }
        public float dayLowerBoundMaxTemp { get; set; }
        public float dayMaxScreenTemperature { get; set; }
        public float dayUpperBoundMaxFeelsLikeTemp { get; set; }
        public float dayUpperBoundMaxTemp { get; set; }
        public int midday10MWindDirection { get; set; }
        public float midday10MWindGust { get; set; }
        public float midday10MWindSpeed { get; set; }
        public int middayMslp { get; set; }
        public float middayRelativeHumidity { get; set; }
        public int middayVisibility { get; set; }
        public int midnight10MWindDirection { get; set; }
        public float midnight10MWindGust { get; set; }
        public float midnight10MWindSpeed { get; set; }
        public int midnightMslp { get; set; }
        public float midnightRelativeHumidity { get; set; }
        public int midnightVisibility { get; set; }
        public float nightLowerBoundMinFeelsLikeTemp { get; set; }
        public float nightLowerBoundMinTemp { get; set; }
        public float nightMinFeelsLikeTemp { get; set; }
        public float nightMinScreenTemperature { get; set; }
        public int nightProbabilityOfHail { get; set; }
        public int nightProbabilityOfHeavyRain { get; set; }
        public int nightProbabilityOfHeavySnow { get; set; }
        public int nightProbabilityOfPrecipitation { get; set; }
        public int nightProbabilityOfRain { get; set; }
        public int nightProbabilityOfSferics { get; set; }
        public int nightProbabilityOfSnow { get; set; }
        public int nightSignificantWeatherCode { get; set; }
        public float nightUpperBoundMinFeelsLikeTemp { get; set; }
        public float nightUpperBoundMinTemp { get; set; }
        public string time { get; set; }
        public float dayMaxFeelsLikeTemp { get; set; }
        public int dayProbabilityOfHail { get; set; }
        public int dayProbabilityOfHeavyRain { get; set; }
        public int dayProbabilityOfHeavySnow { get; set; }
        public int dayProbabilityOfPrecipitation { get; set; }
        public int dayProbabilityOfRain { get; set; }
        public int dayProbabilityOfSferics { get; set; }
        public int dayProbabilityOfSnow { get; set; }
        public int daySignificantWeatherCode { get; set; }
        public int maxUvIndex { get; set; }
    }

    public class Parameter
    {
        public Daylowerboundmaxfeelsliketemp dayLowerBoundMaxFeelsLikeTemp { get; set; }
        public Daylowerboundmaxtemp dayLowerBoundMaxTemp { get; set; }
        public Daymaxfeelsliketemp dayMaxFeelsLikeTemp { get; set; }
        public Daymaxscreentemperature dayMaxScreenTemperature { get; set; }
        public Dayprobabilityofhail dayProbabilityOfHail { get; set; }
        public Dayprobabilityofheavyrain dayProbabilityOfHeavyRain { get; set; }
        public Dayprobabilityofheavysnow dayProbabilityOfHeavySnow { get; set; }
        public Dayprobabilityofprecipitation dayProbabilityOfPrecipitation { get; set; }
        public Dayprobabilityofrain dayProbabilityOfRain { get; set; }
        public Dayprobabilityofsferics dayProbabilityOfSferics { get; set; }
        public Dayprobabilityofsnow dayProbabilityOfSnow { get; set; }
        public Daysignificantweathercode daySignificantWeatherCode { get; set; }
        public Dayupperboundmaxfeelsliketemp dayUpperBoundMaxFeelsLikeTemp { get; set; }
        public Dayupperboundmaxtemp dayUpperBoundMaxTemp { get; set; }
        public Maxuvindex maxUvIndex { get; set; }
        public Midday10mwinddirection midday10MWindDirection { get; set; }
        public Midday10mwindgust midday10MWindGust { get; set; }
        public Midday10mwindspeed midday10MWindSpeed { get; set; }
        public Middaymslp middayMslp { get; set; }
        public Middayrelativehumidity middayRelativeHumidity { get; set; }
        public Middayvisibility middayVisibility { get; set; }
        public Midnight10mwinddirection midnight10MWindDirection { get; set; }
        public Midnight10mwindgust midnight10MWindGust { get; set; }
        public Midnight10mwindspeed midnight10MWindSpeed { get; set; }
        public Midnightmslp midnightMslp { get; set; }
        public Midnightrelativehumidity midnightRelativeHumidity { get; set; }
        public Midnightvisibility midnightVisibility { get; set; }
        public Nightlowerboundminfeelsliketemp nightLowerBoundMinFeelsLikeTemp { get; set; }
        public Nightlowerboundmintemp nightLowerBoundMinTemp { get; set; }
        public Nightminfeelsliketemp nightMinFeelsLikeTemp { get; set; }
        public Nightminscreentemperature nightMinScreenTemperature { get; set; }
        public Nightprobabilityofhail nightProbabilityOfHail { get; set; }
        public Nightprobabilityofheavyrain nightProbabilityOfHeavyRain { get; set; }
        public Nightprobabilityofheavysnow nightProbabilityOfHeavySnow { get; set; }
        public Nightprobabilityofprecipitation nightProbabilityOfPrecipitation { get; set; }
        public Nightprobabilityofrain nightProbabilityOfRain { get; set; }
        public Nightprobabilityofsferics nightProbabilityOfSferics { get; set; }
        public Nightprobabilityofsnow nightProbabilityOfSnow { get; set; }
        public Nightsignificantweathercode nightSignificantWeatherCode { get; set; }
        public Nightupperboundminfeelsliketemp nightUpperBoundMinFeelsLikeTemp { get; set; }
        public Nightupperboundmintemp nightUpperBoundMinTemp { get; set; }
    }

    public class Daylowerboundmaxfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit unit { get; set; }
    }

    public class Unit
    {
        public string label { get; set; }
        public Symbol symbol { get; set; }
    }

    public class Symbol
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Daylowerboundmaxtemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit1 unit { get; set; }
    }

    public class Unit1
    {
        public string label { get; set; }
        public Symbol1 symbol { get; set; }
    }

    public class Symbol1
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Daymaxfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit2 unit { get; set; }
    }

    public class Unit2
    {
        public string label { get; set; }
        public Symbol2 symbol { get; set; }
    }

    public class Symbol2
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Daymaxscreentemperature
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit3 unit { get; set; }
    }

    public class Unit3
    {
        public string label { get; set; }
        public Symbol3 symbol { get; set; }
    }

    public class Symbol3
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofhail
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit4 unit { get; set; }
    }

    public class Unit4
    {
        public string label { get; set; }
        public Symbol4 symbol { get; set; }
    }

    public class Symbol4
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofheavyrain
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit5 unit { get; set; }
    }

    public class Unit5
    {
        public string label { get; set; }
        public Symbol5 symbol { get; set; }
    }

    public class Symbol5
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofheavysnow
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit6 unit { get; set; }
    }

    public class Unit6
    {
        public string label { get; set; }
        public Symbol6 symbol { get; set; }
    }

    public class Symbol6
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofprecipitation
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit7 unit { get; set; }
    }

    public class Unit7
    {
        public string label { get; set; }
        public Symbol7 symbol { get; set; }
    }

    public class Symbol7
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofrain
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit8 unit { get; set; }
    }

    public class Unit8
    {
        public string label { get; set; }
        public Symbol8 symbol { get; set; }
    }

    public class Symbol8
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofsferics
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit9 unit { get; set; }
    }

    public class Unit9
    {
        public string label { get; set; }
        public Symbol9 symbol { get; set; }
    }

    public class Symbol9
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayprobabilityofsnow
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit10 unit { get; set; }
    }

    public class Unit10
    {
        public string label { get; set; }
        public Symbol10 symbol { get; set; }
    }

    public class Symbol10
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Daysignificantweathercode
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit11 unit { get; set; }
    }

    public class Unit11
    {
        public string label { get; set; }
        public Symbol11 symbol { get; set; }
    }

    public class Symbol11
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayupperboundmaxfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit12 unit { get; set; }
    }

    public class Unit12
    {
        public string label { get; set; }
        public Symbol12 symbol { get; set; }
    }

    public class Symbol12
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Dayupperboundmaxtemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit13 unit { get; set; }
    }

    public class Unit13
    {
        public string label { get; set; }
        public Symbol13 symbol { get; set; }
    }

    public class Symbol13
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Maxuvindex
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit14 unit { get; set; }
    }

    public class Unit14
    {
        public string label { get; set; }
        public Symbol14 symbol { get; set; }
    }

    public class Symbol14
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midday10mwinddirection
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit15 unit { get; set; }
    }

    public class Unit15
    {
        public string label { get; set; }
        public Symbol15 symbol { get; set; }
    }

    public class Symbol15
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midday10mwindgust
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit16 unit { get; set; }
    }

    public class Unit16
    {
        public string label { get; set; }
        public Symbol16 symbol { get; set; }
    }

    public class Symbol16
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midday10mwindspeed
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit17 unit { get; set; }
    }

    public class Unit17
    {
        public string label { get; set; }
        public Symbol17 symbol { get; set; }
    }

    public class Symbol17
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Middaymslp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit18 unit { get; set; }
    }

    public class Unit18
    {
        public string label { get; set; }
        public Symbol18 symbol { get; set; }
    }

    public class Symbol18
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Middayrelativehumidity
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit19 unit { get; set; }
    }

    public class Unit19
    {
        public string label { get; set; }
        public Symbol19 symbol { get; set; }
    }

    public class Symbol19
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Middayvisibility
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit20 unit { get; set; }
    }

    public class Unit20
    {
        public string label { get; set; }
        public Symbol20 symbol { get; set; }
    }

    public class Symbol20
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnight10mwinddirection
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit21 unit { get; set; }
    }

    public class Unit21
    {
        public string label { get; set; }
        public Symbol21 symbol { get; set; }
    }

    public class Symbol21
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnight10mwindgust
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit22 unit { get; set; }
    }

    public class Unit22
    {
        public string label { get; set; }
        public Symbol22 symbol { get; set; }
    }

    public class Symbol22
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnight10mwindspeed
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit23 unit { get; set; }
    }

    public class Unit23
    {
        public string label { get; set; }
        public Symbol23 symbol { get; set; }
    }

    public class Symbol23
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnightmslp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit24 unit { get; set; }
    }

    public class Unit24
    {
        public string label { get; set; }
        public Symbol24 symbol { get; set; }
    }

    public class Symbol24
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnightrelativehumidity
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit25 unit { get; set; }
    }

    public class Unit25
    {
        public string label { get; set; }
        public Symbol25 symbol { get; set; }
    }

    public class Symbol25
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Midnightvisibility
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit26 unit { get; set; }
    }

    public class Unit26
    {
        public string label { get; set; }
        public Symbol26 symbol { get; set; }
    }

    public class Symbol26
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightlowerboundminfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit27 unit { get; set; }
    }

    public class Unit27
    {
        public string label { get; set; }
        public Symbol27 symbol { get; set; }
    }

    public class Symbol27
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightlowerboundmintemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit28 unit { get; set; }
    }

    public class Unit28
    {
        public string label { get; set; }
        public Symbol28 symbol { get; set; }
    }

    public class Symbol28
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightminfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit29 unit { get; set; }
    }

    public class Unit29
    {
        public string label { get; set; }
        public Symbol29 symbol { get; set; }
    }

    public class Symbol29
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightminscreentemperature
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit30 unit { get; set; }
    }

    public class Unit30
    {
        public string label { get; set; }
        public Symbol30 symbol { get; set; }
    }

    public class Symbol30
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofhail
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit31 unit { get; set; }
    }

    public class Unit31
    {
        public string label { get; set; }
        public Symbol31 symbol { get; set; }
    }

    public class Symbol31
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofheavyrain
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit32 unit { get; set; }
    }

    public class Unit32
    {
        public string label { get; set; }
        public Symbol32 symbol { get; set; }
    }

    public class Symbol32
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofheavysnow
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit33 unit { get; set; }
    }

    public class Unit33
    {
        public string label { get; set; }
        public Symbol33 symbol { get; set; }
    }

    public class Symbol33
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofprecipitation
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit34 unit { get; set; }
    }

    public class Unit34
    {
        public string label { get; set; }
        public Symbol34 symbol { get; set; }
    }

    public class Symbol34
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofrain
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit35 unit { get; set; }
    }

    public class Unit35
    {
        public string label { get; set; }
        public Symbol35 symbol { get; set; }
    }

    public class Symbol35
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofsferics
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit36 unit { get; set; }
    }

    public class Unit36
    {
        public string label { get; set; }
        public Symbol36 symbol { get; set; }
    }

    public class Symbol36
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightprobabilityofsnow
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit37 unit { get; set; }
    }

    public class Unit37
    {
        public string label { get; set; }
        public Symbol37 symbol { get; set; }
    }

    public class Symbol37
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightsignificantweathercode
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit38 unit { get; set; }
    }

    public class Unit38
    {
        public string label { get; set; }
        public Symbol38 symbol { get; set; }
    }

    public class Symbol38
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightupperboundminfeelsliketemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit39 unit { get; set; }
    }

    public class Unit39
    {
        public string label { get; set; }
        public Symbol39 symbol { get; set; }
    }

    public class Symbol39
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Nightupperboundmintemp
    {
        public string description { get; set; }
        public string type { get; set; }
        public Unit40 unit { get; set; }
    }

    public class Unit40
    {
        public string label { get; set; }
        public Symbol40 symbol { get; set; }
    }

    public class Symbol40
    {
        public string type { get; set; }
        public string value { get; set; }
    }


}
