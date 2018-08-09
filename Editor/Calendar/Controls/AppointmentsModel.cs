using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.Calendar
{

    public class Appointment
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Appointment(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }

    public class AppointmentSource : Dictionary<DateTime, Appointment>
    {
        public AppointmentSource()
        {
            Add(DateTime.Now.Date, new Appointment("General Board meeting", "Board meeting with general manager discussing the plans of WinRT Developement."));
            Add(DateTime.Now.AddDays(-2).Date, new Appointment("Go to church", "Special prayer. St. Peter's Basilica."));
            Add(DateTime.Now.AddDays(+2).Date, new Appointment("Go to art museum", "Fond of Da Vinci paintings. Visit to Louvre at Paris."));
            Add(DateTime.Now.AddDays(-1).Date, new Appointment("Go to movie","Monster's University, second part of Monster's inc."));
            Add(DateTime.Now.AddDays(+3).Date, new Appointment("General body checkup", "Brooklyn medical university. X-Ray, Blood test and Lipid test."));
            Add(DateTime.Now.AddDays(-3).Date, new Appointment("Wash the car", "Wash the car. Get new paintings. Tune engine."));

            Add(DateTime.Now.AddDays(-6).Date, new Appointment("Dental checkup", "Brooklyn medical university. Dental checkup"));
            Add(DateTime.Now.AddDays(6).Date, new Appointment("Go to Furniture shop", "Get new modern furnitures decorating house."));
            Add(DateTime.Now.AddDays(-4).Date, new Appointment("Client meeting", "Meeting with new clients from America."));
            Add(DateTime.Now.AddDays(4).Date, new Appointment("Company auditing", "Discuss plans for annual company auding."));
            Add(DateTime.Now.AddDays(-7).Date, new Appointment("Team meet", "Discussion about new product launch with general manager."));

            Add(DateTime.Now.AddDays(8).Date, new Appointment("Attend party", "Attend birthday party of Mr.Mathew Lechler at Minerve hotel."));
            Add(DateTime.Now.AddDays(9).Date, new Appointment("Buisness Meeting", "Meeting with buisness head about annual sales of product."));
            Add(DateTime.Now.AddDays(14).Date, new Appointment("Conference", "Attend IInd international conference about WinRT Developement."));
            Add(DateTime.Now.AddDays(15).Date, new Appointment("Business meeting", "Business deal with new technical partner."));
            Add(DateTime.Now.AddDays(-11).Date, new Appointment("Recruitment programme", "Recruit resources for new branch at Paris."));

            Add(DateTime.Now.AddDays(5).Date, new Appointment("Business meeting", "Consulting firm laws with business advisers."));
            Add(DateTime.Now.AddDays(-5).Date, new Appointment("Partners meeting", "Planning new investments with partners."));
            Add(DateTime.Now.AddDays(12).Date, new Appointment("Go to church", "Special prayer. St. Peter's Basilica."));
            Add(DateTime.Now.AddDays(11).Date, new Appointment("Sports match", "Watching Tennis match at Roland Garros Stadium."));
            Add(DateTime.Now.AddDays(13).Date, new Appointment("General Board meeting", "Board meeting with general manager discussing the plans of new project."));
        }
    }

}
