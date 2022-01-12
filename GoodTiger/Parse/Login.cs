﻿using Protocol;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GoodTiger.Parse
{
    public static class Login
    {
        private static ulong _counter = 0;
        public static async Task<bool> Parse(this LoginRequest request, StateObject stateObject)
        {
            stateObject.UID = request.UID;
            stateObject.MemoryId = Interlocked.Add(ref _counter, 1); ;

            var csLogin = new CSLogin();
            csLogin.UID = request.UID;
            csLogin.MemoryId = stateObject.MemoryId;
            csLogin.Room = request.Room;
            csLogin.NickName = request.NickName;
            csLogin.SendChan = stateObject.SendChan;
            await stateObject.MainChan.SendAsync(csLogin);

            return true;
        }
    }
}
