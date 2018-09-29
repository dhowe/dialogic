﻿// IronMeta Copyright © Gordon Tisher 2018

using System.IO;

namespace IronMeta
{
    /// <summary>
    /// Simple interface for code generators.
    /// </summary>
    public interface IGenerator
    {
        void Generate(string srcName, TextWriter sb);
    }
}
