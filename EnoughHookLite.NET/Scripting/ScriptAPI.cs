using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public class ScriptAPI
    {
        public ScriptOptions SOptions { get; private set; }

        public void SetupOptions()
        {
            SOptions = ScriptOptions.Default
                .WithOptimizationLevel(Microsoft.CodeAnalysis.OptimizationLevel.Release)
                .WithAllowUnsafe(false)
                .WithImports();
        }

    }
}
