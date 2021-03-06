// ***********************************************************************
// Copyright (c) 2015 NUnit Project
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Android.App;
using Android.Content.PM;
using Android.OS;

using NUnit.Runner.Services;

namespace NUnit.Runner.Tests
{
    [Activity(Label = "nunit.runner", Icon = "@drawable/icon", Theme= "@android:style/Theme.Holo.Light", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // This will load all tests within the current project
            var nunit = new NUnit.Runner.App(new List<Assembly>()
                {
                    typeof(NUnit.Runner.Tests.TestsSample).Assembly,
                });

            // If you want to add tests in another assembly
            //nunit.AddTestAssembly(typeof(MyTests).Assembly);
            // Or, if you want to add tests with an extra test options dictionary
            //nunit.AddTestAssembly(typeof(MyTests).Assembly, new Dictionary<string, object>());

            // Available options for testing
            nunit.Options = new TestOptions
            {
                // If True, the tests will run automatically when the app starts
                // otherwise you must run them manually.
                AutoRun = true,

                // If True, the application will terminate automatically after running the tests.
                //TerminateAfterExecution = true,

                // Information about the tcp listener host and port.
                // For now, send result as XML to the listening server.
                //TcpWriterParameters = new TcpWriterInfo("192.168.0.108", 13000),

                // Creates a NUnit Xml result file on the host file system using PCLStorage library.
                CreateXmlResultFile = true,

                // Choose a different path for the xml result file
                // 修改 参考https://stackoverflow.com/questions/46911486/xamarin-system-unauthorizedaccessexception-access-to-the-path-is-denied
                //ResultFilePath = Path.Combine(Environment.ExternalStorageDirectory.Path, Environment.DirectoryDownloads, "Nunit", "Results.xml")
                ResultFilePath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, Environment.DirectoryDownloads, "Nunit", "Results.xml")
            };

            LoadApplication(nunit);
        }
    }
}

