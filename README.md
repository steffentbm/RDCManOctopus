# RDCManOctopus
This is a small console application written in C# that will create a RDCMan Group (*.rdg) file for Microsofts [Remote Desktop Connection Manager 2.7](https://www.microsoft.com/en-us/download/details.aspx?id=44989) based on json formatted responses from the Octopus Deploy API.

## Data sources
There are currently two repositories implemented:
* a web repository for accessing your Octopus Deploy API directly
* a file repository if you have the json filed stored locally

Configurations for both repositories can be set in the **appsettings.json** file.

## Usage
1. Update **appsettings.json** with the Octopus hostname and [Octopus API key](https://github.com/OctopusDeploy/OctopusDeploy-Api/wiki/Authentication)
2. *Optionally: Save the data from your Octopus Deploy API to json files in the Data directory*
	1. environments.json
	2. machines.json
2. Run the console application
3. **Octopus.rdg** will be generated for you

## Grouping
The grouping follows your setup in Octopus meaning machines are grouped within environments.  
A machine can be present in multiple groups if it is used in multiple environments.