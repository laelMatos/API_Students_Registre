using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class SessionRespose
    {
        public string Token { get; internal set; }
        public UserResponse User { get; internal set; }
        public object SessionData { get; internal set; }
    }
}
