using System.Text.RegularExpressions;
using HarmonyLib;
using LethalOS.TerminalSystem.Bases;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace LethalOS.TerminalSystem;

public abstract class Manager
{
    public static readonly List<Module> Modules = new();
    private static bool _terminalInit;
    
    public static bool Initialized()
    {
        if (!SceneManager.GetActiveScene().name.ToLower().Contains("samplescenerelay") || _terminalInit) return false;
        _terminalInit = true;
            
        var original = typeof(Terminal).GetMethod(nameof(Terminal.LoadNewNode));
        var prefix = typeof(TerminalPatch).GetMethod(nameof(TerminalPatch.CatchNodes));
        Plugin.Harmony.Patch(original, new HarmonyMethod(prefix));
        
        return _terminalInit;
    }
    
    public static void AddMenu(Menu menu)
    {
        var menuNode = ScriptableObject.CreateInstance<TerminalNode>();
        
        //Menu Node
        menuNode.displayText = $"[{menu.Title}]: {menu.Description}\nC:\\{RemoveWhiteSpace(menu.Title)}>\n\n";
        menuNode.clearPreviousText = true;
        menuNode.name = menu.Keyword;
        //Menu Node
        
        foreach (var category in menu.Categories)
        {
            menuNode.displayText += $">{category.Title.ToUpper()}\n{category.Description}\n\n";

            //Category Node
            var categoryNode = ScriptableObject.CreateInstance<TerminalNode>();
            categoryNode.displayText = $"[{menu.Title}]: {menu.Description}\nC:\\{RemoveWhiteSpace(menu.Title)}\\{category.Title}>\n\n";
            categoryNode.clearPreviousText = true;
            categoryNode.name = category.Keyword;
            //Category Node

            foreach (var module in category.Modules)
            {
                categoryNode.displayText += $">{module.Name.ToUpper()}\n{module.Description}\n\n";
            }

            foreach (var module in category.Modules)
            {
                //Module Node
                var moduleNode = ScriptableObject.CreateInstance<TerminalNode>();
                moduleNode.displayText = categoryNode.displayText;
                moduleNode.clearPreviousText = true;
                moduleNode.name = module.Keyword;
                //Module Node
                
                //Module Node
                AddNode(moduleNode);
                AddKeyword(module.Keyword, moduleNode);
                //Module Node
            }
            
            //Category Node
            AddNode(categoryNode);
            AddKeyword(category.Keyword, categoryNode);
            //Category Node
        }

        //Menu Node
        AddNode(menuNode);
        AddKeyword(menu.Keyword, menuNode);
        //Menu Node
    }

    private static void AddNode(TerminalNode node)
    {
        var terminalInstance = Object.FindObjectOfType<Terminal>();
        terminalInstance.terminalNodes.specialNodes.Add(node);
    }

    private static void AddKeyword(string keyword, TerminalNode terminalNode)
    {
        var terminalInstance = Object.FindObjectOfType<Terminal>();
        
        var terminalKeyword = ScriptableObject.CreateInstance<TerminalKeyword>();
        terminalKeyword.word = keyword;
        terminalKeyword.isVerb = true;
        terminalKeyword.specialKeywordResult = terminalNode;
        
        Array.Resize(ref terminalInstance.terminalNodes.allKeywords, terminalInstance.terminalNodes.allKeywords.Length + 1);
        terminalInstance.terminalNodes.allKeywords[^1] = terminalKeyword;
    }
    
    private static readonly Regex Whitespace = new(@"\s+");

    private static string RemoveWhiteSpace(string input) 
    {
        return Whitespace.Replace(input, string.Empty);
    }
    
    [HarmonyPatch(typeof(Terminal), nameof(Terminal.LoadNewNode))]
    public class TerminalPatch
    {
        public static void CatchNodes(TerminalNode node)
        {
            var module = Modules.FirstOrDefault(module => module.Keyword.ToLower() == node.name);
            module?.Toggle();
        }
    }
}