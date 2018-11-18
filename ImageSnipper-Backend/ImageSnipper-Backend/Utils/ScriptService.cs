﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization.Internal;

namespace ImageSnipper_Backend.Utils
{
    public static class ScriptService
    {
        public static string ExecutePythonScript(IHostingEnvironment environment, IConfiguration configuration, string image)
        {
            var projectRoot = new DirectoryInfo(environment.ContentRootPath).Parent.Parent.FullName;
            var sourceDir = Path.Combine(environment.WebRootPath, configuration.GetValue<string>("ImagesPath:Uploaded"));
            var destinationDir = Path.Combine(environment.WebRootPath, configuration.GetValue<string>("ImagesPath:Processed"));

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            var srcImage = Path.Combine(sourceDir, image);
            var destImage = Path.Combine(destinationDir, image);

            var script = Path.Combine(projectRoot, configuration.GetValue<string>("PythonScript:Path"), configuration.GetValue<string>("PythonScript:Name"));
            string[] args = new string[] { srcImage, destImage };

            return GetScriptResult(script, args);
        }

        private static string GetScriptResult(string cmd, string[] args)
        {
            var start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", cmd, args[0], args[1]),
                UseShellExecute = false,   // Do not use OS shell
                CreateNoWindow = true,   // We don't need new window
                RedirectStandardOutput = true, // Any output, generated by application will be redirected back
                RedirectStandardError = true  // Any error in standard output will be redirected back (for example exceptions)
            };

            using (var process = Process.Start(start))
            {
                if (process != null)
                    using (var reader = process.StandardOutput)
                    {
                        var stderr =
                            process.StandardError.ReadToEnd(); // StdErr result
                        var result = reader.ReadToEnd(); // StdOut result expected to be the processed image url
                        if (stderr.Length != 0)
                        {
                            throw new InvalidOperationException(stderr);
                        }
                        return result;
                    }
            }

            throw new InvalidProgramException("Could not start Python Process.");
        }
    }
}