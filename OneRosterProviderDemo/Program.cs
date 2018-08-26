﻿/*
 * Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT license.
* See LICENSE in the project root for license information.
*/

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OneRosterProviderDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Services.EnsureDatabasesMigratedAndSeeded();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilogAndSeq()
                .UseStartup<Startup>()
                .Build();
    }
}
