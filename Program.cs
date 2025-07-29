using System.Net.Http.Headers;
using System.Globalization;
using Newtonsoft.Json;

namespace WeatherApp
{
    class WeatherAPIManager
    {
        //api variables
        static string baseURL;
        static string apiKey;
        static string forecastType;
        static string latitudeString;
        static string latitudeValue;
        static string longitudeString;
        static string longitudeValue;
        public static HttpClient myClient;
        public static bool clientCreated = false;

        //runtime variables
        public static bool anotherForecast = true;
        public static bool exitWeather = false;
        public static string myText = "";
        public static string optionSelection = "";

        public static List<(string locationID, string locationName, string locationCounty, string locationCountry, string locationRegion, string locLatitude, string locLongitude)> myLocationList = new List<(string locationID, string locationName, string locationCounty, string locationCountry, string locationRegion, string locLatitude, string locLongitude)>();
        public static List<(string regionCode, string regionName, string regionCountry)> myRegionList = new List<(string regionCode, string regionName, string regionCountry)>();
        public static Dictionary<string, string> myWeatherTypeDict = new Dictionary<string, string>();
        public static Dictionary<string, string> myUVIndexDict = new Dictionary<string, string>();
        public static Dictionary<string, string> myVisibilityDict = new Dictionary<string, string>();

        static string dataFilesDirectory;
        static string dataFilesFolder = "DataFiles/";
        public static string datafilesLocation;

        public static string weatherAPIServiceDetails = "weatherDataHubAPIDetails.json";
        public static string weatherRegionCodesJson = "weatherRegionsDataHub.json";
        public static string weatherTypeCodesJson = "weatherTypeCodes.json";
        public static string weatherUVIndexJson = "weatherUVIndex.json";
        public static string weatherVisibilityJson = "weatherVisibility.json";
        public static string weatherLocationsJson = "weatherLocations.json";

        static bool serviceAvailable = true;


        static void Main()
        {
            SetUpHTTPClient();

            RunWeatherForecastTool();
        }


        //********************************** Main / Initial methods section **********************************//
        // get directory for files
        // method to display the Weather Forecast menu options and start the main loop
        //********************************** Main / Initial methods section **********************************//

        static string GetDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }


