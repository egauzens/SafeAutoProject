using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace SafeAutoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private const string INPUT_FILE_LOCATION = "./Pages/Input/Input.txt";

        private readonly Dictionary<string, Driver> drivers;
        private string error;

        public DriversController()
        {
            drivers = new Dictionary<string, Driver>();
        }

        private void ParseReport()
        {
            try
            {
                using (var sr = new StreamReader(INPUT_FILE_LOCATION))
                {
                    while (!sr.EndOfStream)
                    {
                        var input = sr.ReadLine().Replace(':', '.').Split(' ');
                        var command = input[0].ToLower();
                        switch (command)
                        {
                            case nameof(Commands.driver):
                                if (input.Length != 2)
                                {
                                    error = nameof(Commands.driver) + " command contains wrong number of parameter(s)";
                                } 
                                else
                                {
                                    var name = input[1];
                                    // Not sure if it's an error if a driver has been declared multiple times, but if it is error can be set here
                                    if (!drivers.ContainsKey(name))
                                    {
                                        drivers.Add(name, new Driver(name));  
                                    }             
                                }
                                break;
                            case nameof(Commands.trip):
                                if (input.Length != 5)
                                {
                                    error = nameof(Commands.trip) + " command contains wrong number of parameter(s)";
                                } 
                                else
                                {
                                    var name = input[1];
                                    var driver = drivers.GetValueOrDefault(name, null);
                                    if (driver == null)
                                    {
                                        error = "Driver must first be registered before assigning them a trip!";
                                        break;
                                    }
                                    try
                                    {
                                        var startTime = double.Parse(input[2]);
                                        var endTime = double.Parse(input[3]);
                                        var milesDriven = double.Parse(input[4]);
                                        driver.AddTrip(new Trip(name, startTime, endTime, milesDriven));
                                    } 
                                    catch (SystemException e)
                                    {
                                        error = nameof(Commands.trip) + " command contains invalid parameter(s): " + e.Message;
                                    }
                                }
                                break;
                            default:
                                error = "Input file contains invalid command(s)!";
                                break;
                        }
                    }           
                }
            }
            catch (IOException e)
            {
                error = "The file could not be read: " + e.Message;
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            ParseReport();
            return string.IsNullOrEmpty(error) ? new JsonResult(StatusCode(200, drivers.Values)) : new JsonResult(StatusCode(501, error));
        }
    }
}
