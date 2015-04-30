using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Event
    {
        public int EventId { get; set; }
        public int GebruikerID { get; set; }
        public DateTime EventBeginDatum { get; set; }
        public DateTime EventEindDatum { get; set; }
        public string EventPlaats { get; set; }
        public int EventPostcodeNumeriek { get; set; }
        public string EventPostcodeAlfanumeriek { get; set; }
        public string EventStraatnaam { get; set; }
        public int EventHuisnummer { get; set; }
        public string EventHuisnummerToevoeging { get; set; }

        public Event(int eventid, int gebruikerid, DateTime eventBeginDatum, DateTime eventEindDatum, string eventPlaats, int eventPostcodeNumeriek, string eventPostcodeAlfanumeriek, string eventStraatnaam, int eventHuisnummer, string eventHuisnummerToevoeging)
        {
            this.EventId = eventid;
            this.GebruikerID = gebruikerid;
            this.EventBeginDatum = eventBeginDatum;
            this.EventEindDatum = eventEindDatum;
            this.EventPlaats = eventPlaats;
            this.EventPostcodeNumeriek = eventPostcodeNumeriek;
            this.EventPostcodeAlfanumeriek = eventPostcodeAlfanumeriek;
            this.EventStraatnaam = eventStraatnaam;
            this.EventHuisnummer = eventHuisnummer;
            this.EventHuisnummerToevoeging = eventHuisnummerToevoeging;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
