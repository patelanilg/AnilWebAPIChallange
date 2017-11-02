using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnilWebAPI.Models
{
    /// <summary>
    /// Class to represent a Vehicle CRUD model
    /// </summary>
    public class VehicleModel : OperationModel
    {
        /// <summary>
        /// Year of manufacture
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Make of the car
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Car model
        /// </summary>
        public string Model { get; set; }
    }
}