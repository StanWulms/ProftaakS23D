using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace event_beheer_systeem
{
    [Serializable]
    class eventaanmaken
    {
        private int camera;
        private int RFID;
        private int TV;
        private int caravan;
        public int eventid { get; set; }
        public int username { get; set; }
        public DateTime begindatum { get; set; }
        public DateTime einddatum { get; set; }
        public string plaats { get; set; }
        public int postcodenumeriek { get; set; }
        public string postcodealfa { get; set; }
        public string straat { get; set; }
        public int huisnummer { get; set; }
        public string bijvoegsel { get; set; }

        public int Camera
        {
            get { return camera; }
            set { camera = value; }
        }
        public int rfid
        {
            get { return RFID; }
            set { RFID = value; }
        }
        public int tv
        {
            get { return TV; }
            set { TV = value; }
        }
        public int Caravan
        {
            get { return caravan; }
            set { caravan = value; }
        }
        public eventaanmaken(int eventid, int username, DateTime begindatum, DateTime einddatum, string plaats, int numeriek, string alfa, string straat, int huisnummer)
        {
            this.eventid = eventid;
            this.username = username;
            this.begindatum = begindatum;
            this.einddatum = einddatum;
            this.plaats = plaats;
            this.postcodenumeriek = numeriek;
            this.postcodealfa = alfa;
            this.straat = straat;
            this.huisnummer = huisnummer;
            
        }
        public eventaanmaken(int eventid, int username, DateTime begindatum, DateTime einddatum, string plaats, int numeriek, string alfa, string straat, int huisnummer, string bijvoegsel)
        {
            this.eventid = eventid;
            this.username = username;
            this.begindatum =  begindatum;
            this.einddatum = einddatum;
            this.plaats = plaats;
            this.postcodenumeriek = numeriek;
            this.postcodealfa = alfa;
            this.straat = straat;
            this.huisnummer= huisnummer;
            this.bijvoegsel = bijvoegsel;
        }
    }
}
