This package exists because of the upm package wraper problem.
There are many unity sdk's that don't support UPM thus they live in the project Assets folder and polute the folder structure with files that the developer doesnt want or shoulnt look at. If developer wants to remove such SDK from the project it is easy for him to miss some of the files/folder thus in time making the project more and more "dirty".
Making a custom UPM package that wraps this SDK is a option but sadly some of the SDK providers make use of hardcoded paths and DLL's making it hard to compleatly encapsulate the package.

Another type of the problematic SDK is the one that makes use of the mobile platform specific files.
For Android plugins it is common to add some Android project specific files that are required to exists in Asset/Plugins/Android/SDKFolder path.

This projects goal is to make such unbehavy SDK more reasonable to wrap inside a UPM package.

The solution in quite simple.
1. When wrapping the SDK note all of its "Asset" directory dependency paths.
2. Create a special folder with specific xml in it. The xml contains the information about the package's dependencies that exist inside Assets folder.
3. The <ThisPackageName> then resolves those dependencies when project changes like so:
Resolver checks the if any of the packages in project contains the special xml.
If so ir reads is and imports those dependencies to the project Assets directory.
when it deos that it also remembers the package-dependdency paths connection so if you ever want to remove the package that contains dependencies in Assets folder it prompts you if you also want to remove x and y folders.
* Because resolver stores the package:dependencies data you can also take a look at it anytime.


PackageWrapping tool:
1. select folder.
2. analyze the folder structure:
3. provide editor that allows you to batch asmdef files creation.
4. provide package.json file creation editor.
5. export package.

Tapjoy problems:
- Uses hardcoded path in Assets folder in Dll
- adds directory to Plugins/Platform


1. can be ignored or decompile dll...
2. External plugin that

Platform Dependency Manager:
- Exposes API that:
	- Creates folders under Plugins/"Platform"
	- or deletes them when plugin is uninstalled

- Additionally it can create custom folder structure required by some plugins like Tapjoy

			-
Manager:
Looks for VirtualFolder inside Packages - Done
Creates them in Asset folder
Stores data about virtual folders and what package created them

When manager sees that it has virtual folder entry from package that no longer exists in the project it asks the user if he wants to delete it(Move to trash)


User can Manually mark folders as package dependencies that doesnt implement VirtualFolders
