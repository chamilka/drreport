using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DoctorCarReport
{
    public class Car : DependencyObject
    {
        public string Id { get; set; }
        public string RegNo { get; set; }
    }
}
