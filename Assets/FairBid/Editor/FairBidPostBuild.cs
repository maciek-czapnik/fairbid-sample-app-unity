using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

public class FairBidPostBuild : MonoBehaviour
{
    [PostProcessBuild(101)]
    private static void OnPostProcessBuildPlayer(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
        {
            return;
        }

        UnityEngine.Debug.Log("FairBid: started post-build script");

        XcodeProject xcodeProject = new XcodeProject(pathToBuiltProject);
        FairBidPostBuild.UpdateProjectSettings(xcodeProject);
        FairBidPostBuild.EmbedFramework(xcodeProject);
        FairBidPostBuild.InsertBuildPhase(xcodeProject);
        xcodeProject.Save();

        UnityEngine.Debug.Log("FairBid: finished post-build script");
    }

    private static void UpdateProjectSettings(XcodeProject xcodeProject)
    {
        xcodeProject.AddBuildProperty("OTHER_LDFLAGS", "-ObjC");
        xcodeProject.SetBuildProperty("CLANG_ENABLE_MODULES", "YES");
        xcodeProject.SetBuildProperty("LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
    }

    private static void EmbedFramework(XcodeProject xcodeProject)
    {
        const string frameworkPath = "Frameworks/FairBid/Plugins/iOS";
        const string frameworkName = "FairBidSDK.framework";
        xcodeProject.EmbedFramework(frameworkName, frameworkPath);
    }

    private static void InsertBuildPhase(XcodeProject xcodeProject)
    {
        string name = "FairBidSDK - Strip Unused Architectures";
        string shellScript = "bash ${BUILT_PRODUCTS_DIR}/${FRAMEWORKS_FOLDER_PATH}/FairBidSDK.framework/strip-frameworks.sh";
        int index = 1000;
        xcodeProject.InsertShellScriptBuildPhase(name, shellScript, index);
    }
}

class XcodeProject
{
    private PBXProject pbxProject;
    private string xcodeProjectPath;
    private string targetGUID;

    public XcodeProject(string projectPath)
    {
        xcodeProjectPath = projectPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
        Open();
    }

    private void Open()
    {
        pbxProject = new PBXProject();
        pbxProject.ReadFromFile(xcodeProjectPath);

    #if UNITY_2019_3_OR_NEWER
        targetGUID = pbxProject.GetUnityMainTargetGuid();
    #else
        targetGUID = pbxProject.TargetGuidByName("Unity-iPhone");
    #endif
    }

    public void Save()
    {
        pbxProject.WriteToFile(xcodeProjectPath);
    }

    public void AddBuildProperty(string name, string value)
    {
        pbxProject.AddBuildProperty(targetGUID, name, value);
    }

    public void SetBuildProperty(string name, string value)
    {
        pbxProject.AddBuildProperty(targetGUID, name, value);
    }

    public void EmbedFramework(string name, string path)
    {
        string framework = Path.Combine(path, name);
        string fileGuid = pbxProject.FindFileGuidByProjectPath(framework);
        pbxProject.AddFileToEmbedFrameworks(targetGUID, fileGuid);
    }

    public void InsertShellScriptBuildPhase(string name, string shellScript, int index)
    {
        string shellPath = "/bin/sh";
        pbxProject.InsertShellScriptBuildPhase(index, targetGUID, name, shellPath, shellScript);
    }
}
