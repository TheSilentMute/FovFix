using EFT.Animations;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static HarmonyLib.CodeMatcher;

namespace FovFix.ExamplePatches
{
    internal class ADSPatch
    {

        static IEnumerable<MethodBase> TargetMethods()
        {
            var result = new List<MethodBase>();

            return result;
        }

        /*static void SetADSFov()
        {

        }*/

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            //var codeMatcher = new CodeMatcher(instructions);
            var codes = new List<CodeInstruction>(instructions);

            int insertionIndex = -1;

            for (int i = 0; i < codes.Count; i++)
            {
                insertionIndex = i;
                if (codes[i].opcode == OpCodes.Ldc_R4/* && (((float)codes[i].operand == 35) || ((float)codes[i].operand == 15))*/)
                {
                    if ((float)codes[i].operand == 35)
                    {
                        var instructionsToInsert = new CodeInstruction(OpCodes.Call, Plugin.OpticsFov.Value);
                        codes[i] = instructionsToInsert;
                        break;
                    }
                    if ((float)codes[i].operand == 15)
                    {
                        var instructionsToInsert = new CodeInstruction(OpCodes.Call, Plugin.AimingDelta.Value);
                        codes[i] = instructionsToInsert;
                        break;
                    }
                }
            }
            return codes.AsEnumerable();
            //return codeMatcher.Instructions();
        }

    }
}
