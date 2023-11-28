using System.Text.RegularExpressions;
using UnityEngine;

namespace LethalOS.API.Terminal;

internal abstract class Node
{
    public static void CreateMenuNode(Menu menu)
    {
        var node = ScriptableObject.CreateInstance<TerminalNode>();
        node.displayText = $"[{menu.MenuName}]: {menu.MenuDescription}\nC:\\{RemoveWhiteSpace(menu.MenuName)}>\n\n";
        node.clearPreviousText = true;
        node.name = menu.MenuKeyword;

        foreach (var category in menu.GetCategories)
        {
            node.displayText += $">{category.Name.ToUpper()}\n{category.Description}\n\n";
            CreateCategoryNode(category, menu);
        }
        
        AddNode(node.name, node);
    }

    private static void CreateCategoryNode(Category category, Menu menu)
    {
        var node = ScriptableObject.CreateInstance<TerminalNode>();
        node.displayText = $"[{menu.MenuName}]: {menu.MenuDescription}\nC:\\{RemoveWhiteSpace(menu.MenuName)}\\{category.Name}>\n\n";
        node.clearPreviousText = true;
        node.name = category.Keyword;

        foreach (var module in category.GetModules())
        {
            node.displayText += $">{module.DisplayName.ToUpper()}\n{module.DisplayDescription}\n\n";
        }

        foreach (var module in category.GetModules())
        {
            CreateModuleNode(module, node);
        }
        
        AddNode(node.name, node);
    }

    private static void CreateModuleNode(ModuleBase moduleBase, TerminalNode categoryNode)
    {
        var moduleNode = ScriptableObject.CreateInstance<TerminalNode>();
        moduleNode.displayText = categoryNode.displayText;
        moduleNode.clearPreviousText = true;
        moduleNode.name = moduleBase.Keyword;
        AddNode(moduleNode.name, moduleNode);
        
        moduleBase.OnAdded();
    }

    private static void AddNode(string keyword, TerminalNode node)
    {
        var terminalInstance = UnityEngine.Object.FindObjectOfType<global::Terminal>();
        
        var terminalKeyword = ScriptableObject.CreateInstance<TerminalKeyword>();
        terminalKeyword.word = keyword;
        terminalKeyword.isVerb = true;
        terminalKeyword.specialKeywordResult = node;
        
        Array.Resize(ref terminalInstance.terminalNodes.allKeywords, terminalInstance.terminalNodes.allKeywords.Length + 1);
        terminalInstance.terminalNodes.allKeywords[^1] = terminalKeyword;
        terminalInstance.terminalNodes.specialNodes.Add(node);
    }
    
    private static readonly Regex Whitespace = new(@"\s+");
    
    private static string RemoveWhiteSpace(string input) 
    {
        return Whitespace.Replace(input, string.Empty);
    }
}