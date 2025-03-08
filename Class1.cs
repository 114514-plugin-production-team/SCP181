using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_SCP181
{
    public class Class1 : IConfig
    {
        public bool IsEnabled {  get; set; } = true;
        public bool Debug { get; set; } = true;
        public SCP181 SCP181 { get; set; } = new SCP181();
    }
}
