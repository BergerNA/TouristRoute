using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{ 
    [Serializable] class TourData
    {
        public string name;
        public string country;
        public string region;
        public int days;
        public int cost;
        public string pictureFile;
        public string descriptionTourFile;
    }
}
