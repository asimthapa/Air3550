using Air3550.Models;
using System;
using System.Collections.Generic;

namespace Air3550.Constants
{
    public class AppConstants
    {
        // Dictionary of US States with their abbreviate
        public static readonly Dictionary<String, String> STATE_MAP = new()
        {
            { "AL", "Alabama" },
            { "AK", "Alaska" },
            { "AZ", "Arizona" },
            { "AR", "Arkansas" },
            { "CA", "California" },
            { "CO", "Colorado" },
            { "CT", "Connecticut" },
            { "DE", "Delaware" },
            { "DC", "District Of Columbia" },
            { "FL", "Florida" },
            { "GA", "Georgia" },
            { "HI", "Hawaii" },
            { "ID", "Idaho" },
            { "IL", "Illinois" },
            { "IN", "Indiana" },
            { "IA", "Iowa" },
            { "KS", "Kansas" },
            { "KY", "Kentucky" },
            { "LA", "Louisiana" },
            { "ME", "Maine" },
            { "MD", "Maryland" },
            { "MA", "Massachusetts" },
            { "MI", "Michigan" },
            { "MN", "Minnesota" },
            { "MS", "Mississippi" },
            { "MO", "Missouri" },
            { "MT", "Montana" },
            { "NE", "Nebraska" },
            { "NV", "Nevada" },
            { "NH", "New Hampshire" },
            { "NJ", "New Jersey" },
            { "NM", "New Mexico" },
            { "NY", "New York" },
            { "NC", "North Carolina" },
            { "ND", "North Dakota" },
            { "OH", "Ohio" },
            { "OK", "Oklahoma" },
            { "OR", "Oregon" },
            { "PA", "Pennsylvania" },
            { "RI", "Rhode Island" },
            { "SC", "South Carolina" },
            { "SD", "South Dakota" },
            { "TN", "Tennessee" },
            { "TX", "Texas" },
            { "UT", "Utah" },
            { "VT", "Vermont" },
            { "VA", "Virginia" },
            { "WA", "Washington" },
            { "WV", "West Virginia" },
            { "WI", "Wisconsin" },
            { "WY", "Wyoming" }
        };

        // 10 airports
        public readonly static List<Airport> AIRPORTS = new()
        {
            new Airport() { Id = 1, Name = "Toledo Express Airport", City = "Toledo", State = "OH", Longitude = 41.58564567676877, Latitude = -83.80969204427619 },
            new Airport() { Id = 2, Name = "Cleveland Hopkins International Airport", City = "Cleaveland", State = "OH", Longitude = 41.40577433795971, Latitude = -81.85379179993403 },
            new Airport() { Id = 3, Name = "Nashville International Airport", City = "Nashville", State = "TN", Longitude = 36.126308314209695, Latitude = -86.67763952278128 },
            new Airport() { Id = 4, Name = "George Bush Intercontinental Airport", City = "Houston", State = "TX", Longitude = 30.01778108607522, Latitude = -95.33779003732307 },
            new Airport() { Id = 5, Name = "Los Angeles International Airport", City = "Los Angeles", State = "CA", Longitude = 33.944577357638394, Latitude = -118.40858675256017 },
            new Airport() { Id = 6, Name = "Denver International Airport", City = "Denver", State = "CO", Longitude = 39.98297878936022, Latitude = -104.65382488101423 },
            new Airport() { Id = 7, Name = "Albuquerque International Sunport", City = "Albuquerque", State = "NM", Longitude = 35.05304388493311, Latitude = -106.61127326306327 },
            new Airport() { Id = 8, Name = "O'Hare International Airport", City = "Chicago", State = "IL", Longitude = 42.001322335461595, Latitude = -87.91670547034117 },
            new Airport() { Id = 9, Name = "Miami International Airport", City = "Miami", State = "FL", Longitude = 25.79933306031335, Latitude = -80.28793572351069 },
            new Airport() { Id = 10, Name = "Tulsa International Airport", City = "Tulsa", State = "OK", Longitude = 36.202200512735395, Latitude = -95.88464206148326 },
        };

        // 3 types of planes
        public readonly static List<Plane> PLANES = new()
        {
            new Plane() { Model = 737, Capacity = 180 },
            new Plane() { Model = 757, Capacity = 250 },
            new Plane() { Model = 777, Capacity = 550 },
        };

        // Program Name
        public static readonly string PROGRAM_NAME = "Air-3550";
    }
}