        static void RunWeatherForecastTool()
        {
            dataFilesDirectory = GetDirectory();
            datafilesLocation = $"{dataFilesDirectory}" + $"{dataFilesFolder}";


            Console.WriteLine("\n\n############################# WEATHER FORECAST GUIDANCE NOTES #############################");
            Console.WriteLine("=> An internet connection is required to obtain UK weather data");
            Console.WriteLine("=> Enter a valid location name (e.g. UK Town) to retrieve a 7 day forecast");
            Console.WriteLine("=> If the value is not valid, use the lists option to view valid values");
            Console.WriteLine("=> NOTE: Select the ENTER key following each user input to submit your response");
            Console.WriteLine("############################# WEATHER FORECAST GUIDANCE NOTES #############################");
            Console.WriteLine("\n-------------------------------------------------------------------------------------------");
            Console.WriteLine("Weather data license attribution: Powered by Met Office data");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine();

            myText = ("Loading weather data translation files and site names ...");
            PrintMyText(myText);

            RunLoadAndProcessJsonFiles();
            myText = ("Load complete");
            PrintMyText(myText);

            myText = ($"\nFetching weather site data, please wait...");
            PrintMyText(myText);

            do
            {
                bool validUserInput = false;
                bool exitToOptions = false;

                myText = ("\n==> Enter l for a list of valid sites for forecasts\n==> Enter w to obtain a weather forecast\n==> Enter f for a fuzzy search on site name\n==> Enter m to return to the main menu");
                PrintMyText(myText);

                string readResult = Console.ReadLine();

                if (readResult != null)
                {
                    optionSelection = readResult.ToLower();
                }

                switch (optionSelection)
                {
                    case "m":
                        exitWeather = true;
                        myText = ($"\nReturning you to the main menu now.\n");
                        PrintMyText(myText);
                        break;

                    case "l":
                        SelectLocationListType();
                        break;

                    case "w":

                        do
                        {
                            while (!validUserInput)
                            {
                                if (!serviceAvailable)
                                {
                                    ApiServiceUnavailable();
                                    exitToOptions = true;
                                    break;
                                }
                                myText = ("\nEnter 'n' to retieve a forecast using Site Name (e.g. UK Town name), 's' to use SiteID or\n'm' to return to the options menu:");
                                PrintMyText(myText);

                                string forecastOption = WeatherGetUserInput();
                                switch (forecastOption)
                                {
                                    case "n":
                                        //get site name
                                        myText = ("\nEnter a site name for which you wish to receive a forecast (e.g. UK Town 'Stourbridge', 'Harlech'):");
                                        PrintMyText(myText);

                                        string siteSelected = WeatherGetUserInput();
                                        siteSelected = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(siteSelected.ToLower());

                                        //check value is in the list
                                        bool foundSite = myLocationList.Exists(x => x.locationName.Contains(siteSelected));

                                        //check here for if >1 match.  If so, then prompt user to review lists and obtain exact match or siteID
                                        bool singleMatch = CheckSiteDetailsForExactMatch(siteSelected.ToLower());

                                        if (foundSite && singleMatch)
                                        {
                                            validUserInput = true;
                                            string myLocationID = ListWeatherSiteDetails(siteSelected);
                                            GetWeatherForecast(myLocationID);

                                            if (!serviceAvailable)
                                            {
                                                exitToOptions = true;
                                            }
                                        }
                                        else if (foundSite && !singleMatch)
                                        {
                                            validUserInput = true;
                                            myText = ($"There is no exact match for the site name provided.  Please obtain the SiteID reference via Fuzzy Search and then search using SiteID");
                                            PrintMyText(myText);
                                            exitToOptions = true;
                                        }
                                        else
                                        {
                                            myText = ($"You have entered an invalid value, please try again.");
                                            PrintMyText(myText);
                                        }
                                        break;

                                    case "s":
                                        myText = ("\nEnter the site ID for which you wish to receive a forecast (e.g. Harlech ID is '351764'):");
                                        PrintMyText(myText);

                                        string siteIDSelected = WeatherGetUserInput();
                                        bool foundSiteID = myLocationList.Exists(x => x.locationID.Equals(siteIDSelected));

                                        if (foundSiteID)
                                        {
                                            validUserInput = true;
                                            ListWeatherSiteDetailsSiteID(siteIDSelected);
                                            GetWeatherForecast(siteIDSelected);
                                        }
                                        else
                                        {
                                            myText = ($"You have entered an invalid value, please try again.");
                                            PrintMyText(myText);
                                        }

                                        break;

                                    case "m":
                                        validUserInput = true;
                                        exitToOptions = true;
                                        myText = ("\nForecast request cancelled.  Returning to the options menu:");
                                        PrintMyText(myText);
                                        break;

                                    default:
                                        myText = ($"You have entered an invalid value, please try again.");
                                        PrintMyText(myText);
                                        break;
                                }
                            }

                            validUserInput = false;

                            //ask if another forecast is desired
                            while (!validUserInput)
                            {
                                if (exitToOptions)
                                {
                                    anotherForecast = false;
                                    validUserInput = true;
                                    break;
                                }

                                myText = ($"\n\nWould you like to view another forecast? (y/n):");
                                PrintMyText(myText);
                                readResult = Console.ReadLine().ToLower();

                                if (readResult == "n")
                                {
                                    anotherForecast = false;
                                    validUserInput = true;
                                }
                                else if (readResult == "y")
                                {
                                    anotherForecast = true;
                                    validUserInput = true;
                                }
                                else
                                {
                                    myText = ($"You have entered an invalid value, please try again.");
                                    PrintMyText(myText);
                                }
                            }

                            validUserInput = false;
                        }
                        while (anotherForecast);

                        break;

                    case "f":
                        myText = ($"\nEnter a full or partial site name for a fuzzy search:");
                        PrintMyText(myText);
                        string searchValue = WeatherGetUserInput();
                        FuzzySearchWeatherSiteDetails(searchValue);

                        break;

                    default:
                        myText = ($"You have entered an invalid value, please try again.");
                        PrintMyText(myText);
                        break;
                }
            }
            while (!exitWeather);

            //clear lists on exit
            myLocationList.Clear();
            myRegionList.Clear();
            myUVIndexDict.Clear();
            myWeatherTypeDict.Clear();
            myVisibilityDict.Clear();
        }



