namespace AnotoWorkshop {

    public class Alias {//eventually will hold all the alias grabbing and organizing functionality.
        //public string aliasStatement;

        public Alias(string name, int startPos, int endPos, string aQuery) {//TODO - combine into the alias class itself, as it should be
            this.name = name;
            this.startPos = startPos;
            this.endPos = endPos;
            this.aQuery = aQuery;
        }

        public string name { get; set; }

        public int startPos { get; set; }

        public int endPos { get; set; }

        public string aQuery { get; set; }
    }
}