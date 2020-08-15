using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using System.IO;

namespace Packages.PackageDependencyManager.Editor
{
    public class VirtualFolderParser
    {
        static string xmlName = "VirtualFolder.xml";

        static ListRequest Request;

        [MenuItem("Tools/Parse")]
        static void StartSearchForXMLs()
        {
            Request = Client.List();
            EditorApplication.update += SearchForXMLs;
        }

        static void SearchForXMLs()
        {
            if (!Request.IsCompleted)
            {
                return;
            }

            if (Request.Status == StatusCode.Success)
                foreach (var package in Request.Result)
                {
                    string[] files = Directory.GetFiles(package.resolvedPath, xmlName, SearchOption.AllDirectories);
                    
                    Debug.Log($"Package name: {package.name}: {package.resolvedPath} found: {files.Length >0}");

                }
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= SearchForXMLs;
        }
    }
}