        //********************************** API call processing section **********************************//
        // create HTTP client if not already created
        // methods for calling the API methods with a wait for completion
        // method to retrieve latitude and longitude values based on locationID
        // method to call the site specific daily weather forecast API
        // method to deserialize JSON response from API and print to screen
        // methods to retrieve weather type / visibility / wind direction / UV advice descriptions
        //********************************** API call processing section **********************************//        

        public static void SetUpHTTPClient()
        {
            if (!clientCreated)
            {
                myClient = new HttpClient();
                clientCreated = true;
            }
        }


        static void GetWeatherForecast(string locationID)
        {
            // get lat/long from locationID
            Task GetLatLong = GetLatAndLongValues(locationID);
            GetLatLong.Wait();

            //if values are returned then run API
            //if no values, then provide an error message

            if (latitudeValue == null || longitudeValue == null)
            {
                Console.WriteLine("Latitude and Longitude values are not available for this locationID");
                return;
            }
            else 
            {
                Task myTask = GetApiData(myClient, latitudeValue, longitudeValue);
                myTask.Wait();
            } 
        }


        static Task GetLatAndLongValues(string locationID)
        {
            //search locations and get lat and long values
            foreach(var siteRecords in myLocationList) 
            {
                if (siteRecords.locationID == locationID) 
                {
                    latitudeValue = siteRecords.locLatitude;
                    longitudeValue = siteRecords.locLongitude;
                }
            }

            return Task.CompletedTask;
        }


        static async Task GetApiData(HttpClient client, string latValue, string longValue)
        {
            string requestURL = $"{baseURL}{forecastType}{latitudeString}{latValue}" + "&" + $"{longitudeString}{longValue}";

            using var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestURL),
                Method = HttpMethod.Get,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("apikey", apiKey);

