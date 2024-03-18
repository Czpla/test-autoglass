namespace App.Shared.DotEnv
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static void Load(params string[] files)
        {
            foreach (var file in files)
                Load(file);
        }

        public static void Load(string file)
        {
            if (!File.Exists(file))
                return;

            foreach (var line in File.ReadAllLines(file))
            {
                if (line.Length < 4)
                    continue;

                if (line.StartsWith("#"))
                    continue;

                var parts = line.Split('=', 2);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1].Trim('"'));
            }
        }
    }
}
