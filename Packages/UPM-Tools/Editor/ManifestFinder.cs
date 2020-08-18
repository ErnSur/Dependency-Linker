using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace QuickEye.DependencyLinker
{
    public class ManifestFinder
    {
        public const string ManifestFileName = "DependencyLinks.xml";

        [InitializeOnLoadMethod]
        [MenuItem("Window/DEBUG List (#`O′)")]
        private static async void StartSearchForXmlsAsync()
        {
            var packages = await GetProjectPackages(); // handle fail?

            foreach (var package in packages)
            {
                var dependencyLinks = Directory.GetFiles(package.resolvedPath, ManifestFileName,
                    SearchOption.AllDirectories);
                if (dependencyLinks.Length > 0)
                    Debug.Log($"Found {dependencyLinks.Length} Links in {package.name} at {package.resolvedPath}");
                foreach (var link in dependencyLinks)
                {
                    ManifestParser.Parse(link);
                }
            }
        }

        private static async Task<PackageCollection> GetProjectPackages()
        {
            var request = Client.List();
            while (!request.IsCompleted)
                await Task.Yield();
            if (request.Status >= StatusCode.Failure)
                Debug.Log($"{request.Error.message}");
            return request.Result;
        }

        private string[] GetAllFolders(string root, string folderName) =>
            new DirectoryInfo(root)
                .GetDirectories(folderName, SearchOption.AllDirectories)
                .Select(d => d.FullName)
                .ToArray();
    }

    public class ManifestParser
    {
        public static void Parse(string path)
        {
            var manifest = new XmlDocument();
            manifest.Load(new FileStream(path, FileMode.Open));
            var links = manifest.DocumentElement.SelectNodes("//Link");
            
            foreach (XmlNode link in links)
            {
                Debug.Log($"{link.Name} {link.ParentNode.Name}");
            }
        }

        private class LinkNode
        {
            public string name;
            public string relativePath;

            // public LinkNode Convert(XmlNode node)
            // {
            //     var n = new LinkNode();
            //     n.name = node.Attributes.FirstOrDefault()
            // }
        }
    }
}