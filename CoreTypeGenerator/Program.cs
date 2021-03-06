﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elements.Generate;

namespace CoreTypeGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You need to specify the input and output directory.");
                return;
            }

            var inputDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, args[0]));
            var outputDir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, args[1]));

            Console.WriteLine($"Input dir: {inputDir}");
            Console.WriteLine($"Output dir: {outputDir}");

            var tasks = new List<Task<GenerationResult>>();

            foreach (var dir in Directory.EnumerateDirectories(inputDir, "*.*", SearchOption.AllDirectories))
            {
                tasks.AddRange(ProcessFilesInDir(dir, inputDir, outputDir));
            }
            tasks.AddRange(ProcessFilesInDir(inputDir, inputDir, outputDir));

            await Task.WhenAll(tasks.ToArray());
        }

        private static List<Task<GenerationResult>> ProcessFilesInDir(string dir, string inputDir, string outputDir)
        {
            var tasks = new List<Task<GenerationResult>>();

            foreach (var fi in Directory.EnumerateFiles(dir, "*.json"))
            {
                // ../Geometry/Vector3.json => Geometry/
                var schemaSubDir = Path.GetDirectoryName(fi).Replace(inputDir, ".");
                var outDir = Path.GetFullPath(Path.Combine(outputDir, schemaSubDir));

                if (!Directory.Exists(outDir))
                {
                    Console.WriteLine($"Creating a new directory {outDir}...");
                    Directory.CreateDirectory(outDir);
                }

                var classPath = Path.Combine(outDir, Path.GetFileNameWithoutExtension(fi) + ".g.cs");
                if (File.Exists(classPath))
                {
                    File.Delete(classPath);
                }

                var schema = File.ReadAllText(fi);
                tasks.Add(TypeGenerator.GenerateUserElementTypeFromJsonAsync(schema, outDir));
            }

            return tasks;
        }
    }
}