            try 
            {
                var results = await client.SendAsync(request);

                var content = await results.Content.ReadAsStringAsync();

                DeserializeJSONAndPrintToScreen(content);
            }
            catch (Exception e)
            {
                //exception message
                Console.WriteLine(e.Message);
                ApiServiceUnavailable();
                serviceAvailable = false;
            }
        }


        static void DeserializeJSONAndPrintToScreen(string content) 
        {
            var myForecastData = JsonConvert.DeserializeObject<ForecastResponseRoot>(content);

            string currentDate = DateTime.Now.ToShortDateString();

            int recordCounter = 0;

            foreach (var forecastRecord in myForecastData.features) 
            {
                foreach (var resultRecord in forecastRecord.properties.timeSeries) 
                {
                    string myDate = resultRecord.time.ToString();
                    string myYear = myDate.Substring(0, 4);
                    string myMonth = myDate.Substring(5, 2);
                    string myDay = myDate.Substring(8, 2);
                    string myFormattedDate = myDay + "/" + myMonth + "/" + myYear;

                    //IGNORE WHERE DATE = YESTERDAY, GO TO NEXT RECORD
                    DateTime myParsedDateCurrent = DateTime.Parse(currentDate);
                    DateTime myParsedDateRecord = DateTime.Parse(myFormattedDate);

                    Console.WriteLine();

                    if (myParsedDateRecord >= myParsedDateCurrent) 
                    {
                        recordCounter++;

                        string weatherTypeDescDay = "";
                        string weatherTypeDescNight = "";
                        string visibilityDescDay = "";
                        string visibilityDescNight = "";
                        string windDirectionDay = "";
                        string windDirectionNight = "";
                        string uvAdvice = "";
                        int fieldNamePadding = 32;

                        //get matching weather type description
                        weatherTypeDescDay = GetWeatherCodeDescription(resultRecord.daySignificantWeatherCode.ToString());
                        weatherTypeDescNight = GetWeatherCodeDescription(resultRecord.nightSignificantWeatherCode.ToString());

                        //get visibility description
                        visibilityDescDay = GetVisibilityCodeDescription(resultRecord.middayVisibility);
                        visibilityDescNight = GetVisibilityCodeDescription(resultRecord.midnightVisibility);

                        //get wind direction
                        windDirectionDay = GetWeatherDirectionDescription(resultRecord.midday10MWindDirection);
                        windDirectionNight = GetWeatherDirectionDescription(resultRecord.midnight10MWindDirection);

                        //get UV Index advice
                        uvAdvice = GetUVIndexAdvice(resultRecord.maxUvIndex.ToString());

                        Console.WriteLine(myFormattedDate);
                        Console.Write($"DAY:" +
                                      $"\nWeather: ".PadRight(fieldNamePadding) + $"{weatherTypeDescDay}" +
                                      $"\nMax Temp (Celsius): ".PadRight(fieldNamePadding) + $"{resultRecord.dayUpperBoundMaxTemp}" +
                                      $"\nMax Temp Feels Like (Celsius): ".PadRight(fieldNamePadding) + $"{resultRecord.dayMaxFeelsLikeTemp}" +
                                      $"\nHail Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfHail}" +
                                      $"\nPrecipitation Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfPrecipitation}" +
                                      $"\nRain Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfRain}" +
                                      $"\nHeavy Rain Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfHeavyRain}" +
                                      $"\nLightning Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfSferics}" +
                                      $"\nSnow Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfSnow}" +
                                      $"\nHeavy Snow Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.dayProbabilityOfHeavySnow}" +
                                      $"\nWind Speed (mph): ".PadRight(fieldNamePadding) + $"{ resultRecord.midday10MWindSpeed}" +
                                      $"\nWind Direction (degrees): ".PadRight(fieldNamePadding) + $"{ resultRecord.midday10MWindDirection} : {windDirectionDay}" +
                                      $"\nRelative Humidity %: ".PadRight(fieldNamePadding) + $"{ resultRecord.middayRelativeHumidity}" +
                                      $"\nVisibility (m): ".PadRight(fieldNamePadding) + $"{ resultRecord.middayVisibility} : {visibilityDescDay}" +
                                      $"\nMax UV Index: ".PadRight(fieldNamePadding) + $"{ resultRecord.maxUvIndex}" +
                                      $"\nAdvice: ".PadRight(fieldNamePadding) + $"{uvAdvice}");

                        Console.Write($"\n\nNIGHT:" +
                                      $"\nWeather: ".PadRight(fieldNamePadding) + $"{ weatherTypeDescNight}" +
                                      $"\nMax Temp (Celsius): ".PadRight(fieldNamePadding) + $"{ resultRecord.nightUpperBoundMinTemp}" +
                                      $"\nMax Temp Feels Like (Celsius): ".PadRight(fieldNamePadding) + $"{ resultRecord.nightUpperBoundMinFeelsLikeTemp}" +
                                      $"\nHail Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfHail}" +
                                      $"\nPrecipitation Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfPrecipitation}" +
                                      $"\nRain Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfRain}" +
                                      $"\nHeavy Rain Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfHeavyRain}" +
                                      $"\nLightning Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfSferics}" +
                                      $"\nSnow Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfSnow}" +
                                      $"\nHeavy Snow Probability %: ".PadRight(fieldNamePadding) + $"{ resultRecord.nightProbabilityOfHeavySnow}" +
                                      $"\nWind Speed (mph): ".PadRight(fieldNamePadding) + $"{ resultRecord.midnight10MWindSpeed}" +
                                      $"\nWind Direction (degrees): ".PadRight(fieldNamePadding) + $"{ resultRecord.midnight10MWindDirection} : {windDirectionNight}" +
                                      $"\nRelative Humidity %: ".PadRight(fieldNamePadding) + $"{ resultRecord.midnightRelativeHumidity}" +
                                      $"\nVisibility (m): ".PadRight(fieldNamePadding) + $"{ resultRecord.midnightVisibility} : {visibilityDescNight}");

                        if (recordCounter < 7) 
                        {
                            Console.WriteLine("\n\nPress Return/Enter for the next date in the weather forecast:\n");
                            WeatherGetUserInput();
                        }
                    }
                }    
            }
            Console.WriteLine();
        }


        static string GetWeatherCodeDescription(string code) 
        {
            //get matching weather type description
            bool foundItem = myWeatherTypeDict.ContainsKey(code.ToString());
            string weatherTypeDesc = "";

            if (foundItem)
            {
                weatherTypeDesc = myWeatherTypeDict.GetValueOrDefault(code.ToString());
            }
            else
            {
                weatherTypeDesc = "Unknown";
            }

            return weatherTypeDesc;
        }


        static string GetVisibilityCodeDescription(int code)
        {
            string visibilityCode = "";

            if (code < 1000)
            {
                visibilityCode = "VP";
            }
            else if (code >= 1000 && code < 4000)
            {
                visibilityCode = "PO";
            }
            else if (code >= 4000 && code < 10000)
            {
                visibilityCode = "MO";
            }
            else if (code >= 10000 && code < 20000)
            {
                visibilityCode = "GO";
            }
            else if (code >= 20000 && code < 40000)
            {
                visibilityCode = "VG";
            }
            else if (code >= 40000)
            {
                visibilityCode = "EX";
            }
            else 
            {
                visibilityCode = "UN";
            }


            bool foundItem = myVisibilityDict.ContainsKey(visibilityCode);
            string visibilityDesc = "";

            if (foundItem)
            {
                visibilityDesc = myVisibilityDict.GetValueOrDefault(visibilityCode);
            }
            else
            {
                visibilityDesc = "Unknown";
            }

            return visibilityDesc;
        }


        static string GetWeatherDirectionDescription(int degrees) 
        {
            string weatherDirectionDesc = "";

            if (degrees > 337.5 || degrees <= 22.5)
            {
                weatherDirectionDesc = "North";
            }
            else if (degrees > 22.5 && degrees <= 67.5)
            {
                weatherDirectionDesc = "North-East";
            }
            else if (degrees > 67.5 && degrees <= 112.5)
            {
                weatherDirectionDesc = "East";
            }
            else if (degrees > 112.5 && degrees <= 157.5)
            {
                weatherDirectionDesc = "South-East";
            }
            else if (degrees > 157.5 && degrees <= 202.5)
            {
                weatherDirectionDesc = "South";
            }
            else if (degrees > 202.5 && degrees <= 247.5)
            {
                weatherDirectionDesc = "South-West";
            }
            else if (degrees > 247.5 && degrees <= 292.5)
            {
                weatherDirectionDesc = "West";
            }
            else if (degrees > 292.5 && degrees <= 337.5)
            {
                weatherDirectionDesc = "North-West";
            }
            else 
            {
                weatherDirectionDesc = "Unknown";
            }

            return weatherDirectionDesc;
        }


        static string GetUVIndexAdvice(string code)
        {
            bool foundItem = myUVIndexDict.ContainsKey(code.ToString());
            string uvAdviceString = "";

            if (foundItem)
            {
                uvAdviceString = myUVIndexDict.GetValueOrDefault(code.ToString());
            }
            else
            {
                uvAdviceString = "Unknown";
            }

            return uvAdviceString;
        }



        //********************************** Display list methods section **********************************//
        // method for displaying all weather regions
        // method for displaying all sites for a region
        // method for displaying site specific details
        // method for user input to determine which list is displayed
        //********************************** Display list methods section **********************************//

        static void ListWeatherRegions()
        {
            Console.WriteLine("\n" + "REGION CODE".PadRight(12) + "| " + "REGION NAME".PadRight(33) + "| " + "REGION COUNTRY");

            foreach (var region in myRegionList)
            {
                Console.Write($"\n{region.regionCode}".PadRight(13));
                Console.Write($"| {region.regionName}".PadRight(35));
                Console.Write($"| {region.regionCountry}");
            }

            Console.WriteLine();
        }


        static void ListWeatherSitesByRegion(string regionValue)
        {
            foreach (var site in myLocationList)
            {
                if (site.locationRegion == regionValue)
                {
                    Console.Write($"\nID: {site.locationID}".PadRight(12));
                    Console.Write($"| NAME: {site.locationName}".PadRight(60));
                    Console.Write($"| REGION: {site.locationCounty}".PadRight(43));
                    Console.Write($"| COUNTRY: {site.locationCountry}".PadRight(28));
                    Console.Write($"| REGION CODE: {site.locationRegion}");
                }
            }
            Console.WriteLine();
        }


        static string ListWeatherSiteDetails(string siteValue)
        {
            string locationID = "";
            foreach (var site in myLocationList)
            {
                if (site.locationName == siteValue)
                {
                    Console.WriteLine($"\nWeather details requested for the following site:");
                    Console.Write($"\nID: {site.locationID}".PadRight(12));
                    Console.Write($"| NAME: {site.locationName}".PadRight(60));
                    Console.Write($"| REGION: {site.locationCounty}".PadRight(43));
                    Console.Write($"| COUNTRY: {site.locationCountry}".PadRight(28));
                    Console.Write($"| REGION CODE: {site.locationRegion}");

                    locationID = site.locationID;
                }
            }
            Console.WriteLine();
            return locationID;
        }


        static void ListWeatherSiteDetailsSiteID(string siteIDValue)
        {
            foreach (var site in myLocationList)
            {
                if (site.locationID == siteIDValue)
                {
                    Console.WriteLine($"\nWeather details requested for the following site:");
                    Console.Write($"\nID: {site.locationID}".PadRight(12));
                    Console.Write($"| NAME: {site.locationName}".PadRight(60));
                    Console.Write($"| REGION: {site.locationCounty}".PadRight(43));
                    Console.Write($"| COUNTRY: {site.locationCountry}".PadRight(28));
                    Console.Write($"| REGION CODE: {site.locationRegion}");
                }
            }
            Console.WriteLine();
        }


        static void FuzzySearchWeatherSiteDetails(string siteName)
        {
            Console.WriteLine($"\nThe following weather sites contain the search term '{siteName}':");
            int counterValue = 0;
            foreach (var site in myLocationList)
            {
                if (site.locationName.ToLower().Contains(siteName))
                {
                    Console.Write($"\nID: {site.locationID}".PadRight(12));
                    Console.Write($"| NAME: {site.locationName}".PadRight(60));
                    Console.Write($"| REGION: {site.locationCounty}".PadRight(43));
                    Console.Write($"| COUNTRY: {site.locationCountry}".PadRight(28));
                    Console.Write($"| REGION CODE: {site.locationRegion}");
                    counterValue++;
                }
            }

            if (counterValue == 0)
            {
                Console.WriteLine($"\nThere are no weather site names containing the search term '{siteName}':");
            }
            Console.WriteLine();
        }


        static bool CheckSiteDetailsForExactMatch(string siteName)
        {
            bool exactMatch = false;
            int siteCount = 0;

            foreach (var site in myLocationList)
            {
                if (site.locationName.ToLower() == siteName)
                {
                    siteCount++;
                }
            }

            if (siteCount == 1)
            {
                exactMatch = true;
            }
            else
            {
                exactMatch = false;
            }

            return exactMatch;
        }

   
        static void SelectLocationListType()
        {
            bool validUserInput = false;
            string listSelected = "";

            myText = ("\nEnter 'r' to view weather regions or 's' to view sites associated to a region:");
            PrintMyText(myText);
            myText = ("Example Data: REGION CODE: gwy, REGION: Gwynedd, COUNTRY: Wales, SITE: Harlech");
            PrintMyText(myText);

            while (!validUserInput)
            {
                listSelected = WeatherGetUserInput();
                switch (listSelected)
                {
                    case "r":
                        validUserInput = true;
                        ListWeatherRegions();
                        break;

                    case "s":
                        while (!validUserInput)
                        {
                            myText = ("\nEnter the three character region code for which you wish to retrieve weather sites (e.g. gwy):");
                            PrintMyText(myText);
                            string regionSelected = WeatherGetUserInput();

                            //check value is in the list
                            bool foundRegionCode = myRegionList.Exists(x => x.regionCode.Equals(regionSelected));

                            if (foundRegionCode)
                            {
                                validUserInput = true;
                                ListWeatherSitesByRegion(regionSelected);
                            }
                            else
                            {
                                myText = ($"You have entered an invalid value, please try again.");
                                PrintMyText(myText);
                            }
                        }

                        break;

                    default:
                        myText = ($"You have entered an invalid value, please try again.");
                        PrintMyText(myText);
                        break;
                }
            }

            validUserInput = false;
        }



        //********************************** Generic methods section **********************************//
        // method for obtaining and validating user input
        // method for printing text using interface to the print text class
        //********************************** Generic methods section **********************************//

        static string WeatherGetUserInput(bool isValue = false)
        {
            while (true)
            {
                string readResult = Console.ReadLine();

                if (readResult != null)
                {
                    readResult = readResult.ToLower();
                }

                decimal parseResult;

                if (isValue)
                {
                    if (decimal.TryParse(readResult, out parseResult))
                    {
                        return parseResult.ToString();
                    }
                    else
                    {
                        Console.WriteLine($"You have entered an invalid value, please try again.");
                    }
                }
                else
                {
                    return readResult;
                }
            }
        }


        static void PrintMyText(string textValue, int textSpeed = 10)
        {
            IPrint newPrinter = new PrintText();
            newPrinter.TextPrinter(textValue, textSpeed);
        }



        //********************************** JSON file processing section **********************************//
        // generic method for loading and reading json file and returning json values
        // methods to deserialise JSON and add to lists / dictionary
        // method to run all file loads
        //********************************** JSON file processing section **********************************//

        static string MyJSONProcessor(string filename)
        {
            IResourceProcessor resouceProcessor = new EmbeddedResourceJSONProcessor();
            string result = resouceProcessor.ReadResourceAsString(filename);
            return result;
        }


        static Task PopulateAPIAccessDetails(string filename)
        {
            string myJson = MyJSONProcessor(filename);
            var myAPIDetails = JsonConvert.DeserializeObject<APIDetailsRoot>(myJson);
            foreach (var record in myAPIDetails.WeatherAPIDetails)
            {
                baseURL = record.baseURL;
                forecastType = record.forecastTypeStringURLExt;
                latitudeString = record.latitudeStringURLExt;
                longitudeString = record.longitudeStringURLExt;
                apiKey = record.apiKeyExt;
            }

            return Task.CompletedTask;
        }


        static Task PopulateWeatherRegionsListData(string filename)
        {
            string myJson = MyJSONProcessor(filename);
            var myRegions = JsonConvert.DeserializeObject<RegionRoot>(myJson);
            foreach (var region in myRegions.Regions)
            {
                myRegionList.Add(($"{region.RegionCode}", $"{region.RegionName}", $"{region.RegionCountry}"));
            }

            return Task.CompletedTask;
        }


        static Task PopulateWeatherTypeCodeListData(string filename)
        {
            string myJson = MyJSONProcessor(filename);
            var myWeatherCodes = JsonConvert.DeserializeObject<WeatherTypeRoot>(myJson);
            foreach (var typeCode in myWeatherCodes.WeatherTypes)
            {
                myWeatherTypeDict.Add(typeCode.Value, typeCode.Description);
            }

            return Task.CompletedTask;
        }


        static Task PopulateUVIndexListData(string filename)
        {
            string myJson = MyJSONProcessor(filename);
            var myUVIndexData = JsonConvert.DeserializeObject<UVIndexRoot>(myJson);
            foreach (var UVIndexRecord in myUVIndexData.UVIndex)
            {
                myUVIndexDict.Add(UVIndexRecord.Value, UVIndexRecord.Description);
            }

            return Task.CompletedTask;
        }


        static Task PopulateWeatherVisibilityListData(string filename)
        {
            string myJson = MyJSONProcessor(filename);
            var myVisibilityData = JsonConvert.DeserializeObject<VisibilityRoot>(myJson);
            foreach (var visibilityRecord in myVisibilityData.Visibility)
            {
                myVisibilityDict.Add(visibilityRecord.Value, visibilityRecord.Description);
            }

            return Task.CompletedTask;
        }


        static Task PopulateLocationsListData(string filename) 
        {
            string myJson = MyJSONProcessor(filename);
            var myLocationData = JsonConvert.DeserializeObject<LocationRoot>(myJson);

            foreach (var locationRecord in myLocationData.LocationDetails)
            {
                myLocationList.Add(($"{locationRecord.LocationID}", $"{locationRecord.LocationName}", $"{locationRecord.LocationCounty}",
                                    $"{locationRecord.LocationCountry}", $"{locationRecord.LocationRegion}", $"{locationRecord.Latitude}",
                                    $"{locationRecord.Longitude}"));
            }

            return Task.CompletedTask;
        }


        static void RunLoadAndProcessJsonFiles()
        {
            PopulateAPIAccessDetails(weatherAPIServiceDetails).Wait();
            PopulateWeatherRegionsListData(weatherRegionCodesJson).Wait();
            PopulateWeatherTypeCodeListData(weatherTypeCodesJson).Wait();
            PopulateUVIndexListData(weatherUVIndexJson).Wait();
            PopulateWeatherVisibilityListData(weatherVisibilityJson).Wait();
            PopulateLocationsListData(weatherLocationsJson).Wait();
        }



        //********************************** API unavailable section **********************************//
        // api unavailable message
        //********************************** API unavailable  section **********************************//

        static void ApiServiceUnavailable()
        {
            string myText = ("SERVICE UNAVAILABLE:\n" +
                             "The weather service is currently unavailable. Please check that you are connected to the internet or try again later");
            PrintMyText(myText);

            serviceAvailable = true;
        }
    }
}

