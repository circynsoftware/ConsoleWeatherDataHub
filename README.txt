######################################################################################
################## CONSOLE WEATHER APP FOR UK WEATHER FORECAST DATA ##################
######################################################################################
##################### Created by Chris Cooper - circyn software ######################
######################################################################################

GNU General Public License v3

#################################### DESCRIPTION #####################################

A console style app that presents the following options relating to UK Weather 
Forecasts:
	- List locations for weather forecasts (based on Office For National Statistics 
	  Data)
	- Obtain a weather forecast from the Met Office DataHub service and present the 
	  returned data to users
	- Allow a user to complete a fuzzy search on location name
	
	
#################################### REQUIREMENTS ####################################

- To retrieve data from the Met Office DataHub API, you will need to sign up to the 
  service, obtain an API Key and insert the value into the 
  weatherDataHubAPIDetails.json file as the value for the "apiKeyExt" field.
  

######################################## DATA ########################################

This application only retrieves DAILY forecast data from the MetOffice DataHub API.
With a little work, it could be expanded to make use of the other forecast options.

UK (including Northern Ireland) location data was sourced from:
- Office for National Statistics - Index of Place Names GB 2024
- OSNI Open Data - Gazetteer - Place Names

UVIndex, WeatherTypeCodes, WeatherVisibility data json files were created using
information available on the MetOffice website


#################################### AUTHOR NOTES ####################################

This could probably be done better, but it was a good learning project for me when I
was just starting to learn C#. Hopefully some other person learning to code can use this to 
help them too.