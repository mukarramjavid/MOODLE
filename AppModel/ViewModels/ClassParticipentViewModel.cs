using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel.ViewModels
{
    public class ClassParticipentViewModel
    {
        public int Class_Part_id { get; set; }
        public Nullable<int> StdUserID { get; set; }
        public Nullable<int> ClassID { get; set; }
    }
}
