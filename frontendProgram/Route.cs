using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frontendProgram.Routes
{
    public class Route
    {
        private int routeId;
        private int lineNumber;
        private string filePath;

        public Route(int routeId, int lineNumber, string filePath)
        {
            this.routeId = routeId;
            this.lineNumber = lineNumber;
            this.filePath = filePath;
        }

        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
    }
}